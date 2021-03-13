using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class _SaveSystem
{
    private static string path = Application.persistentDataPath + "/gameConfig.umx";
    public static void SaveOptionsMenu(SaveOptionsData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveOptionsData dataToSave = new SaveOptionsData(data);

        formatter.Serialize(stream, dataToSave);
        stream.Close();
    }

    public static SaveOptionsData LoadOptionsMenu()
    {
        if (DoesFileExists())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveOptionsData data = formatter.Deserialize(stream) as SaveOptionsData;
            stream.Close();

            return data;
        } else
        {
            saveDefaultGameValues();
            return GetDefaultGameConfigData();
        }
    }

    public static void saveDefaultGameValues()
    {
        if(DoesFileExists() == false)
        {
            SaveOptionsMenu(GetDefaultGameConfigData());
        }
    }

    private static bool DoesFileExists()
    {
        return File.Exists(path);
    }

    private static SaveOptionsData GetDefaultGameConfigData()
    {
        SaveOptionsData dataToSave = new SaveOptionsData(null);
        dataToSave.musicVolume = 100f;

        return dataToSave;
    }
}
