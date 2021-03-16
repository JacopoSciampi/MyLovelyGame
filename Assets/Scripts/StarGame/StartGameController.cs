using TMPro;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class StartGameController : MonoBehaviour
{
    [Header("Required")]
    public Texture2D defaultCursor;

    private string basePath;

    private void Awake()
    {
        WorldManager.__INIT__();

        basePath = Application.persistentDataPath;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        _upsertNeededDirectories();
    }

    private void _upsertNeededDirectories()
    {
        if (!Directory.Exists(basePath + "/SavedData"))
        {
            Directory.CreateDirectory(basePath + "/SavedData");
        }
        if (!Directory.Exists(basePath + "/SavedData/Servers"))
        {
            Directory.CreateDirectory(basePath + "/SavedData/Servers");
        }
        if (!Directory.Exists(basePath + "/SavedData/Characters"))
        {
            Directory.CreateDirectory(basePath + "/SavedData/Characters");
        }
    }
}
