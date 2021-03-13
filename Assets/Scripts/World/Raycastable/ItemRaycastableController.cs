using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRaycastableController : MonoBehaviour
{
    [Header("Required")]
    public string itemName;

    public bool canBeDestroyed;
    public bool showInteractTooltip;
    public bool canRespwan;

    public string tooltipText;
    public int lifePoints;

    public void __DEBUG__()
    {
        Debug.Log(itemName);
    }

    public void Damage(int points)
    {
        lifePoints -= points;

        //if can respawn.. do
    }
}
