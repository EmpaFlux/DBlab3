using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3DB.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public List<string> Categories { get; set; }

    }
}
