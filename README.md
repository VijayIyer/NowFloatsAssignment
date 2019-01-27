# NowFloatsAssignment
Small project containing code for given assignment

Whole code is in Program.cs file inside DownloadImageAssignment Folder

Downloading image - done using Webclient.DownloadFile method

Part 1 - Splitting image 

For this bitmap.clone method overload taking a rectangle and pixelformat as arguements was used. 
Logic of splitting image consists of asking the user the number of subimages, then dividing the downloaded image width by n. 


Part 2  - Combining Image

For this purpose Graphics class was used. 
Using Graphics.drawimage , whole image is drawn by individually drawing subimages, then image is stored in a new file , this getting back the original image. 

