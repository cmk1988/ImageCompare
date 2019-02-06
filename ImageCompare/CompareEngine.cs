using ImageCompare.DTOs;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageCompare
{
    public class CompareEngine
    {
        public static IEnumerable<Difference> GetDiff(Bitmap a, Bitmap b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                yield break;
            List<Position> list = new List<Position>();
            for (int x = 0; x < a.Width; x++)
            {
                for(int y = 0; y < a.Height; y++)
                {
                    var p1 = a.GetPixel(x, y);
                    var p2 = b.GetPixel(x, y);
                    if (p1 != p2)
                        list.Add(new Position(x, a.Height - y));
                }
            }
            List<List<Position>> groups = new List<List<Position>>();
            while(list.Count > 0)
            {
                var p1 = list.First();
                list.Remove(p1);
                var last = p1;
                var group = list.Where(x =>
                {
                    var bo = x.NextTo(last, 20);
                    if (bo)
                        last = x;
                    return bo;
                }).ToList();
                foreach (var g in group)
                    list.Remove(g);
                groups.Add(group);
            }

            foreach (var group in groups)
            {
                var xmax = group.Select(x => x.X).Max();
                var xmin = group.Select(x => x.X).Min();
                var ymax = group.Select(x => x.Y).Max();
                var ymin = group.Select(x => x.Y).Min();
                yield return new Difference
                {
                    From = new Position(xmin, ymin),
                    To = new Position(xmax - xmin, ymax - ymin),
                };
            }
        }

        public static Bitmap GetDiff2(Bitmap a, Bitmap b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                return null;
            var img = new Bitmap(b);
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    var p1 = a.GetPixel(x, y);
                    var p2 = b.GetPixel(x, y);
                    if (p1 != p2)
                        img.SetPixel(x, y, Color.Red);
                }
            }
            return img;
        }
    }
}
