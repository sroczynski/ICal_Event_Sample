using Calendar.Data;
using Calendar.Models;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Interfaces.DataTypes;
using Ical.Net.Serialization;
using Ical.Net.Serialization.iCalendar.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Calendar.Utils
{
    public static class Calendar
    {
        public static MemoryStream GenerateICalendar(CalendarModel model) 
        {
            if (model.FileType.Equals(FileTypeEnum.csv))
            {
                return GenerateCSV(model);
            }
            return GenerateICS(model);

        }

        private static MemoryStream GenerateCSV(CalendarModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Event Name;Event Description;Event Venue;Event Start Date;Event Start Time;Event End Date;Event End Time");
            sb.Append(string.Concat(model.Title,";"));
            sb.Append(string.Concat(model.Description, ";"));
            sb.Append(string.Concat(model.Local, ";"));
            sb.Append(string.Concat(model.Date.ToString("yyyy-MM-dd"),";"));
            sb.Append(string.Concat(model.Date.ToString("HH:mm"), ";"));
            
            var endEvent = model.Date.AddHours(2);
            sb.Append(string.Concat(endEvent.ToString("yyyy-MM-dd"), ";"));
            sb.Append(endEvent.ToString("HH:mm"));

            var calendarBytes = System.Text.Encoding.Unicode.GetBytes(sb.ToString());

            MemoryStream msCal = new MemoryStream(calendarBytes, 0, calendarBytes.Length);

            return msCal;
        }
        public static MemoryStream GenerateICS(CalendarModel model)
        {
            var evento = new Event
            {   
                Summary = model.Title,
                Location = model.Local,
                Description = model.Description,
                DtStart = new CalDateTime(model.Date),
                DtEnd = new CalDateTime(model.Date.AddHours(2))
            };

            var calendar = new Ical.Net.Calendar();
            calendar.Events.Add(evento);
            calendar.Events.Add(evento);

            
            var serializer = new CalendarSerializer(new SerializationContext());
            var serializedCalendar = serializer.SerializeToString(calendar);
            var calBytes = System.Text.Encoding.Unicode.GetBytes(serializedCalendar);

            MemoryStream msCal = new MemoryStream(calBytes, 0, calBytes.Length);

            return msCal;
        }
    }
}