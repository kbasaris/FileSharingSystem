using System;
using System.Collections.Generic;
using System.Text;

namespace FSS.Data.Models
{
    public class FileRepsonseDto : ResponseDto
    {
        public double FileLength { get; set; }
        public string FileName { get; set; }

    }
}
