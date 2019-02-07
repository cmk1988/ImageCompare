using ImageCompare;
using ImageCompare.DTOs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CMK
{
    public class ImageCompare
    {
        Config config;
        HtmlCreator htmlCreator;

        public ImageCompare()
        {
            config = Config.GetDefault();
            htmlCreator = new HtmlCreator(config);
        }

        public ImageCompare(Config config)
        {
            this.config = config ?? Config.GetDefault();
            htmlCreator = new HtmlCreator(config);
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
            htmlCreator.Create($"{config.OutputPath}//{config.OutputFileName}", list);
            Process.Start($"{config.OutputPath}//{config.OutputFileName}");
        }
        public List<string> CompareToList(List<Images> images)
        {
            var list = new List<string>();
            list.Add(File.ReadAllText($"{config.TemplatePath}\\{config.ClientScript}"));
            int i = 1;
            foreach (var image in images)
            {
                var diffimage = Compare(image.ImageA, image.ImageB, i);
                list.Add(htmlCreator.GetHtmlSnippet(diffimage, i));
                i++;
            }
            return list;
        }

        private string convertImage(Image image, int i, string name)
        {
            if (image.RawFormat == ImageFormat.Tiff)
            {
                image.Save($"converted{name}{i}.bmp", ImageFormat.Bmp);
                return $"converted{name}{i}.bmp";
            }
            return null;
        }

        private Image loadImage(string str)
        {
            return str.StartsWith("http") ? ImageLoader.FromUrl(str) : Image.FromFile(str);
        }

        public Images Compare(string image1, string image2, int i)
        {
            using (var imagea = loadImage(image1))
            using (var imageb = loadImage(image2))
            {
                if(config.ConvertTiffToBmp)
                {
                    image1 = convertImage(imagea, i, "A") ?? image1;
                    image2 = convertImage(imagea, i, "B") ?? image2;
                }
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
