[System.Serializable]
public class StartGameSavedData
{
    public string name { get; set; }
    public int classId { get; set; }
    public string className { get; set; }


    public StartGameSavedData(StartGameSavedData data)
    {
        if (data != null)
        {
            name = data.name;
            classId = data.classId;
            className = data.className;
        }
    }
}
