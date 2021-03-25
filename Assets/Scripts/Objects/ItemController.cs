using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private string itemName;
    private int itemDamage;

    private void Awake()
    {
        // remove this shit
        itemName = "test";
        itemDamage = 1;
    }

    public ItemController()
    {
        WorldManager.playerItem = this;
    }
}
