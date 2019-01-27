using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackComp : Entity
{
    private WeaponDef _currentWeapon;

    [SerializeField]
    public Transform _overlapSphereTransform;
    [SerializeField]
    public float _sphereRadious;
    [SerializeField]
    public float _attackDelay = .75f;

    public bool CanAttack { get; private set; }

    public Vector3 OverlapSpherePosition { get { return _overlapSphereTransform.position; } }

    public UnityEvent OnAttacked;

    private void Start()
    {
        CanAttack = true;
        SetWeapon(0);
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
        if (_currentWeapon.durability > 0)
        {
            CanAttack = false;
            StartCoroutine(_WaitToBeAbleToAttack());

            Physics.OverlapSphere(_overlapSphereTransform.position,
                _sphereRadious);
            Collider[] _collider = Physics.OverlapSphere(transform.position, 2f);

            Debug.Log("Attack!");

            OnAttacked.Invoke();

            foreach (var contact in _collider)
            {
                if (contact.gameObject != gameObject && contact.GetComponent<IHurtable>() != null)
                {
                    contact.GetComponent<IHurtable>().Hurt(_currentWeapon.damage, transform.position);

                    _currentWeapon.durability--;

                    Debug.LogFormat("Durability Left {0}", _currentWeapon.durability);
                    if (_currentWeapon.durability <= 0)
                    {
                        WeaponEmpty();
                        break;
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
    }

    private void WeaponEmpty() {
        // TODO Destroy weapon
    }
}
