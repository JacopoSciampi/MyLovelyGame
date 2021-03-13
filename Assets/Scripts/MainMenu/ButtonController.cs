using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Required")]
    public AudioSource myAudio;

    private bool isAudioAlreadyPlayer;

    private void Awake()
    {
        myAudio.clip = Resources.Load<AudioClip>("Media/Audio/MainMenu/ButtonHover");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isAudioAlreadyPlayer == false)
        {
            isAudioAlreadyPlayer = true;
            myAudio.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isAudioAlreadyPlayer = false;
        myAudio.Stop();
    }
}
