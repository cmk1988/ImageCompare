using ImageCompare;
using ImageCompare.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMK
{
    public class ImageCompare
    {
        public void Compare(List<Images> images)
        {
            var list = new List<Images>();
            int i = 1;
            foreach (var image in images)
            {
                list.Add(Compare(image.ImageA, image.ImageB, i));
                i++;
            }
            HtmlCreator.Create("index.html",list);
            System.Diagnostics.Process.Start("index.html");
        }

        public Images Compare(string image1, string image2, int i)
        {
            using (var imagea = Image.FromFile(image1))
            using (var imageb = Image.FromFile(image2))
            {
                var test = CompareEngine.GetDiff2((Bitmap)imagea, (Bitmap)imageb);
                test.Save($"test{i}.bmp");
                return new Images
                {
                    ImageA = image1,
                    ImageB = image2,
                    ImageDiff = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\\test{i}.bmp"
                };
            }
        }
    }
}
