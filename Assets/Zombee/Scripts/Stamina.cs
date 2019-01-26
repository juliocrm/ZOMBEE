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
        var entities = GetComponents<Entity>();
        foreach (var entity in entities)
            entity.Die();
    }

     public int Hurt(int damage)
    {
        stamina += damage;

        if (stamina <= 0)
            Die();

        return stamina;
    }
}
