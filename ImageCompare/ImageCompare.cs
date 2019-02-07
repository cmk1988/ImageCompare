using ImageCompare;
using ImageCompare.DTOs;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace CMK
{
    public class ImageCompare
    {
        Config config;

        public ImageCompare()
        {
            config = Config.GetDefault();
        }

        public ImageCompare(Config config)
        {
            this.config = config ?? Config.GetDefault();
        }

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
            list.Add(File.ReadAllText($"{config.TemplatePath}\\{config.ClientScript}"));
            int i = 1;
            foreach (var image in images)
            {
                var diffimage = Compare(image.ImageA, image.ImageB, i);
                list.Add(HtmlCreator.GetHtmlSnippet(diffimage, i, config));
                i++;
            }
            return list;
        }

        public Images Compare(string image1, string image2, int i)
        {
            using (var imagea = image1.StartsWith("http") ? ImageLoader.FromUrl(image1) : Image.FromFile(image1))
            using (var imageb = image2.StartsWith("http") ? ImageLoader.FromUrl(image2) : Image.FromFile(image2))
            {
                var test = CompareEngine.GetDiff2((Bitmap)imagea, (Bitmap)imageb);
                test.Save($"{config.OutputPath}\\{config.ImageFileName}{i}.bmp");
                return new Images
                {
                    ImageA = image1,
                    ImageB = image2,
                    ImageDiff = $"{config.OutputPath}\\{config.ImageFileName}{i}.bmp"
                };
            }
        }
    }
}
