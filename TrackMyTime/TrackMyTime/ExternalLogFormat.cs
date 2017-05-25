using System.ComponentModel;

namespace TrackMyTime
{
    public enum ExternalLogFormat
    {
        [Description("XXXh:XXm")]
        HourMinute = 0,
        [Description("XXXd:XXh:XXm")]
        DayHourMinute = 1
    }
}