using TMPro;
using UnityEngine;

public class MainMenuTranslationController : MonoBehaviour
{
    [Header("Required")]
    public TextMeshProUGUI quitText;
    public TextMeshProUGUI sloganText;
    public TextMeshProUGUI optionsText;
    public TextMeshProUGUI startGameText;

    private void Start()
    {
        quitText.text = WorldManager.GetTranslation("quit");
        sloganText.text = WorldManager.GetTranslation("slogan");
        optionsText.text = WorldManager.GetTranslation("options");
        startGameText.text = WorldManager.GetTranslation("startGame");
    }
}
