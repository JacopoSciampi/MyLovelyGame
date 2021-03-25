using UnityEngine;
using Mirror;
using System.Collections;
using System.Collections.Generic;

public class WorldObjectController : NetworkBehaviour
{
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private string objectName;
    [SerializeField]
    private int hitPoint;
    [SerializeField]
    private int reflectBack; // IE: punching nodes inflicts damage
    [SerializeField]
    private bool canBeDamagedByHand;
    [SerializeField]
    private bool keepSameRespwanPoint;

    private Vector3 startPosition;
    private List<Vector3> respwanPositionList;
    private System.Random random;
    private void Awake()
    {
        objectName = WorldManager.GetTranslation(objectName);

        if (!keepSameRespwanPoint)
        {
            respwanPositionList = new List<Vector3>();
            startPosition = transform.position;

            respwanPositionList.Add(new Vector3(startPosition.x + 1, startPosition.y, startPosition.z));
            respwanPositionList.Add(new Vector3(startPosition.x - 1, startPosition.y, startPosition.z));
            respwanPositionList.Add(new Vector3(startPosition.x, startPosition.y, startPosition.z + 1));
            respwanPositionList.Add(new Vector3(startPosition.x, startPosition.y, startPosition.z - 1));
            random = new System.Random();
        }
    }

    public void onObjectDamageTaken()
    {
        if (WorldManager.hasValidItemInHand)
        {
            Debug.LogWarning("TODO");
        }
        else
        {
            // character is using his hand
            Debug.Log("NON PUOI");
        }
    }

    private void onItemDesotryed()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine(respawnObject());

    }

    IEnumerator respawnObject()
    {
        yield return new WaitForSeconds(respawnTime);

        gameObject.transform.position = respwanPositionList[random.Next(respwanPositionList.Count)];
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

}
