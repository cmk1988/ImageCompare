using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompare
{
    public class ImageLoader
    {

        public static Image FromUrl(string url)
        {
            using (var wc = new WebClient())
            {
                return FromUrl(url, wc);
            }
        }
        public static Image FromUrl(string url, WebClient wc)
        {
            var data = wc.DownloadData(url);
            using (var stream = new MemoryStream(data))
            {
                return Image.FromStream(stream);
            }
        }
    }
}
