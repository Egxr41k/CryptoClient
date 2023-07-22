using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartsTest
{
    public class ViewModel
    {
        public Dictionary<string, int> Data { get; set; }

        public ViewModel()
        {
            Data = new()
            {
                { "David",  180 },
                {"Michael", 170 },
                { "Steve", 160 },
                {"Joel", 182 }
            };
        }
    }
}
