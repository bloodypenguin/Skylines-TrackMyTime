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

        private StreamWriter _sw = null;


        public void Awake()
        {
            var path = OptionsWrapper<Options>.Options.LogFileLocation;
            try
            {
                _sw = File.AppendText(path);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
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
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + OptionsWrapper<Options>.Options.LogIntervalSecs;
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format($"{{0:{OptionsWrapper<Options>.Options.DateTimeFormat}}}", DateTime.Now));
                if (OptionsWrapper<Options>.Options.LogCityName)
                {
                    builder.Append(" @");
                    builder.Append(SimulationManager.instance?.m_metaData?.m_CityName);
                }
                builder.Append(": ");
                builder.Append(spendTimeStr);
                _sw?.WriteLine(builder.ToString());
                _sw?.Flush();
            }
            _lastTrackedTimeTick = utcNowTicks;
        }

        public void OnDestroy()
        {
            _sw?.Flush();
            _sw?.Close();
        }
    }
}