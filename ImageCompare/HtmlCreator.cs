using ImageCompare.DTOs;
using System.Collections.Generic;
using System.IO;

namespace ImageCompare
{
    public class HtmlCreator
    {
        public static void Create(string filePath, IEnumerable<Images> images, Config config = null)
        {
            config = config ?? Config.GetDefault();
            var str = File.ReadAllText($"{config.TemplatePath}\\{config.IndexTemplate}");
            var script = File.ReadAllText($"{config.TemplatePath}\\{config.ClientScript}");
            str = str.Replace(config.Placeholder_IndexTemplate_Script, script);
            var div = File.ReadAllText($"{config.TemplatePath}\\{config.AddRemoveImageTemplate}");
            var rows = "";
            var id = 0;
            foreach (var img in images)
            {
                rows += div
                    .Replace(config.Placeholder_AddRemoveImageTemplate_UrlA, img.ImageA.Replace("\\", "/"))
                    .Replace(config.Placeholder_AddRemoveImageTemplate_UrlB, img.ImageB.Replace("\\", "/"))
                    .Replace(config.Placeholder_AddRemoveImageTemplate_UrlD, img.ImageDiff.Replace("\\","/"))
                    .Replace(config.Placeholder_AddRemoveImageTemplate_Id, id.ToString());
                id++;
            }
            str = str.Replace("{{list}}", rows);
            File.WriteAllText(filePath, str);
        }

        public static string GetHtmlSnippet(Images img, int i, Config config = null)
        {
            config = config ?? Config.GetDefault();
            var div = File.ReadAllText($"{config.TemplatePath}\\{config.AddRemoveImageTemplate}");
            return div
                    .Replace(config.Placeholder_AddRemoveImageTemplate_UrlA, img.ImageA.Replace("\\", "/"))
                    .Replace(config.Placeholder_AddRemoveImageTemplate_UrlB, img.ImageB.Replace("\\", "/"))
                    .Replace(config.Placeholder_AddRemoveImageTemplate_UrlD, img.ImageDiff.Replace("\\", "/"))
                    .Replace(config.Placeholder_AddRemoveImageTemplate_Id, i.ToString());
        }
    }
}
