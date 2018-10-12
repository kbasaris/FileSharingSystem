using System;
using System.Collections.Generic;
using System.Text;

namespace FSS.Data.DbObjects
{
    public class File : IEntityBase
    {
        public File(){}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
       
    }
}
