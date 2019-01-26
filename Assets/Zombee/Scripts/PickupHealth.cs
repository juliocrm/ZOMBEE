using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    public int Seconds;
    public int StaminaIncrease;
    Stamina staminaComponent;
    private void OnCollisionEnter(Collision collision)
    {
        staminaComponent= collision.gameObject.GetComponent<Stamina>();
        StartCoroutine(IncreaseStamina());
    }
    private IEnumerator IncreaseStamina() {
        yield return new WaitForSeconds(Seconds);
        if (staminaComponent != null) {
            staminaComponent.Hurt(StaminaIncrease);
            StartCoroutine(IncreaseStamina());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (staminaComponent != null && collision.gameObject.GetComponent<Stamina>()) {
            StopCoroutine(IncreaseStamina());
            staminaComponent = null;
        }
    }

}
