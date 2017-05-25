using System.Linq;
using System.Runtime.CompilerServices;
using ColossalFramework.UI;
using ICities;
using TrackMyTime.OptionsFramework.Extensions;

namespace TrackMyTime
{
    public class Mod : IUserMod
    {
        private static UILabel _spentTimeLabel;


        public string Name => "Track My Time";

        public string Description => "Tracks Time Spent In Game";

        public void OnSettingsUI(UIHelperBase helper)
        {
            var components = helper.AddOptionsGroup<Options>();
            var labels = components.OfType<UILabel>().ToArray();
            if (labels.Length > 0)
            {
                _spentTimeLabel = labels[0];
            }
        }

        public static void UpdateLabelText(string text)
        {
            if (_spentTimeLabel == null)
            {
                return;
            }
            _spentTimeLabel.text = text;
        }
    }
}
