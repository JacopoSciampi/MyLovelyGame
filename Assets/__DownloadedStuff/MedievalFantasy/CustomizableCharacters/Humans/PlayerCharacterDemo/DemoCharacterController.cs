using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCharacterController : MonoBehaviour {

    public static DemoCharacterController instance;
    private void Awake() {
        instance = this;
    }
    Animator anim;
    PlayerCharacter player;
    CharacterController cc;

    private float movementSpeed;

    public float walkSpeed = 1.5f;
    public float runSpeed = 4;
    public float rotationSpeed = 10;

    public void PickupItem(int itemId) {
        player.EquipItem(itemId);
    }




    void Start() {
        player = GetComponent<PlayerCharacter>();
        anim = player.animator;
        cc = GetComponent<CharacterController>();

        player.EquipItem(37);
        player.EquipItem(38);
        player.EquipItem(249);
        player.EquipItem(257);
    }

    void Update() {

        Vector3 movementDirection = UpdateInput();

        if (movementDirection != Vector3.zero) {
            cc.Move((transform.forward * movementSpeed) * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * rotationSpeed); // rotate character to movement direction
        } else {
            cc.Move(Vector3.zero);
        }
        anim.SetFloat("MovementSpeed", cc.velocity.magnitude); // play idle/walk/run animation
    }


    Vector3 UpdateInput() {
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            movementDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            movementDirection += -Vector3.forward;
        }
        if (Input.GetKey(KeyCode.D)) {
            movementDirection += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A)) {
            movementDirection += -Vector3.right;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            movementSpeed = runSpeed;
        } else {
            movementSpeed = walkSpeed;
        }
        return movementDirection;
    }

}
