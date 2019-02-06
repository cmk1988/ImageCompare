﻿using ImageCompare.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompare
{
    public class HtmlCreator
    {
        public static void Create(string filePath, IEnumerable<Images> images)
        {
            var str = File.ReadAllText("Templates\\Index.html");
            var script = File.ReadAllText("Templates\\WebClientScript.js");
            str = str.Replace("{{script}}", script);
            var div = File.ReadAllText("Templates\\AddRemoveImages.html");
            var rows = "";
            var id = 0;
            foreach (var img in images)
            {
                rows += div
                    .Replace("{{urla}}", img.ImageA)
                    .Replace("{{urlb}}", img.ImageB)
                    .Replace("{{urld}}", img.ImageDiff.Replace("\\","/"))
                    .Replace("{{id}}", id.ToString());
                id++;
            }
            str = str.Replace("{{list}}", rows);
            File.WriteAllText(filePath, str);
        }

        public static string GetHtmlSnippet(string image1, string image2, string image3, int i)
        {
            var div = File.ReadAllText("Templates\\AddRemoveImages.html");
            return div
                    .Replace("{{urla}}", image1)
                    .Replace("{{urlb}}", image2)
                    .Replace("{{urld}}", image3.Replace("\\", "/"))
                    .Replace("{{id}}", i.ToString());
        }
    }
}
