using TMPro;
using UnityEngine;

public class StartGameTranslationsController : MonoBehaviour
{
    [Header("**Required**")]
    public TextMeshProUGUI slogan;
    public TMP_Dropdown DropdownClassList;

    [Header("**Required Character Info**")]
    public TextMeshProUGUI characterInfoText;
    public TextMeshProUGUI c_name;
    public TextMeshProUGUI c_class;

    [Header("**Required ServerList Info**")]
    public TextMeshProUGUI serverListText;

    [Header("**Required Footer button Info**")]
    public TextMeshProUGUI footerNewCharacterText;
    public TextMeshProUGUI footerLoadCharacterText;
    public TextMeshProUGUI footerDeleteCharacterText;
    
    [Header("**Create Character Modal")]
    public TextMeshProUGUI newNameText;
    public TextMeshProUGUI newClassText;
    public TextMeshProUGUI modalHeaderTitleText;
    public TextMeshProUGUI newClassDropdownTitleText;
    public TextMeshProUGUI newCreateCharacterButtonText;
    public TextMeshProUGUI newCancelCharacterButtonText;

    private void _populateClassDropdownData()
    {
        DropdownClassList.options.Clear();
        foreach (string className in WorldManager.classListDropdownData)
        {
            DropdownClassList.options.Add(new TMP_Dropdown.OptionData() { text = WorldManager.GetTranslation(className) });
        }
    }
    private void Start()
    {
        WorldManager.__INIT__();
        _populateClassDropdownData();

        /** REQUIRED **/
        slogan.text = WorldManager.GetTranslation("slogan");

        /** CHARACTER INFO **/
        characterInfoText.text = WorldManager.GetTranslation("character_info");
        c_name.text = WorldManager.GetTranslation("name");
        c_class.text = WorldManager.GetTranslation("class");

        /** SERVER LIST INFO **/
        serverListText.text = WorldManager.GetTranslation("server_list");

        /** FOOTER BUTTON INFO **/
        footerNewCharacterText.text = WorldManager.GetTranslation("new_character");
        footerLoadCharacterText.text = WorldManager.GetTranslation("load_character");
        footerDeleteCharacterText.text = WorldManager.GetTranslation("delete_character");

        /** CREATE CHARACTER MODAL **/
        newNameText.text = WorldManager.GetTranslation("name");
        newClassText.text = WorldManager.GetTranslation("class");
        modalHeaderTitleText.text = WorldManager.GetTranslation("create_new_character");
        newClassDropdownTitleText.text = WorldManager.GetTranslation("select_the_class");
        newCancelCharacterButtonText.text = WorldManager.GetTranslation("cancel");
        newCreateCharacterButtonText.text = WorldManager.GetTranslation("create_new_character_button");

    }
}
