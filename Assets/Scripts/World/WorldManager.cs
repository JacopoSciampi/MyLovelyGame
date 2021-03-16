using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Collections.Generic;

public static class WorldManager
{
    private class TranslationDictionary
    {
        public string it { set; get; }
        public string en { set; get; }
    }

    private static TextMeshProUGUI tooltipText;
    private static Image tooltipImage;

    public static bool isTooltipActive;
    public static bool hasValidItemInHand;

    public static Color errorColor;
    public static Color successColor;
    public static Color hintColor;

    public static string language;
    public static List<string> classListDropdownData;
    public static CharacterSavedData currentCharactedSelected;

    private static bool hasBeenInit;
    private static Dictionary<string, TranslationDictionary> translations;

    public static void __INIT__()
    {
        if (!hasBeenInit)
        {
            hasBeenInit = true;
            _initTranslations();
            if (language == null)
            {
                language = "it";
            }

            classListDropdownData = new List<string>() {
                "class_mage",
                "class_paladin",
                "class_assassin",
                "class_necromancer"
            };

            hintColor = new Color(24, 224, 0);
            errorColor = new Color(224, 57, 0);
            successColor = new Color(255, 228, 122);
        }
    }

    public static void setCurrentCharactedSelected(CharacterSavedData data)
    {
        currentCharactedSelected = data;
    }

    public static string GetTranslation(string key)
    {
        if (translations == null)
        {
            _initTranslations();
        }

        TranslationDictionary qualcosa;
        if (translations.TryGetValue(key, out qualcosa))
        {
            if (language == "en")
            {
                return qualcosa.en;
            }
            else if (language == "it")
            {
                return qualcosa.it;
            }
        }

        return key;
    }

    private static void _initTranslations()
    {
        JsonReader jsonReader = new GameObject().AddComponent<JsonReader>();

        jsonReader.text = (Resources.Load("Translations") as TextAsset);

        translations = JsonConvert.DeserializeObject<Dictionary<string, TranslationDictionary>>(jsonReader.text.ToString());
        Object.Destroy(jsonReader);
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