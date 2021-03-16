using System.Collections.Generic;

[System.Serializable]
public class CharacterSavedData
{
    public string name { get; set; }
    public string className { get; set; }
    public int classId { get; set; }

    public CharacterSavedData(CharacterSavedData data)
    {
        if (data != null)
        {
            name = data.name;
            className = data.className;
            classId = data.classId;
        }
    }

    /*
     inventario
    equip
    vita
    ...
     */
}
