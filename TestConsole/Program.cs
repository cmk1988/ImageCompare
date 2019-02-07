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
                    ImageA = @"C:/testbilder/11.bmp",
                    ImageB = @"C:/testbilder/12.bmp"
                },
                new Images
                {
                    ImageA = @"http://cmk.bplaced.net/pictures/31.png",
                    ImageB = @"http://cmk.bplaced.net/pictures/32.png"
                }
            };
            var t = new CMK.ImageCompare();
            t.Compare(list);
            //var test = t.CompareToList(list);
        }
    }
}
