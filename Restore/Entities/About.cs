using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restore.Entities
{
    public class About
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

        public string JobTitle { get; set; }
        public string Description { get; set; }

        public string ImagePath { get; set; }

    }
}