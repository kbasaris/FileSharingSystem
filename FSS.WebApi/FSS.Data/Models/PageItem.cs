using System;
using System.Collections.Generic;
using System.Text;

namespace FSS.Data.Models
{
    public class PageItem<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
    }
}
