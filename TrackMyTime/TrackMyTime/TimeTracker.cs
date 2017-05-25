using System;
using System.IO;
using System.Text;
using TrackMyTime.OptionsFramework;
using UnityEngine;

namespace TrackMyTime
{
    public class TimeTracker : MonoBehaviour
    {
        private int nextUpdate = 0;
        private long _lastTrackedTimeTick = -1;
        private string path;

        public void Awake()
        {
            path = OptionsWrapper<Options>.Options.LogFileLocation;
        }

        public void Update()
        {
            var utcNowTicks = DateTime.UtcNow.Ticks;
            if (_lastTrackedTimeTick < 0)
            {
                _lastTrackedTimeTick = utcNowTicks;
            }
            var diffTicks = utcNowTicks - _lastTrackedTimeTick;
            var timeSpan = TimeSpan.FromTicks(SerializableDataExtension.SpentTimeMSecs * 10000 + diffTicks);
            SerializableDataExtension.SpentTimeMSecs += diffTicks / 10000;
            var spendTimeStr = TimeSpanFormat.ToString(timeSpan);
            Mod.UpdateLabelText(spendTimeStr);

            if (OptionsWrapper<Options>.Options.OutputEnabled && Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + OptionsWrapper<Options>.Options.LogIntervalSecs;
                try
                {
                    using (var sw = new StreamWriter(path, false))
                    {
                        sw.WriteLine(TimeSpanFormat.ToString(timeSpan,
                            (ExternalLogFormat)OptionsWrapper<Options>.Options.LogFormat));
                        sw.Flush();
                    }
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
            _lastTrackedTimeTick = utcNowTicks;
        }
    }
}