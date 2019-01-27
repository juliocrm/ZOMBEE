﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComp : Entity
{
    private WeaponDef _currentWeapon;

    private bool attacking = false;

    [SerializeField]
    public Transform _overlapSphereTransform;
    [SerializeField]
    public float _sphereRadious;

    public Vector3 OverlapSpherePosition { get { return _overlapSphereTransform.position; } }

    private void Awake()
    {
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
        
    }

    public void Attack()
    {
        //Debug.Log("Atacking");
        if (_currentWeapon.durability > 0)
            attacking = true;
    }

    public void SetWeapon(int weaponID)
    {
        //Get weapon from GameManager
        _currentWeapon = GameManager.Get.weaponDefs[weaponID];
    }

    private void Update()
    {
        if(attacking == true)
        {
            Physics.OverlapSphere(_overlapSphereTransform.position,
                _sphereRadious);
            Collider[] _collider = Physics.OverlapSphere(transform.position, 2f);
            foreach (var contact in _collider)
            {
                if (contact.GetComponent<IHurtable>() != null)
                {
                    contact.GetComponent<IHurtable>().Hurt(_currentWeapon.damage);

                    _currentWeapon.durability--;
                    if (_currentWeapon.durability <= 0)
                    {
                        WeaponEmpty();
                        break;
                    }
                }
            }
            attacking = false;
        }
    }

    private void WeaponEmpty() {
        // TODO Destroy weapon
    }
}
