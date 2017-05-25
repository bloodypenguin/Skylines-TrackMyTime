using ICities;
using UnityEngine;

namespace TrackMyTime
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public static bool inGame = false;

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (mode != LoadMode.NewGame && mode != LoadMode.NewGameFromScenario && mode != LoadMode.LoadGame)
            {
                return;
            }
            inGame = true;
            var go = GameObject.Find("TrackMyTime");
            if (go != null)
            {
                return;
            }
            new GameObject("TrackMyTime").AddComponent<TimeTracker>();

        }

        public override void OnLevelUnloading()
        {
            inGame = false;
            base.OnLevelUnloading();
            var go = GameObject.Find("TrackMyTime");
            if (go == null)
            {
                return;
            }
            GameObject.Destroy(go);
        }
    }
}