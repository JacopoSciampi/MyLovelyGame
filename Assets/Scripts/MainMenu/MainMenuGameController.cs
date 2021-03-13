using UnityEngine;
using UnityEngine.UI;

public class MainMenuGameController : MonoBehaviour
{
    [Header("Required")]
    public Button Backbutton;
    public AudioController audioController;

    [Header("RequiredComponents")]
    public Slider musicVolumeSlider;

    private SaveOptionsData toSave;

    private void Start()
    {
        Backbutton.onClick.AddListener(delegate { Save(); });
        toSave = new SaveOptionsData(null);

        initStoredData(_SaveSystem.LoadOptionsMenu());
    }

    private void Save()
    {
        toSave.musicVolume = musicVolumeSlider.value;

        _SaveSystem.SaveOptionsMenu(toSave);
    }

    private void initStoredData(SaveOptionsData data)
    {
        musicVolumeSlider.value = data.musicVolume;
        audioController.setVolume(data.musicVolume);
    }
}
