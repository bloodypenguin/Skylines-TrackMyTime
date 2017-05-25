using ICities;

namespace TrackMyTime
{
    public class SerializableDataExtension : SerializableDataExtensionBase
    {
        public static long SpentTimeMSecs;

        public override void OnLoadData()
        {
            base.OnLoadData();
            byte[] byteArray = serializableDataManager.LoadData("TrackMyTime");
            if (byteArray == null || byteArray.Length == 0)
            {
                SpentTimeMSecs = 0L;
                return;
            }
            string result = System.Text.Encoding.UTF8.GetString(byteArray);
            SpentTimeMSecs = long.Parse(result);
        }

        public override void OnSaveData()
        {
            base.OnSaveData();
            string str = SpentTimeMSecs.ToString();
            serializableDataManager.SaveData("TrackMyTime", System.Text.Encoding.UTF8.GetBytes(str));
        }
    }
}