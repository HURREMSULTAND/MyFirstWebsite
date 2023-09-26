using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restore.Entities
{
    public class CustomerReview
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Comment { get; set; }
        public string ImagePath { get; set; }


    }
}