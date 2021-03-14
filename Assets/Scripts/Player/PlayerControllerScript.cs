using UnityEngine;
using Mirror;

public class PlayerControllerScript : NetworkBehaviour
{
    [Header("Required")]
    public Animator animator;
    public override void OnStartAuthority()
    {
        enabled = true;
    }

    public void toDo(bool itemEquipped)
    {
        WorldManager.hasValidItemInHand = itemEquipped;
        animator.SetBool("hasValidItemInHand", itemEquipped);
    }


}
