using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : Entity
{
    enum InputState
    {
        PlayerInput,
        TrapInput
    }

    [SerializeField]
    Movement playerMovement;
    [SerializeField]
    AttackComp playerAttack;
    [SerializeField]
    TrapHandler trapHandler;

    private bool enableRightTrigger = true;

    void FixedUpdate()
    {
        // Right Stick to rotate and Left stick to move forward and backwards
        //Move(Input.GetAxis("Vertical"));
        //Rotate(new Vector3(0, Input.GetAxis("Right Horizontal")));

        // Moves and looks wherever the analog stick is
        playerMovement.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        playerMovement.LookToStick(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (Input.GetAxis("Right Trigger") > .5f || Input.GetMouseButtonDown(0))
        {
            if (enableRightTrigger)
            {
                playerAttack.Attack();
                enableRightTrigger = false;
            }
        }
        else
            enableRightTrigger = true;

        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            trapHandler.PutTrap(0,playerMovement.GetPlayerPosition());
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            trapHandler.PutTrap(1, playerMovement.GetPlayerPosition());
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            trapHandler.PutTrap(2, playerMovement.GetPlayerPosition());
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            trapHandler.PutTrap(3, playerMovement.GetPlayerPosition());
        }
    }

    

    public override void Die()
    {
        enabled = false;
    }
}
