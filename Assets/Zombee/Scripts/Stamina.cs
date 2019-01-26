using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Entity, IHurtable
{
    const int maxStamina = 100;

    int stamina =  maxStamina;
    public int StaminaAmount { get; set; }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    int IHurtable.Hurt(int damage)
    {
        throw new System.NotImplementedException();
    }
}
