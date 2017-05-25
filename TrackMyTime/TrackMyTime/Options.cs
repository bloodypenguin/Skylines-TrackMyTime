using System;
using System.Xml.Serialization;
using TrackMyTime.OptionsFramework.Attibutes;

namespace TrackMyTime
{
    [Options("TrackMyTime")]
    public class Options
    {
        private const string LOG_FILE_OPTIONS = "Log file options";

        [Label("...", "Time spent in current city")]
        [XmlIgnore]
        public object Label { get; set; }
        
        [Textfield("Log file location (default: TrackMyTime.log in game's root folder)", LOG_FILE_OPTIONS)]
        public string LogFileLocation { get; set; } = "TrackMyTime.log";

        [Slider("How ofter log entries, seconds (default: 1)", 1, 3600, 1, LOG_FILE_OPTIONS)]
        public int LogIntervalSecs { get; set; } = 1;

        [Textfield("Timestamp format (default: M/d/yyyy h:mm:ss tt)", LOG_FILE_OPTIONS)]
        public string DateTimeFormat { get; set; } = "M/d/yyyy h:mm:ss tt";

        [Checkbox("Log city name (default: true)", LOG_FILE_OPTIONS)]
        public bool LogCityName { get; set; } = true;
    }
}