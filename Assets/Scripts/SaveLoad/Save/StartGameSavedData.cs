[System.Serializable]
public class StartGameSavedData
{
    public string defaultCharacterName { get; set; }

    public StartGameSavedData(StartGameSavedData data)
    {
        if (data != null)
        {
            defaultCharacterName = data.defaultCharacterName;
        }
    }
}
