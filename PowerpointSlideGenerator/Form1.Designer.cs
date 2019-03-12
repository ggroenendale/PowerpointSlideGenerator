namespace PowerpointSlideGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title_input = new System.Windows.Forms.TextBox();
            this.title_in_label = new System.Windows.Forms.Label();
            this.text_input = new System.Windows.Forms.TextBox();
            this.text_in_label = new System.Windows.Forms.Label();
            this.gen_slide_button = new System.Windows.Forms.Button();
            this.find_slide_images = new System.Windows.Forms.Button();
            this.slide_selector = new System.Windows.Forms.ListBox();
            this.num_slides_butt = new System.Windows.Forms.Button();
            this.num_slides_in = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.save_slide_butt = new System.Windows.Forms.Button();
            this.current_label = new System.Windows.Forms.Label();
            this.current_slide_label = new System.Windows.Forms.Label();
            this.message_label = new System.Windows.Forms.Label();
            this.thumbs_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // title_input
            // 
            this.title_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_input.Location = new System.Drawing.Point(353, 23);
            this.title_input.Name = "title_input";
            this.title_input.Size = new System.Drawing.Size(297, 24);
            this.title_input.TabIndex = 0;
            // 
            // title_in_label
            // 
            this.title_in_label.AutoSize = true;
            this.title_in_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_in_label.Location = new System.Drawing.Point(249, 24);
            this.title_in_label.Name = "title_in_label";
            this.title_in_label.Size = new System.Drawing.Size(92, 24);
            this.title_in_label.TabIndex = 1;
            this.title_in_label.Text = "Slide Title";
            // 
            // text_input
            // 
            this.text_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_input.Location = new System.Drawing.Point(353, 77);
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(344, 100);
            this.text_input.TabIndex = 2;
            // 
            // text_in_label
            // 
            this.text_in_label.AutoSize = true;
            this.text_in_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_in_label.Location = new System.Drawing.Point(247, 83);
            this.text_in_label.Name = "text_in_label";
            this.text_in_label.Size = new System.Drawing.Size(94, 24);
            this.text_in_label.TabIndex = 3;
            this.text_in_label.Text = "Slide Text";
            // 
            // gen_slide_button
            // 
            this.gen_slide_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gen_slide_button.Location = new System.Drawing.Point(728, 129);
            this.gen_slide_button.Name = "gen_slide_button";
            this.gen_slide_button.Size = new System.Drawing.Size(177, 48);
            this.gen_slide_button.TabIndex = 4;
            this.gen_slide_button.Text = "Generate Powerpoint Slide";
            this.gen_slide_button.UseVisualStyleBackColor = true;
            this.gen_slide_button.Click += new System.EventHandler(this.gen_slide_button_Click);
            // 
            // find_slide_images
            // 
            this.find_slide_images.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.find_slide_images.Location = new System.Drawing.Point(728, 72);
            this.find_slide_images.Name = "find_slide_images";
            this.find_slide_images.Size = new System.Drawing.Size(177, 48);
            this.find_slide_images.TabIndex = 5;
            this.find_slide_images.Text = "Find Slide Images";
            this.find_slide_images.UseVisualStyleBackColor = true;
            this.find_slide_images.Click += new System.EventHandler(this.find_slide_images_Click);
            // 
            // slide_selector
            // 
            this.slide_selector.FormattingEnabled = true;
            this.slide_selector.Location = new System.Drawing.Point(126, 12);
            this.slide_selector.Name = "slide_selector";
            this.slide_selector.Size = new System.Drawing.Size(74, 95);
            this.slide_selector.TabIndex = 6;
            this.slide_selector.SelectedValueChanged += new System.EventHandler(this.change_slide);
            // 
            // num_slides_butt
            // 
            this.num_slides_butt.Location = new System.Drawing.Point(28, 86);
            this.num_slides_butt.Name = "num_slides_butt";
            this.num_slides_butt.Size = new System.Drawing.Size(75, 23);
            this.num_slides_butt.TabIndex = 7;
            this.num_slides_butt.Text = "Set";
            this.num_slides_butt.UseVisualStyleBackColor = true;
            this.num_slides_butt.Click += new System.EventHandler(this.num_slides_butt_Click);
            // 
            // num_slides_in
            // 
            this.num_slides_in.Location = new System.Drawing.Point(28, 48);
            this.num_slides_in.Name = "num_slides_in";
            this.num_slides_in.Size = new System.Drawing.Size(75, 20);
            this.num_slides_in.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Number of Slides";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // save_slide_butt
            // 
            this.save_slide_butt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_slide_butt.Location = new System.Drawing.Point(728, 14);
            this.save_slide_butt.Name = "save_slide_butt";
            this.save_slide_butt.Size = new System.Drawing.Size(177, 48);
            this.save_slide_butt.TabIndex = 10;
            this.save_slide_butt.Text = "Save Current Slide";
            this.save_slide_butt.UseVisualStyleBackColor = true;
            this.save_slide_butt.Click += new System.EventHandler(this.save_slide_butt_Click);
            // 
            // current_label
            // 
            this.current_label.AutoSize = true;
            this.current_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_label.Location = new System.Drawing.Point(28, 129);
            this.current_label.Name = "current_label";
            this.current_label.Size = new System.Drawing.Size(97, 18);
            this.current_label.TabIndex = 11;
            this.current_label.Text = "Current Slide:";
            // 
            // current_slide_label
            // 
            this.current_slide_label.AutoSize = true;
            this.current_slide_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_slide_label.Location = new System.Drawing.Point(132, 129);
            this.current_slide_label.Name = "current_slide_label";
            this.current_slide_label.Size = new System.Drawing.Size(0, 20);
            this.current_slide_label.TabIndex = 12;
            // 
            // message_label
            // 
            this.message_label.AutoSize = true;
            this.message_label.Location = new System.Drawing.Point(28, 177);
            this.message_label.Name = "message_label";
            this.message_label.Size = new System.Drawing.Size(0, 13);
            this.message_label.TabIndex = 13;
            // 
            // thumbs_panel
            // 
            this.thumbs_panel.AutoScroll = true;
            this.thumbs_panel.Location = new System.Drawing.Point(12, 242);
            this.thumbs_panel.Name = "thumbs_panel";
            this.thumbs_panel.Size = new System.Drawing.Size(903, 340);
            this.thumbs_panel.TabIndex = 15;
            this.thumbs_panel.Click += new System.EventHandler(this.picture_click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 641);
            this.Controls.Add(this.thumbs_panel);
            this.Controls.Add(this.message_label);
            this.Controls.Add(this.current_slide_label);
            this.Controls.Add(this.current_label);
            this.Controls.Add(this.save_slide_butt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.num_slides_in);
            this.Controls.Add(this.num_slides_butt);
            this.Controls.Add(this.slide_selector);
            this.Controls.Add(this.find_slide_images);
            this.Controls.Add(this.gen_slide_button);
            this.Controls.Add(this.text_in_label);
            this.Controls.Add(this.text_input);
            this.Controls.Add(this.title_in_label);
            this.Controls.Add(this.title_input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox title_input;
        private System.Windows.Forms.Label title_in_label;
        private System.Windows.Forms.TextBox text_input;
        private System.Windows.Forms.Label text_in_label;
        private System.Windows.Forms.Button gen_slide_button;
        private System.Windows.Forms.Button find_slide_images;
        private System.Windows.Forms.ListBox slide_selector;
        private System.Windows.Forms.Button num_slides_butt;
        private System.Windows.Forms.TextBox num_slides_in;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button save_slide_butt;
        private System.Windows.Forms.Label current_label;
        private System.Windows.Forms.Label current_slide_label;
        private System.Windows.Forms.Label message_label;
        private System.Windows.Forms.FlowLayoutPanel thumbs_panel;
    }
}

