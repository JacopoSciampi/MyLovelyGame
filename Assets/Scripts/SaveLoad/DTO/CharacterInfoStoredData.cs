using UnityEngine;

[System.Serializable]
public class CharacterInfoStoredData
{
    public float[] position { get; set; }
    public string name { get; set; }

    public CharacterInfoStoredData(CharacterInfoStoredData data)
    {
        position = data.position;
        name = data.name;
    }
}
