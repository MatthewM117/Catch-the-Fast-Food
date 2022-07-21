using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{

    public static void SaveStats(ScaleObject scaleObject)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/stats.stuff";
        FileStream stream = new FileStream(filePath, FileMode.Create);

        StatsData statsData = new StatsData(scaleObject);

        formatter.Serialize(stream, statsData);
        stream.Close();
    }

    public static StatsData LoadStats()
    {
        string filePath = Application.persistentDataPath + "/stats.stuff";
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            StatsData statsData = formatter.Deserialize(stream) as StatsData;
            stream.Close();

            return statsData;
        }
        else
        {
            Debug.LogError("data file not found " + filePath);
            return null;
        }
    }
}
