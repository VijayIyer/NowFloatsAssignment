using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace DownloadImageAssignment
{
    class Program
    {
        static void Main(string[] args)
        { 
            Console.WriteLine("Enter URL - ");
            string uri = Console.ReadLine();
            Console.WriteLine("\nEnter path to save downloaded image");
            string downloadpath = Console.ReadLine();

            #region downloading image
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFile(new Uri(uri), downloadpath);
                }
                catch (WebException)
                {
                    Console.WriteLine("Error occured while downloading the file.");
                }

                Bitmap bmp = new Bitmap(downloadpath);
                #endregion

            #region Logic for splitting images

                Console.WriteLine("How many subimages do you want?");
                int subimages = Convert.ToInt32(Console.ReadLine());

                Bitmap[] newimages = new Bitmap[subimages];
                Console.WriteLine("\nEnter format for new image e.g. - (Path)\\Image1 for 1st image , (Path)\\Image2 for 2nd image(different directory from downloaded image) - ");
                string imagepath = Console.ReadLine();


                for (int i=0;i<subimages;i++)
                {
                    Rectangle rec = new Rectangle(i*(bmp.Width/subimages), 0, bmp.Width / subimages, bmp.Height);
                    newimages[i] =  bmp.Clone(rec,bmp.PixelFormat);
                    string filename = string.Concat(imagepath, i,".bmp");
                    newimages[i].Save(filename,ImageFormat.Bmp);
                    
                }
                #endregion


                #region Logic for combining images

                DirectoryInfo directory = new DirectoryInfo(downloadpath);
                if (directory != null)
                {
                    FileInfo[] files = directory.GetFiles();
                    CombineImages(files);
                }

                #endregion
            }

        }

       
        private static void CombineImages(FileInfo[] files)
        {
            Console.WriteLine("Enter Path for saving new combined image");
            string finalImage = Console.ReadLine();
            List<int> imageHeights = new List<int>();
            int nIndex = 0;
            int width = 0;
            int height = 0;
            foreach (FileInfo file in files)
            {
                Image img = Image.FromFile(file.FullName);

                height = img.Height;
                width += img.Width;

                img.Dispose();
            }

           
            

            Bitmap img3 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img3);

            g.Clear(SystemColors.AppWorkspace);



            foreach (FileInfo file in files)
            {
                Image img = Image.FromFile(file.FullName);
                if (nIndex == 0)
                {
                    g.DrawImage(img, new Point(0, 0));
                    nIndex++;
                    width = img.Width;
                }
                else
                {
                    g.DrawImage(img, new Point(width, 0));

                    width += img.Width;
                }
                img.Dispose();
            }

            g.Dispose();

            img3.Save(finalImage,ImageFormat.Bmp);
            Console.WriteLine("Reformed image saved in location  - {0}",finalImage);
            img3.Dispose();
          

         //   imageLocation.Image = Image.FromFile(finalImage);
        }
    }
}
