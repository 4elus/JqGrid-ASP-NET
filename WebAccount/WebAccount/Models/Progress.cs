using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAccount.Models
{
    public class Progress
    {
        public int Id { get; set; }
        public int Student_Id { get; set; }
        public string Activity { get; set; }
        public string Descrption { get; set; }
    }
}