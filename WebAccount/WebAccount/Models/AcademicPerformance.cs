using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAccount.Models
{
    public class AcademicPerformance
    {
        public int Id { get; set; }
        public int Student_Id { get; set; }
        public int Semester { get; set; }
        public string Name_Sub { get; set; }
        public int Mark { get; set; }
    }
}