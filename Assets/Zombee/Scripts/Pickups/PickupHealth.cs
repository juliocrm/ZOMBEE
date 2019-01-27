using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    public int StaminaIncrease;
    public int Time;
    Stamina staminaComponent;
    bool Incontact = false;

    private void OnTriggerEnter(Collider collision)
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
            staminaComponent.Hurt(StaminaIncrease, transform.position);
            gameObject.SetActive(false);
        }
    }
    

    private void OnTriggerExit(Collider collision)
    {
        if (staminaComponent == collision.gameObject.GetComponent<Stamina>())
        {
            StopCoroutine(TimeToReceibeStamina());
            Incontact = false;
            staminaComponent = null;
        }
    }
}
