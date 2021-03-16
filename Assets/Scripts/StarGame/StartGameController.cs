using TMPro;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class StartGameController : MonoBehaviour
{
    [Header("Required")]
    public Texture2D defaultCursor;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterClassText;
    public TextMeshProUGUI footerMessageText;

    private string basePath;

    private void Awake()
    {
        WorldManager.__INIT__();

        basePath = Application.persistentDataPath;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        _upsertNeededDirectories();
        _readConfigFile();
    }

    private void _readConfigFile()
    {
        string path = basePath + "/savedData/startGameConfig.umx";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            _setupUiByData(formatter.Deserialize(stream) as StartGameSavedData);
            stream.Close();
        }
        else
        {
            footerMessageText.color = WorldManager.hintColor;
            footerMessageText.text = WorldManager.GetTranslation("HINT_no_pg_at_start");
        }
    }

    private void _setupUiByData(StartGameSavedData data)
    {
        characterNameText.text = data.name;
        characterClassText.text = WorldManager.GetTranslation(data.className);
    }

    private void _upsertNeededDirectories()
    {
        if (!Directory.Exists(basePath + "/savedData"))
        {
            Directory.CreateDirectory(basePath + "/savedData");
        }
    }
}
