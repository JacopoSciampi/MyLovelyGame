using UnityEngine;

public class ItemHandController : MonoBehaviour
{
    [Header("Required")]
    public LayerMask layer;

    private float nextRayCheckTime = 1f;

    void FixedUpdate()
    {
        if(WorldManager.hasValidItemInHand)
        {
            CheckIfItemHasCollided();
        }
    }

    private void CheckIfItemHasCollided()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 80) && Time.time > nextRayCheckTime)
        {

            ItemRaycastableController item = hit.transform.gameObject.GetComponent<ItemRaycastableController>();

            if (item != null)
            {
                Debug.Log(item.name);
            }
        }
    }
}
