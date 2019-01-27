using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : Entity, IHurtable
{
    [SerializeField]
    private int hp;

    private float damageMultiplier = 1;

    public EnemyHP()
    {

    }

    public override void Die()
    {
        var entities = GetComponents<Entity>();
        foreach (var entity in entities)
            entity.Die();
    }

    public int Hurt(int damage)
    {
        hp -= Mathf.RoundToInt(damage * damageMultiplier);
        if (hp <= 0)
            Die();

        return hp;
    }

    public void SetDamageMultiplier(float multiplier, float time)
    {
        damageMultiplier = multiplier;
        StartCoroutine(SetDamageMultiplierAfter(1, time));
    }

    private IEnumerator SetDamageMultiplierAfter(float multiplier, float time)
    {
        yield return new WaitForSeconds(time);
        damageMultiplier = multiplier;
    }

}
