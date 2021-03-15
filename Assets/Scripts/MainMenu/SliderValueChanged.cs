using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanged : MonoBehaviour
{
    [Header("Required")]
    public Text text;
    public Slider slider;
    public AudioController audioController;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueChangeCheck();
    }

    public void ValueChangeCheck()
    {
        text.text = slider.value.ToString();
        audioController.setVolume(slider.value);
    }
}
