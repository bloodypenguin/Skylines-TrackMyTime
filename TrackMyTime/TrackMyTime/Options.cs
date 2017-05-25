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

        [Checkbox("Output time to log (default: false)", LOG_FILE_OPTIONS)]
        public bool OutputEnabled { get; set; } = false;

        [Textfield("Log file location (default: TrackMyTime.log in game's root folder)", LOG_FILE_OPTIONS)]
        public string LogFileLocation { get; set; } = "TrackMyTime.log";

        [Slider("How often overwrite log, seconds (default: 1)", 1, 3600, 1, LOG_FILE_OPTIONS)]
        public int LogIntervalSecs { get; set; } = 1;

        [DropDown("Log format", nameof(ExternalLogFormat), LOG_FILE_OPTIONS)]
        public int LogFormat { get; set; } = 0;
    }
}