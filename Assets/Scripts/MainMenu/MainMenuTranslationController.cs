using TMPro;
using UnityEngine;

public class MainMenuTranslationController : MonoBehaviour
{
    [Header("Required")]
    public TextMeshProUGUI quitText;
    public TextMeshProUGUI sloganText;
    public TextMeshProUGUI optionsText;
    public TextMeshProUGUI startGameText;

    public TextMeshProUGUI opt_backText;
    public TextMeshProUGUI opt_musicText;
    public TextMeshProUGUI opt_audioText;

    private void Start()
    {
        quitText.text = WorldManager.GetTranslation("quit");
        sloganText.text = WorldManager.GetTranslation("slogan");
        optionsText.text = WorldManager.GetTranslation("options");
        startGameText.text = WorldManager.GetTranslation("startGame");

        opt_backText.text = WorldManager.GetTranslation("back");
        opt_musicText.text = WorldManager.GetTranslation("music");
        opt_audioText.text = WorldManager.GetTranslation("audio");
    }
}
