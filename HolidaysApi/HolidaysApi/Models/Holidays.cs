using HolidaysApi.Helper;
using HolidaysApi.Helper.Enums;
using System;

namespace HolidaysApi.Models
{
    public class Holidays
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }

        private string _type;
        public string Type
        {
            get { return _type; }
            set => _type = ((HolidayType)Int32.Parse(value)).GetDescription();
        }
    }
}
