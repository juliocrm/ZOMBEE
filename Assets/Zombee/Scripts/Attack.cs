using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComp : Entity
{
    private WeaponDef _currentWeapon;
    private bool attacking = true;

    public override void Die()
    {
        WeaponEmpty();
    }

    public AttackComp()
    {
        
    }

    public void Attack()
    {
        if(_currentWeapon.durability > 0)
            attacking = true;
    }

    public void SetWeapon(int weaponID)
    {
        //Get weapon from GameManager
        //_currentWeapon = GameManager.Instance.weaponDefs[weaponID];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (attacking == false) return;

        ContactPoint[] contacts = null;
        collision.GetContacts(contacts);

        foreach (var contact in contacts)
        {
            if (contact.otherCollider.GetComponent<IHurtable>() != null)
            {
                contact.otherCollider.GetComponent<IHurtable>().Hurt(_currentWeapon.damage);

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

    private void WeaponEmpty() {
        // TODO Destroy weapon
    }
}
