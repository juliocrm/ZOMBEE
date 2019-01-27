using System.Collections;
using System.Collections.Generic;
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
        
        if (Input.GetAxis("Right Trigger") > .5f)
        {
            if (enableRightTrigger)
            {
                playerAttack.Attack();
                enableRightTrigger = false;
            }
        }
        else
            enableRightTrigger = true;

        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            //trapHandler.PutTrap(0);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            //trapHandler.PutTrap(1);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            //trapHandler.PutTrap(2);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            //trapHandler.PutTrap(3);
        }
    }

    

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
