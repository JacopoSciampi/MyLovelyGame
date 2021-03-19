using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoItemPickup : MonoBehaviour
{
    public int itemId;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Check", 0, 0.25f);
    }
    void Check() {
        if (DemoCharacterController.instance != null) {
            if(Vector3.Distance(transform.position, DemoCharacterController.instance.transform.position) < 1.5f) {
                DemoCharacterController.instance.PickupItem(itemId);
                GameObject.Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
