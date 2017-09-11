using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calendar.Models
{
    public class CalendarModel
    {
        public string Title { get; set; }
        public string Local { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Calendar.Data.FileTypeEnum FileType { get; set; }
    }
}