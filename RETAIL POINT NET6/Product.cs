using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RETAIL_POINT_NET6
{
    public class Product
    {
        public string Id { get; set; }

        public string Character { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Brush BgColor { get; set; }
    }

}
