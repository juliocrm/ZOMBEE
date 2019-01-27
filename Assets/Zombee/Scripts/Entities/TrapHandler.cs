using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHandler : Entity
{

    public TrapComponent[] traps;

    public override void Die()
    {
        enabled = false;
    }

    public void CraftTrap(int i)
    {

    }
    public void PutTrap(int Trap, Vector3 PlayerPosition)
    {
        if (enabled)
        {
            traps[Trap].InstantiateTrap(PlayerPosition);
        }
    }
}
