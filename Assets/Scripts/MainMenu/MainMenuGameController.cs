using UnityEngine;
using UnityEngine.UI;

public class MainMenuGameController : MonoBehaviour
{
    [Header("Required")]
    public Button Backbutton;
    public Button Optionsbutton;
    public AudioController audioController;
    public Texture2D defaultCursor;

    [Header("RequiredComponents")]
    public Slider musicVolumeSlider;

    private SaveOptionsData toSave;


    private void Start()
    {
        Backbutton.onClick.AddListener(delegate { Save(); });
        Optionsbutton.onClick.AddListener(delegate { _OnOptionsButtonClicked(); });
        toSave = new SaveOptionsData(null);

        initStoredData(_SaveSystem.LoadOptionsMenu());
    }

    private void _OnOptionsButtonClicked()
    {
        _resetCursor();
    }

    private void _resetCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Save()
    {
        _resetCursor();
        toSave.musicVolume = musicVolumeSlider.value;

        _SaveSystem.SaveOptionsMenu(toSave);
    }

    private void initStoredData(SaveOptionsData data)
    {
        musicVolumeSlider.value = data.musicVolume;
        audioController.setVolume(data.musicVolume);
    }
}
