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

    public Vector3 OverlapSpherePosition { get { return _overlapSphereTransform.position; } }

    public UnityEvent OnAttacked;

    private void Start()
    {
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
        //Debug.Log("Atacking");
        if (_currentWeapon.durability > 0)
        {
            Physics.OverlapSphere(_overlapSphereTransform.position,
                _sphereRadious);
            Collider[] _collider = Physics.OverlapSphere(transform.position, 2f);

            OnAttacked.Invoke();

            Debug.Log("HIT");

            foreach (var contact in _collider)
            {
                if (contact.GetComponent<IHurtable>() != null)
                {
                    contact.GetComponent<IHurtable>().Hurt(_currentWeapon.damage, transform.position);

                    _currentWeapon.durability--;
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

    public void SetWeapon(int weaponID)
    {
        //Get weapon from GameManager
        _currentWeapon = GameManager.Get.weaponDefs[weaponID];
    }

    private void WeaponEmpty() {
        // TODO Destroy weapon
    }
}
