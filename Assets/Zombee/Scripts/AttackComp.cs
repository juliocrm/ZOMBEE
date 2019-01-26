using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComp : Entity
{
    private WeaponDef _currentWeapon;

    private bool attacking = false;

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
