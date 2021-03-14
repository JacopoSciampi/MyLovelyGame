using UnityEngine;

public class RayCastController : MonoBehaviour
{
    [Header("Required")]
    public LayerMask layer;

    void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            if (WorldManager.isTooltipActive == false)
            {
                ItemRaycastableController item = hit.transform.gameObject.GetComponent<ItemRaycastableController>();

                if (item != null && item.showInteractTooltip)
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
