using System.ComponentModel;

namespace HolidaysApi.Helper.Enums
{
    public enum HolidayType
    {
        [Description("Municipal")]
        Municipal,
        [Description("Estadual")]
        Estadual,
        [Description("Nacional")]
        Nacional
    }
}
