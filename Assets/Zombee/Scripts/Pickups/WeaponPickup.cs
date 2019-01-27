using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPickup : MonoBehaviour
{

    public int TypeWeapon;

    private void OnTriggerEnter(Collider collider)
    {
        AttackComp _attackComp = collider.gameObject.GetComponent<AttackComp>();
        if (_attackComp != null) {
            if (!_attackComp.HasWeapon) {
                _attackComp.SetWeapon(TypeWeapon);
                gameObject.SetActive(false);
            }
        }
    }

}
