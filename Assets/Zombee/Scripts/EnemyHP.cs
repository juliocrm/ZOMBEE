using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : Entity, IHurtable
{
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public int Hurt(int damage)
    {
        throw new System.NotImplementedException();
    }
}
