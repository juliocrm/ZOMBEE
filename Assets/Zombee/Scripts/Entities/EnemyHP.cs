using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class EnemyHP : Entity, IHurtable
{
    [SerializeField]
    private float hp;

    private float damageMultiplier = 1;

    [SerializeField]
    public GameObject _hitFeedback;


    [SerializeField]
    public GameObject _deadBodyPrefab;

    public UnityEvent Injured;
    
    public Animator _animator;

    public EnemyHP()
    {

    }

    private void Awake()
    {
        Injured = new UnityEvent();
        Assert.IsNotNull(_hitFeedback, "Asigna las particulas _hitFeedback para sangrar cuando golpeen");
        _animator = GetComponentInChildren<Animator>();
    }

    public override void Die()
    {
        GameObject deadBody = Instantiate(_deadBodyPrefab, transform.parent);
        deadBody.transform.position = transform.position;
        deadBody.transform.rotation = transform.rotation;

        var entities = GetComponents<Entity>();
        foreach (var entity in entities)
            if(entity != this) entity.Die();

        Destroy(gameObject, .05f);
    }

    public float Hurt(float damage, Vector3 from)
    {
        hp -= Mathf.RoundToInt(damage * damageMultiplier);

        if (damage > 0 && (from - transform.position).sqrMagnitude > 0.1f)
        {
            Instantiate(_hitFeedback, transform.position, Quaternion.LookRotation(from, transform.position));
            _animator.SetTrigger("Hit");
        }

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
