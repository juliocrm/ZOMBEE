using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Stamina : Entity, IHurtable
{
    public const int maxStamina = 100;

    [SerializeField]
    private int StartingStamina = maxStamina;

    public float StaminaAmount { get; private set; }

    [SerializeField]
    public GameObject _hitFeedback;

    [SerializeField]
    public GameObject _deadBodyPrefab;
    
    public Animator _animator;

    public UnityEvent Injured;

    private void Awake()
    {
        Injured = new UnityEvent();
        StaminaAmount = StartingStamina;

        _animator = GetComponentInChildren<Animator>();

        Assert.IsNotNull(_hitFeedback, "Asigna las particulas _hitFeedback para sangrar cuando golpeen");
    }

    public override void Die()
    {
        GameObject deadBody = Instantiate(_deadBodyPrefab, transform.parent);
        deadBody.transform.position = transform.position;
        deadBody.transform.rotation = transform.rotation;

        var entities = GetComponents<Entity>();
        foreach (var entity in entities)
            if(entity != this) entity.Die();

        GetComponentInChildren<Animator>().SetBool("Dead", true);
    }

     public float Hurt(float damage, Vector3 from)
    {
        Debug.LogFormat("Stamina: {0}", StaminaAmount);
        StaminaAmount -= damage;

        if (StaminaAmount <= 0)
            Die();

        if (damage > 0 && (from - transform.position).sqrMagnitude > 0.1f)
        {
            Instantiate(_hitFeedback, transform.position, Quaternion.LookRotation(from, transform.position));
            _animator.SetTrigger("Hit");
        }

        Injured.Invoke();

        return StaminaAmount;
    }
}
