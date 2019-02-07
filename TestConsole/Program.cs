using ImageCompare;
using ImageCompare.DTOs;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Images>
            {
                new Images
                {
                    ImageA = @"C:/difftest/11.bmp",
                    ImageB = @"C:/difftest/12.bmp"
                },
                new Images
                {
                    ImageA = @"C:/difftest/21.bmp",
                    ImageB = @"C:/difftest/22.bmp"
                },
                new Images
                {
                    ImageA = @"C:/difftest/31.bmp",
                    ImageB = @"C:/difftest/32.bmp"
                }
            };
            var cfg = Config.GetDefault();
            cfg.OutputPath = "C:\\difftest";
            var t = new CMK.ImageCompare(cfg);
            t.Compare(list);
        }
    }
}
