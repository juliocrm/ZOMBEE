using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    public int StaminaIncrease;
    public int Time;
    Stamina staminaComponent;
    bool Incontact = false;
    private void OnCollisionEnter(Collision collision)
    {
        staminaComponent= collision.gameObject.GetComponent<Stamina>();
        if (staminaComponent != null) {
            Incontact = true;
            StartCoroutine(TimeToReceibeStamina());
        }   
    }
    private IEnumerator TimeToReceibeStamina() {
        yield return new WaitForSeconds(Time);
        if (Incontact) {
            staminaComponent.Hurt(StaminaIncrease);
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (staminaComponent == collision.gameObject.GetComponent<Stamina>())
        {
            StopCoroutine(TimeToReceibeStamina());
            Incontact = false;
            staminaComponent = null;
        }
    }
}
