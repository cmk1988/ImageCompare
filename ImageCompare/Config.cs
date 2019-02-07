using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompare
{
    public class Config
    {
        public string TemplatePath { get; set; } = "Templates";
        public string IndexTemplate { get; set; } = "Index.html";
        public string AddRemoveImageTemplate { get; set; } = "AddRemoveImages.html";
        public string ClientScript { get; set; } = "WebClientScript.js";
        public string OutputPath { get; set; } = "";
        public string ImageFileName { get; set; } = "img";
        public string OutputFileName { get; set; } = "index.html";

        public bool ConvertTiffToBmp { get; set; } = true;

        // Placeholders
        public string Placeholder_IndexTemplate_Script { get; set; } = "{{script}}";
        public string Placeholder_IndexTemplate_List { get; set; } = "{{list}}";

        public string Placeholder_AddRemoveImageTemplate_UrlA { get; set; } = "{{urla}}";
        public string Placeholder_AddRemoveImageTemplate_UrlB { get; set; } = "{{urlb}}";
        public string Placeholder_AddRemoveImageTemplate_UrlD { get; set; } = "{{urld}}";
        public string Placeholder_AddRemoveImageTemplate_Id { get; set; } = "{{id}}";


        public static Config GetDefault()
        {
            return new Config();
        }
    }
}
