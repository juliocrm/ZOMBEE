using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class EnemyHP : Entity, IHurtable
{
    [SerializeField]
    private int hp;

    private float damageMultiplier = 1;

    [SerializeField]
    public GameObject _hitFeedback;

    public UnityEvent Injured;

    public EnemyHP()
    {

    }

    private void Awake()
    {
        Injured = new UnityEvent();
        Assert.IsNotNull(_hitFeedback, "Asigna las particulas _hitFeedback para sangrar cuando golpeen");
    }

    public override void Die()
    {
        var entities = GetComponents<Entity>();
        foreach (var entity in entities)
            if(entity != this) entity.Die();
    }

    public int Hurt(int damage, Vector3 from)
    {
        hp -= Mathf.RoundToInt(damage * damageMultiplier);

        if (damage < 0) Instantiate(_hitFeedback, transform.position, Quaternion.LookRotation(from, transform.position));

        if (hp <= 0)
            Die();

        
        Injured.Invoke();

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
