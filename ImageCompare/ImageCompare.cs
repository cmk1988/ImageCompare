using ImageCompare;
using ImageCompare.DTOs;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
        public List<string> CompareToList(List<Images> images)
        {
            var list = new List<string>();
            list.Add(File.ReadAllText("Templates\\WebClientScript.js"));
            int i = 1;
            foreach (var image in images)
            {
                var diffimage = Compare(image.ImageA, image.ImageB, i);
                list.Add(HtmlCreator.GetHtmlSnippet(image.ImageA, image.ImageB, diffimage.ImageDiff, i));
                i++;
            }
            return list;
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
