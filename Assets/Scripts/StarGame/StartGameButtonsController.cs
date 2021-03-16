using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class StartGameButtonsController : MonoBehaviour
{
    [Header("**Required**")]
    public Button deleteCharacter;
    public Button newCharacter;
    public Button loadCharacter;
    public Button undoModalCreateNewCharacter;
    public Button confirmModalCreateNewCharacter;

    [Header("**Required GameObjects**")]
    public GameObject modalCreateNewCharacter;

    [Header("**Required extra component**")]
    public Texture2D defaultMouseCursor;
    public TMP_Dropdown DropdownClassList;
    public Text CharacterNameText;
    public TextMeshProUGUI ClassDescriptionText;
    public TextMeshProUGUI createNewCharacterFooterErrorMessage;

    private string basePath;
    private int classIdSelected;
    private string classNameSelected;

    private void Start()
    {
        classIdSelected = 0; // Mage
        classNameSelected = WorldManager.GetTranslation("class_mage");
        basePath = Application.persistentDataPath;

        newCharacter.onClick.AddListener(delegate { _onNewCharacterClick(); });
        undoModalCreateNewCharacter.onClick.AddListener(delegate { _onUndoModalCreateNewCharacterClick(); });
        confirmModalCreateNewCharacter.onClick.AddListener(delegate { _onCreateNewCharacterCreateNew(); });

        ClassDescriptionText.text = WorldManager.GetTranslation("class_mage_desc");
        DropdownClassList.onValueChanged.AddListener(delegate { _onDropdownClassListValueChanged(DropdownClassList.value); });
    }

    private void _onCreateNewCharacterCreateNew()
    {
        createNewCharacterFooterErrorMessage.text = "";

        if (CharacterNameText.text.Length < 5)
        {
            createNewCharacterFooterErrorMessage.text = WorldManager.GetTranslation("invalid_name");
        } else
        {
            StartGameSavedData dataToStore = new StartGameSavedData(null);
            dataToStore.name = CharacterNameText.text;
            dataToStore.classId = classIdSelected;
            dataToStore.className = classNameSelected;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(basePath + "/savedData/startGameConfig.umx", FileMode.Create);

            formatter.Serialize(stream, dataToStore);
            stream.Close();
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
