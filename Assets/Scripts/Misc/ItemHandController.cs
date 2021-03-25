using UnityEngine;

public class ItemHandController : MonoBehaviour
{
    [Header("Required")]
    public LayerMask layer;

    private float nextRayCheckTime = 2f;
    private float lastCheckedTime;
    private void Awake()
    {
        lastCheckedTime = Time.time;
    }

    void FixedUpdate()
    {
        CheckIfItemHasCollided();
    }

    private void CheckIfItemHasCollided()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 80))
        {
            WorldObjectController item = hit.transform.gameObject.GetComponent<WorldObjectController>();

            if (item != null && Time.time > (lastCheckedTime + nextRayCheckTime))
            {
                lastCheckedTime = Time.time;
                Debug.Log(item.name);
            }
        }
    }
}
