using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class StartGameButtonsController : MonoBehaviour
{
    [Header("**Required**")]
    public Button deleteCharacter;
    public Button newCharacter;
    public Button loadCharacter;
    public Button undoModalCreateNewCharacter;
    public Button confirmModalCreateNewCharacter;
    public Button addServerButton;
    public Button cancelCreateNewServer;
    public Button confirmCreateNewServer;

    [Header("TO BE REWORKED")]
    public Button playServerButton;

    [Header("**Required GameObjects**")]
    public GameObject modalCreateNewCharacter;
    public GameObject modalCreateNewServer;

    [Header("**Required extra component**")]
    public Texture2D defaultMouseCursor;
    public TMP_Dropdown DropdownClassList;
    public Text CharacterNameText;
    public Text ServerNameText;
    public TextMeshProUGUI ClassDescriptionText;
    public TextMeshProUGUI createNewCharacterFooterErrorMessage;
    public TextMeshProUGUI createNewServerFooterErrorMessage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterClassText;
    public TextMeshProUGUI footerMessageText;

    private string pathStartGameConfigUmx;
    private string basePath;
    private int classIdSelected;
    private string classNameSelected;
    private string currentNameCharacterSelected;
    private List<StartGameServerList> serverList { get; set; }
    private List<CharacterSavedData> characterList { get; set; }

    private void Start()
    {
        classIdSelected = 0; // Mage
        classNameSelected = WorldManager.GetTranslation("class_mage");
        basePath = Application.persistentDataPath;
        pathStartGameConfigUmx = basePath + "/SavedData/startGameConfig.umx";

        newCharacter.onClick.AddListener(delegate { _onNewCharacterClick(); });
        undoModalCreateNewCharacter.onClick.AddListener(delegate { _onUndoModalCreateNewCharacterClick(); });
        confirmModalCreateNewCharacter.onClick.AddListener(delegate { _onCreateNewCharacterCreateNew(); });

        ClassDescriptionText.text = WorldManager.GetTranslation("class_mage_desc");
        DropdownClassList.onValueChanged.AddListener(delegate { _onDropdownClassListValueChanged(DropdownClassList.value); });

        _readConfigFile();

        addServerButton.onClick.AddListener(delegate { OnAddServerButtonClicked(); });
        confirmCreateNewServer.onClick.AddListener(delegate { onCofirmCreateNewServer(); });
        cancelCreateNewServer.onClick.AddListener(delegate { onCancelCreateNewServer(); });

        playServerButton.onClick.AddListener(delegate { onPlayServerButtonToFix(); });
    }

    private void onPlayServerButtonToFix()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void onCofirmCreateNewServer()
    {
        createNewServerFooterErrorMessage.text = "";
        string fileName = basePath + "/SavedData/Servers/" + ServerNameText.text + ".umx";

        if (ServerNameText.text.Length < 5)
        {
            createNewServerFooterErrorMessage.text = WorldManager.GetTranslation("invalid_name");
        }
        else if (File.Exists(fileName))
        {
            createNewServerFooterErrorMessage.text = WorldManager.GetTranslation("server_already_exists");
        }
        else
        {
            // Save server
            StartGameServerList dataToStore = new StartGameServerList(null);
            dataToStore.serverName = ServerNameText.text;
            dataToStore.characterList = new List<CharacterSavedData>();
            dataToStore.characterInfoList = new List<StartGameServerCharacterInfo>();

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(fileName, FileMode.Create);

            formatter.Serialize(stream, dataToStore);
            stream.Close();

            WorldManager.setCurrentServerSelected(dataToStore);

            // Update default player selected when entering this scene
            // server list. add -> characterList.Add(dataToStore);
            serverList.Add(dataToStore);

            formatter = new BinaryFormatter();
            stream = new FileStream(pathStartGameConfigUmx, FileMode.Create);

            StartGameSavedDataList dataToSave = new StartGameSavedDataList(null);
            dataToSave.characterList = characterList;
            dataToSave.serverList = serverList;
            dataToSave.currentPlayerName = currentNameCharacterSelected;

            formatter.Serialize(stream, dataToSave);
            stream.Close();

            //Update UI
            _setupUiByData(dataToSave);
            onCancelCreateNewServer();
        }
    }

    private void OnAddServerButtonClicked()
    {
        modalCreateNewServer.SetActive(true);
    }

    private void onCancelCreateNewServer()
    {
        modalCreateNewServer.SetActive(false);
        Cursor.SetCursor(defaultMouseCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

        private void _readConfigFile()
    {
        if (File.Exists(pathStartGameConfigUmx))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathStartGameConfigUmx, FileMode.Open);

            _setupUiByData(formatter.Deserialize(stream) as StartGameSavedDataList);
            stream.Close();
        }
        else
        {
            serverList = new List<StartGameServerList>();
            characterList = new List<CharacterSavedData>();

            footerMessageText.color = WorldManager.hintColor;
            footerMessageText.text = WorldManager.GetTranslation("HINT_no_pg_at_start");
        }
    }

    private void _setupUiByData(StartGameSavedDataList data)
    {
        if (data.serverList != null)
        {
            serverList = data.serverList;
            //_populateScrollViewServerList();
        }
        if (data.characterList != null)
        {
            characterList = data.characterList;
        }

        if (data.currentPlayerName == null)
        {
            footerMessageText.color = WorldManager.hintColor;
            footerMessageText.text = WorldManager.GetTranslation("HINT_no_pg_at_start");
        }
        else
        {
            string fileName = basePath + "/SavedData/Characters/" + data.currentPlayerName + ".umx";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(fileName, FileMode.Open);

            CharacterSavedData storedData = formatter.Deserialize(stream) as CharacterSavedData;

            currentNameCharacterSelected = storedData.name;
            characterNameText.text = storedData.name;
            characterClassText.text = WorldManager.GetTranslation(storedData.className);
        }
    }

    private void _onCreateNewCharacterCreateNew()
    {
        createNewCharacterFooterErrorMessage.text = "";
        string fileName = basePath + "/SavedData/Characters/" + CharacterNameText.text + ".umx";

        if (CharacterNameText.text.Length < 5)
        {
            createNewCharacterFooterErrorMessage.text = WorldManager.GetTranslation("invalid_name");
        }
        else if (File.Exists(fileName))
        {
            createNewCharacterFooterErrorMessage.text = WorldManager.GetTranslation("character_already_exists");
        }
        else
        {
            // Save character
            CharacterSavedData dataToStore = new CharacterSavedData(null);
            dataToStore.name = CharacterNameText.text;
            dataToStore.classId = classIdSelected;
            dataToStore.className = classNameSelected;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(fileName, FileMode.Create);

            formatter.Serialize(stream, dataToStore);
            stream.Close();

            WorldManager.setCurrentCharactedSelected(dataToStore);

            // Update default player selected when entering this scene
            characterList.Add(dataToStore);

            formatter = new BinaryFormatter();
            stream = new FileStream(pathStartGameConfigUmx, FileMode.Create);

            StartGameSavedDataList dataToSave = new StartGameSavedDataList(null);
            dataToSave.characterList = characterList;
            dataToSave.serverList = serverList;
            dataToSave.currentPlayerName = dataToStore.name;

            currentNameCharacterSelected = dataToStore.name;
            formatter.Serialize(stream, dataToSave);
            stream.Close();

            //Update UI
            _setupUiByData(dataToSave);
            _onUndoModalCreateNewCharacterClick();
        }
    }

    private void _onDropdownClassListValueChanged(int value)
    {
        classIdSelected = value;
        classNameSelected = WorldManager.classListDropdownData[classIdSelected];

        ClassDescriptionText.text = WorldManager.GetTranslation(classNameSelected.ToLower() + "_desc");
    }

    private void _onNewCharacterClick()
    {
        modalCreateNewCharacter.SetActive(true);
    }

    private void _onUndoModalCreateNewCharacterClick()
    {
        modalCreateNewCharacter.SetActive(false);
        Cursor.SetCursor(defaultMouseCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
