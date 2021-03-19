using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRaces : MonoBehaviour {
    [System.Serializable]
    public class Race {
        public int race;
        public GameObject item;
    }
    public List<Race> races = new List<Race>();

}
