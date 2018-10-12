using System;
using System.Collections.Generic;
using System.Text;

namespace FSS.Data.Models
{
    public class FilterDto
    {
            public string PropertyName { get; set; }
          
            public Operation Operation { get; set; }
          
            public object Value { get; set; }

            public string ComparisonRule { get; set; }
    }
}
