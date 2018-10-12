using System;
using System.Collections.Generic;
using System.Text;

namespace FSS.Data.Models
{
    public class PagedRequestDto
    {
        public List<FilterDto> Filters { get; set; }
        public string OrderBy { get; set; }
        public string OrderDir { get; set; }
        public int PageNo { get; set;} 
        public int PageLen { get; set; }
    }
}
