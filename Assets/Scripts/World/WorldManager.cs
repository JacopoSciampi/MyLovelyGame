using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class WorldManager
{
    private static TextMeshProUGUI tooltipText;
    private static Image tooltipImage;

    public static bool isTooltipActive;
    public static bool hasValidItemInHand;

    public static Color errorColor;
    public static Color successColor;
    public static Color hintColor;

    public static void __INIT__()
    {
        errorColor = new Color(224, 57, 0);
        successColor = new Color(255, 228, 122);
        hintColor = new Color(24, 224, 0);
    }

    public static void setTooltip(string message)
    {
        if (tooltipImage == null || tooltipText == null)
        {
            GameObject image = GameObject.Find("IMG_tooltip");
            tooltipImage = image.GetComponent<Image>();
            GameObject text = GameObject.Find("tooltipText");
            tooltipText = text.GetComponent<TextMeshProUGUI>();
        }

        if (isTooltipActive == false)
        {
            isTooltipActive = true;

            Color tmpColor = tooltipImage.color;
            tmpColor.a = 1f;

            tooltipImage.color = tmpColor;
            tooltipText.text = message;
        }
    }

    public static void removeTooltip()
    {
        if (tooltipImage != null && tooltipText != null)
        {
            isTooltipActive = false;
            Color tmpColor = tooltipImage.color;
            tmpColor.a = 0f;

            tooltipImage.color = tmpColor;
            tooltipText.text = null;
        }
    }
}