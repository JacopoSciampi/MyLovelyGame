using UnityEngine;

public class RayCastController : MonoBehaviour
{
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;

    void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {
            if (WorldManager.isTooltipActive == false)
            {
                ItemRaycastableController item = hit.transform.gameObject.GetComponent<ItemRaycastableController>();

                if (item.showInteractTooltip)
                {
                    WorldManager.setTooltip(item.tooltipText);
                }
            }
        }
        else
        {
            WorldManager.removeTooltip();
        }
    }
}
