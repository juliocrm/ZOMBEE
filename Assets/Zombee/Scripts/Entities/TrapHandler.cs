using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHandler : Entity
{

    public TrapComponent[] traps;
    bool Died = false;
    public Animator _anim;
    public int StaminaCost;

    public override void Die()
    {
        enabled = false;
    }

    public void PutTrap(int Trap, Vector3 PlayerPosition)
    {
        
        if (!Died && GetComponent<Stamina>().StaminaAmount>=StaminaCost)
        {
            GetComponent<Stamina>().Hurt(StaminaCost, transform.position);
            _anim.SetTrigger("Crouch");
            traps[Trap].InstantiateTrap(PlayerPosition);
        }
    }
}
