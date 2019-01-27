using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackComp : Entity
{
    public enum WeaponOwner
    {
        Player,
        Enemy,
        InfectedEnemy
    }
    public WeaponOwner owner;

    private WeaponDef _currentWeapon;

    [SerializeField]
    public Transform _overlapSphereTransform;
    [SerializeField]
    public float _sphereRadious;
    [SerializeField]
    public float _attackDelay = .75f;
    [SerializeField]
    public Animator _animator;

    public GameObject WeaponObject;

    public bool CanAttack { get; private set; }

    public Vector3 OverlapSpherePosition { get { return _overlapSphereTransform.position; } }

    public UnityEvent OnAttacked;

    private void Start()
    {
        CanAttack = true;
        SetWeapon(1);
    }

    public bool HasWeapon
    {
        get
        {
            return _currentWeapon.durability > 0;
        }
    }


    public override void Die()
    {
        WeaponEmpty();
    }

    public AttackComp()
    {
        OnAttacked = new UnityEvent();
    }

    public void Attack()
    {
        if (_currentWeapon.durability > 0 && CanAttack)
        {
            CanAttack = false;
            StartCoroutine(_WaitToBeAbleToAttack());
            _animator.SetTrigger("Attack");
            Collider[] _collider = Physics.OverlapSphere(_overlapSphereTransform.position,_sphereRadious);

            //Debug.Log("Attack!");

            OnAttacked.Invoke();

            foreach (var contact in _collider)
            {
                if (contact.gameObject != gameObject && contact.GetComponent<IHurtable>() != null && contact.GetComponent<AttackComp>())
                {
                    AttackComp contactAttackComp = contact.GetComponent<AttackComp>();
                    bool isEnemy = false;
                    switch (owner)
                    {
                        case WeaponOwner.Player:
                            isEnemy = contactAttackComp.owner == WeaponOwner.Enemy || contactAttackComp.owner == WeaponOwner.InfectedEnemy;
                            break;
                        case WeaponOwner.Enemy:
                            isEnemy = contactAttackComp.owner == WeaponOwner.Player || contactAttackComp.owner == WeaponOwner.InfectedEnemy;
                            break;
                        case WeaponOwner.InfectedEnemy:
                            isEnemy = contactAttackComp.owner == WeaponOwner.Enemy;
                            break;
                        default:
                            break;
                    }

                    if (isEnemy)
                    {
                        contact.GetComponent<IHurtable>().Hurt(_currentWeapon.damage, transform.position);

                        if (owner == WeaponOwner.Player)
                            _currentWeapon.durability--;

                        //Debug.LogFormat("Durability Left {0}", _currentWeapon.durability);
                        if (_currentWeapon.durability <= 0)
                        {
                            WeaponEmpty();
                            break;
                        } 
                    }
                }
            }
        }
        else
        {
            // TODO Make error noise
            Debug.Log("No weapon");
        }
    }

    private IEnumerator _WaitToBeAbleToAttack()
    {
        yield return new WaitForSeconds(_attackDelay);
        CanAttack = true;
    }

    public void SetWeapon(int weaponID)
    {
        //Get weapon from GameManager
        _currentWeapon = GameManager.Get.weaponDefs[weaponID];

        if (owner == WeaponOwner.Player)
        {
            Debug.Log(_currentWeapon.durability);
            WeaponObject.SetActive(_currentWeapon.durability > 0);
        }
        
    }

    private void WeaponEmpty() {
        Debug.Log("LLAMADO"+name);
        if (owner == WeaponOwner.Player)
            WeaponObject.SetActive(false);
    }
}
