using UnityEngine;
using Mirror;
using System.Collections;

public class respawnable : NetworkBehaviour
{
    [SerializeField]
    private float timeToRespwan;

    public void onItemDesotryed()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine(respawnObject());

    }

    IEnumerator respawnObject()
    {
        yield return new WaitForSeconds(timeToRespwan);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

}
