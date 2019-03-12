# PowerpointSlideGenerator

This project anables you to create a PowerPoint with any number of slides and each of those slides will have a title, a text area, and a picture. The app utilizes the [ContextualWebSearch API](https://rapidapi.com/contextualwebsearch/api/web-search "Contextual Web Search API (RapidAPI.com)") handled by [RapidAPI.com](https://rapidapi.com "RapidAPI.com") to search for the web for images based on text entered into the text inputs for both the title and the text area. Url's from the images returned are then used to create pictureboxes for selection in a FlowLayoutPanel. You can then select an image and the information for the image and the two text areas will first be saved to a csv file. Once ready the entire PowerPoint file can be created. Each slide has a 2 column layout where the title is on top and the text is on the left and the picture on the right. The config file stores the api key, image return limits, and words that are considered extranneous when creating a search query.

The project could be improved to allow text formatting for fonts and sizes. The highlighting of the picture thumbnails also needs to be improved so that if a different picture is selected it would reset the border on the previous image. I would also liek to improve the user interface overall however the solution is at least functional at the moment.


![alt text](https://github.com/ggroenendale/powerpointslidegenerator/exampleimages/example1.PNG "User Interface")

![alt text](https://github.com/ggroenendale/powerpointslidegenerator/exampleimages/example2.PNG "Finished Powerpoint")

