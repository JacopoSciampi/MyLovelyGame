using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveOptionsData
{
    public float musicVolume { get; set; }

    public SaveOptionsData(SaveOptionsData data)
    {
        if (data != null)
        {
            this.musicVolume = data.musicVolume;
        }

        /*
         Vector3 -> position float[];
        position[0] = player.transform.position.x; and so on
         */
    }
}
