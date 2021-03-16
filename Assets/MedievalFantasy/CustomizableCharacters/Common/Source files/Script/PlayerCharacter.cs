using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacters;
public class PlayerCharacter :  CharacterBase{
    void SaveItems() {
        string itemsList = "";
        foreach (EquipmentSlot e in equipmentSlots) {
            if (e.inUse) {
                itemsList += e.itemId + ","; // add all item IDs to a list, separated by commas
            }
        }
        PlayerPrefs.SetString("SavedPlayerEquipment", itemsList); // save list
    }
    void LoadItems() {
        if (PlayerPrefs.HasKey("SavedPlayerEquipment")) {
            foreach (string s in PlayerPrefs.GetString("SavedPlayerEquipment").Split(',')) { // split by comma and loop through all item ids
                if (s != "") {
                    int itemId = System.Convert.ToInt32(s);
                    EquipItem(itemId);
                }
            }
        }
    }
}
