using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Required")]
    public AudioSource myAudio;
    public Image defaultImage;
    public Image activeImage;
    public Texture2D defaultMouseTexture;
    public Texture2D clickMouseTexture;

    private bool isAudioAlreadyPlayer;

    private void Awake()
    {
        myAudio.clip = Resources.Load<AudioClip>("Media/Audio/MainMenu/ButtonHover");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAudioAlreadyPlayer == false)
        {
            isAudioAlreadyPlayer = true;
            myAudio.Play();
        }

        Cursor.SetCursor(clickMouseTexture, Vector2.zero, CursorMode.ForceSoftware);
        if (activeImage.enabled == false)
        {
            activeImage.enabled = true;
            defaultImage.enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isAudioAlreadyPlayer = false;
        myAudio.Stop();

        Cursor.SetCursor(defaultMouseTexture, Vector2.zero, CursorMode.ForceSoftware);
        if (activeImage.enabled)
        {
            activeImage.enabled = false;
            defaultImage.enabled = true;
        }

    }
}
