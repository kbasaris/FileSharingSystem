using System;
using System.Collections.Generic;
using System.Text;

namespace FSS.Data.Models
{
    public class ResponseDto
    {
        public StatusEnum Status { get; set; }
        public string Message { get; set; }
    }
}
