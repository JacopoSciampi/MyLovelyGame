using System.Collections.Generic;

[System.Serializable]
public class StartGameServerList
{
    public string serverName { get; set; }
    public List<CharacterSavedData> characterList { get; set; }
    public List<StartGameServerCharacterInfo> characterInfoList { get; set; }

    public StartGameServerList(StartGameServerList data)
    {
        if (data != null)
        {
            serverName = data.serverName;
            characterList = data.characterList;
            data.characterInfoList = data.characterInfoList;
        }
    }
}

[System.Serializable]
public class StartGameServerCharacterInfo
{
    public string playerName { get; set; }
    public float[] playerPosition { get; set; }

    public StartGameServerCharacterInfo(StartGameServerCharacterInfo data)
    {
        if (data != null)
        {
            playerName = data.playerName;
            playerPosition = data.playerPosition;
        }
    }
}

[System.Serializable]
public class StartGameSavedDataList
{
    public List<StartGameServerList> serverList { get; set; }
    public List<CharacterSavedData> characterList { get; set; }
    public string currentPlayerName { get; set; }

    public StartGameSavedDataList(StartGameSavedDataList data)
    {
        if (data != null)
        {
            currentPlayerName = data.currentPlayerName;
            serverList = data.serverList;
            characterList = data.characterList;
        }
    }
}
