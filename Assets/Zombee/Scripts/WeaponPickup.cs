using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public int TypeWeapon;

    private void OnCollisionEnter(Collision collision)
    {
        AttackComp _attackComp =collision.gameObject.GetComponent<AttackComp>();
        if (_attackComp != null) {
            if (!_attackComp.HasWeapon) {
                _attackComp.SetWeapon(TypeWeapon);
                gameObject.SetActive(false);
            }
        }
    }

}
