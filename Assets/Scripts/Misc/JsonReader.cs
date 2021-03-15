using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    private static JsonReader jsonReader;
    public TextAsset text { get; set; }
    public static JsonReader Instance()
    {
        if (!jsonReader)
        {
            jsonReader = FindObjectOfType(typeof(JsonReader)) as JsonReader;

            if (!jsonReader)
            {
                Debug.LogError("JsonReader inactive or missing from unity scene.");
            }
        }

        return jsonReader;
    }
}
