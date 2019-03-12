using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using System.Configuration;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;

namespace PowerpointSlideGenerator
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /* ####### Field Variables ####### */

        /// <summary>
        /// This field variable contains an array of extranneous terms. Using this list we can
        /// compare any text entries and narrow them down to only keywords.
        /// </summary>
        public string[] extras = ConfigurationManager.AppSettings["extras"].Split(',');
        
        /// <summary>
        /// This field variable contains the apikey for the Google Custom Search API used
        /// to return images from the internet.
        /// </summary>
        public string search_api_key = ConfigurationManager.AppSettings["customapikey"];

        /// <summary>
        /// This field variable contains the apikey for the Google Custom Search API used
        /// to return images from the internet.
        /// </summary>
        public string rapid_search_key = ConfigurationManager.AppSettings["rapidapikey"];

        /// <summary>
        /// This field variable simply holds the filename for the csv file that saves
        /// all of the information for each powerpoint slide.
        /// </summary>
        public string csv_slide_file = ConfigurationManager.AppSettings["csvslides"];

        /// <summary>
        /// Field variable that defines a limit to the number of pictures returned from
        /// the internet. Default set to 20.
        /// </summary>
        public int search_limit = int.Parse(ConfigurationManager.AppSettings["piclimit"]);

        /// <summary>
        /// Make a variable to contain a counter/placeholder for the current slide being worked on.
        /// </summary>
        public int current_slide = 0;

        /// <summary>
        /// Field variable to hold the current slide image url
        /// </summary>
        public string current_slide_img = string.Empty;

        /// <summary>
        /// Create a field variable that can store the total number of slides.
        /// </summary>
        public int slide_count = 0;

        /// <summary>
        /// 
        /// </summary>
        public char sep_char = ',';

        /// <summary>
        /// This is a field variable to contain the search terms.
        /// </summary>
        public List<string> terms_for_search = new List<string>();

        /* ####### END OF Field Variables ####### */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void num_slides_butt_Click(object sender, EventArgs e)
        {
            int total_slides = 0;
            if (int.TryParse(num_slides_in.Text, out total_slides))
            {
                //1) Save the total amount of slides into the slide count field variable.
                slide_count = total_slides;

                //2) Create the entries for the listbox
                for (int y = 0; y < total_slides; y++)
                {
                    slide_selector.Items.Add("("+ (y + 1) +")");
                }

                //3) First clear the info in the file if the boolean flag says this is a new file?
                try
                {
                    File.WriteAllText(csv_slide_file, string.Empty);
                }
                catch (Exception err)
                {
                    Console.WriteLine("Can't Access File in order to clear the contents");
                    Console.WriteLine(err.Message);
                }
                //4) Create the entries in the CSV file that can be edited later. 
                using (StreamWriter slide_info = new StreamWriter(new FileStream(csv_slide_file, FileMode.Create, FileAccess.Write)))
                {
                    //Create the Header String and write it to the CSV file
                    string header = "Slide Number, Slide Title, Slide Text, Slide Image";
                    slide_info.WriteLine(header);

                    //Use a for loop to write the info for each slide. 
                    for (int x = 0; x < slide_count; x++)
                    {
                        string data = (x+1).ToString() + ",Enter Title,Enter Text,Enter Image";
                        slide_info.WriteLine(data);
                    }
                    slide_info.Close();
                }
            }
            else
            {
                Console.WriteLine("No luck");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void change_slide(object sender, EventArgs e)
        {
            if (slide_selector.SelectedIndex == -1)
            {
                return;
            }

            //i) Declare some placeholder helpers
            int slide_number = 0;
            int slide_tit = 1;
            int slide_txt = 2;
            int slide_picture = 3;

            //Change the current slide field variable index
            current_slide = slide_selector.SelectedIndex;

            //Modify the label
            current_slide_label.Text = (current_slide + 1).ToString();

            //Pull the slide title and slide text info from the csv file
            List<List<string>> info_list = new List<List<string>>();
            try
            {
                //3A) Open csv file
                StreamReader info = File.OpenText(csv_slide_file);
                //3B) Get header line to be used later
                string[] header_line = info.ReadLine().Split(sep_char);

                //3C) Use a while loop to get all of the info
                while (!info.EndOfStream)
                {
                    string rawline = info.ReadLine();
                    //string[] line = sep_and_return(rawline);
                    string[] line = rawline.Split(sep_char);
                    List<string> entry = line.ToList();
                    info_list.Add(entry);
                }

                //Always be sure to close the file after opening it!
                info.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            if ((slide_selector.SelectedIndex + 1) != (current_slide))
            {
                terms_for_search.Clear();
                title_input.Text = info_list[current_slide][slide_tit];
                text_input.Text = info_list[current_slide][slide_txt];

                thumbs_panel.Controls.Clear();
            }
            //message_label.Text = slide_selector.SelectedIndex.ToString();
            //foreach (Control control in list_thumbs)
            //{
            //    //thumbs_panel.Controls.Remove(control);
            //    control.Dispose();
            //}
        }

        /// <summary>
        /// Save the info for the current slide. Use the select box in order to determine which slide is currently
        /// active.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_slide_butt_Click(object sender, EventArgs e)
        {
            message_label.Text = string.Empty;
            if (slide_selector.SelectedIndex == -1)
            {
                message_label.Text = "You need to select a slide from the listbox.";
                return;
            }
            //i) Declare some placeholder helpers
            int slide_number = 0;
            int slide_tit = 1;
            int slide_txt = 2;
            int slide_picture = 3;

            //1) First Get the information from each input
            int slide = current_slide;
            string title = string.Empty;
            string text = string.Empty;

            if (title_input.Text == "" && text_input.Text == "")
            {
                message_label.Text = "Please provide a Slide Title";
                return;
            }
            else
            {
                title = title_input.Text;
            }
            
            if (text_input.Text == "")
            {
                message_label.Text = "Don't Forget to include some Slide Text.";
            }
            else
            {
                text = text_input.Text;
            }
            
            List<List<string>> info_list = new List<List<string>>();


            //2) Get the selected image for the slide
            string selected_image = current_slide_img;


            //3) Open csv file, read contents
            try
            {
                //3A) Open csv file
                StreamReader info = File.OpenText(csv_slide_file);
                //3B) Get header line to be used later
                string[] header_line = info.ReadLine().Split(sep_char);

                //3C) Use a while loop to get all of the info
                while (!info.EndOfStream)
                {
                    string rawline = info.ReadLine();
                    //string[] line = sep_and_return(rawline);
                    string[] line = rawline.Split(sep_char);
                    List<string> entry = line.ToList();
                    info_list.Add(entry);
                }

                //Always be sure to close the file after opening it!
                info.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            //4) Write the modified info back to the CSV File
            //4A) Modify the entry for the current slide
            info_list[slide][slide_tit] = title;
            info_list[slide][slide_txt] = text;
            info_list[slide][slide_picture] = selected_image;

            //4B) First clear the info in the file
            try
            {
                File.WriteAllText(csv_slide_file, string.Empty);
            }
            catch (Exception err)
            {
                Console.WriteLine("Can't Access File in order to clear the contents");
                Console.WriteLine(err.Message);
            }

            //4C) Use the using statement to reopen the csv file and save the new info
            using (StreamWriter slide_info = new StreamWriter(new FileStream(csv_slide_file, FileMode.Create, FileAccess.Write)))
            {
                //Create the Header String and write it to the CSV file
                string header = "Slide Number, Slide Title, Slide Text, Slide Image";
                slide_info.WriteLine(header);

                //Use a for loop to write the info for each slide. 
                for (int x = 0; x < slide_count; x++)
                {
                    string data = string.Join(sep_char.ToString(), info_list[x]);
                    slide_info.WriteLine(data);
                }
                slide_info.Close();
            }

            //Modify the itembox values after everything is saved.
            slide_selector.Items[slide_selector.SelectedIndex] = "(" + (slide_selector.SelectedIndex + 1).ToString() + ")" + " " + title;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string get_search_terms()
        {
            //Get text from search box
            string search_val = title_input.Text.ToString();

            //Search for images using text values
            string[] search_terms = search_val.Split(' ');

            /* Counter for the foreach */
            int n = 0;

            /* Counter for the search_terms_array */
            int t = 0;

            foreach (string terms in search_terms)
            {
                n++;
                if (!extras.Any(terms.Contains))
                {
                    //Add this term to the array that will be sent to the internet
                    terms_for_search.Add(terms);
                }
            }

            //Pack all of the terms into an array
            string[] searchables = terms_for_search.ToArray();

            //Combine the array into one string
            string search_list = string.Join(",", searchables);

            //Return the string list
            return search_list;

        }

        /// <summary>
        /// This function finds images online through use of the Google Custom Search API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void find_slide_images_Click(object sender, EventArgs e)
        {
            //Get the terms from the search fields
            string search_list = get_search_terms();

            //Split the terms again into an array.
            string[] searchables = search_list.Split(',');

            //Load the query variable
            string query = string.Join("+", searchables);

            //Search the internet for shtuff
            var url = "https://contextualwebsearch-websearch-v1.p.rapidapi.com/api/Search/ImageSearchAPI?autoCorrect=true&pageNumber=1&pageSize="+ search_limit +"&q=" + query + "&safeSearch=true";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", rapid_search_key);
            var response = await httpClient.GetStringAsync(url);
            //var json = JsonConvert.DeserializeObject<object>(response);
            var details = JObject.Parse(response);

            var images = details["value"].ToArray();
            List<string> pictures = new List<string>();
            foreach (var image in images)
            {
                //Get the URLS from the search
                string pic_url = image["url"].ToString();
                Console.WriteLine(pic_url);

                //Create some pictureboxes
                var pb = new PictureBox();
                pb.Click += picture_click;
                pb.Paint += picture_paint;
                pb.ImageLocation = pic_url;
                try
                {
                    pb.Load();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
                pb.Size = new Size(100, 100);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                
                //Add the PictureBox to the FlowLayoutPanel
                thumbs_panel.Controls.Add(pb);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// This method is called when one of the picture boxes inside of the flowlayoutpanel
        /// is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picture_click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                //Once the picture is clicked we want to give it a border for highlight
                message_label.Text = pb.ImageLocation;
                pb.Tag = Color.Azure;
                pb.Refresh();

                string filename = pb.ImageLocation;
                filename = Path.GetFileName(filename);

                //Then save it for the powerpoint.
                Bitmap slide_img = new Bitmap(pb.Image);
                ImageCodecInfo imgcodecinfo;
                System.Drawing.Imaging.Encoder img_encoder;
                EncoderParameter img_encoder_param;
                EncoderParameters img_encoder_params;

                imgcodecinfo = GetEncoderInfo("image/jpeg");
                img_encoder = System.Drawing.Imaging.Encoder.Quality;

                img_encoder_params = new EncoderParameters(1);

                img_encoder_param = new EncoderParameter(img_encoder, 90L);
                img_encoder_params.Param[0] = img_encoder_param;
                slide_img.Save(filename, imgcodecinfo, img_encoder_params);

                //Record image filename in the field variable
                FileInfo info = new FileInfo(filename);
                current_slide_img = info.FullName;
            }
            else
            {
                message_label.Text = "No Luck";
            }
        }

        /// <summary>
        /// This method allows some intial border settings for each picture loaded
        /// from the internet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picture_paint(object sender, PaintEventArgs e)
        {
            PictureBox picbox = sender as PictureBox;
            if (picbox.Tag == null)
            {
                picbox.Tag = Color.Black;
            }
            ControlPaint.DrawBorder(e.Graphics, picbox.ClientRectangle, (Color)picbox.Tag, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// This method will generate the PowerPoint from the information provided in the slide.csv.
        /// It will take each slide title, slide text, and slide image and make it into a simple 
        /// powerpoint with simple white background and normal fonts/sizes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gen_slide_button_Click(object sender, EventArgs e)
        {
            //string PictureFile = @"C:\powerpoint\img1.jpg";
            //First get required information from csv file
            //3) Open csv file, read contents
            List<List<string>> info_list = new List<List<string>>();
            try
            {
                //3A) Open csv file
                StreamReader info = File.OpenText(csv_slide_file);
                //3B) Get header line to be used later
                string[] header_line = info.ReadLine().Split(sep_char);

                //3C) Use a while loop to get all of the info
                while (!info.EndOfStream)
                {
                    string rawline = info.ReadLine();
                    //string[] line = sep_and_return(rawline);
                    string[] line = rawline.Split(sep_char);
                    List<string> entry = line.ToList();
                    info_list.Add(entry);
                }

                //Always be sure to close the file after opening it!
                info.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            //Then create PowerPoint File
            Microsoft.Office.Interop.PowerPoint.Application ppt = new Microsoft.Office.Interop.PowerPoint.Application();
            Presentation pptpresent = ppt.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);
            int num_slides = slide_count;
            for (int i = 0; i < num_slides; i++)
            {
                string[] current_info = info_list[i].ToArray();
                Microsoft.Office.Interop.PowerPoint.Slides slides;
                Microsoft.Office.Interop.PowerPoint._Slide slide;
                Microsoft.Office.Interop.PowerPoint.TextRange slide_title;
                Microsoft.Office.Interop.PowerPoint.TextRange slide_text;

                Microsoft.Office.Interop.PowerPoint.CustomLayout custLayout =
                    pptpresent.SlideMaster.CustomLayouts[Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutTable];

                slides = pptpresent.Slides;
                slide = slides.AddSlide(i + 1, custLayout);

                //slide.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, 10, 10, 100, 10);
                //slide.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, 30, 30, 100, 10);
                //slide.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, 50, 50, 100, 10);

                //slide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 10, 10, 300, 100);
                slide_title = slide.Shapes[1].TextFrame.TextRange;
                slide_title.Text = current_info[1];
                slide_title.Font.Name = "";
                slide_title.Font.Size = 32;

                //slide.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 10, 110, 300, 300).TextFrame.TextRange;
                slide_text = slide.Shapes[2].TextFrame.TextRange;
                slide_text.Text = current_info[2];
                slide_text.Font.Name = "";
                slide_text.Font.Size = 18;

                string PictureFile = current_info[3];
                Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[3];
                slide.Shapes.AddPicture(PictureFile, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, shape.Left, shape.Top, shape.Width, shape.Height);

            }
            //pptpresent.SaveAs("newslide.pptx", Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, Microsoft.Office.Core.MsoTriState.msoTrue);
        }
    }
}
