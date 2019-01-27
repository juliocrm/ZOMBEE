using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickupHealth : MonoBehaviour
{
    public float StaminaIncrease = 35f;
    public float StaminaTransferRate = 20f;
    Stamina staminaComponent;
    bool Incontact = false;
    [SerializeField]
    private GameObject _HealFeedback;

    private void Awake()
    {
        Assert.IsNotNull(_HealFeedback, "Falta asignar el _HealFeedback para mostrar curacion");
        _HealFeedback.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        staminaComponent= collision.gameObject.GetComponent<Stamina>();
        if (staminaComponent != null) {
            Incontact = true;
            StartCoroutine(TimeToReceibeStamina());
            _HealFeedback.SetActive(true);
        }   
    }

    private IEnumerator TimeToReceibeStamina()
    {
        float accum = 0f;
        while (StaminaIncrease > 0 && staminaComponent)
        {
            float delta = StaminaTransferRate * Time.deltaTime;
            StaminaIncrease -= StaminaTransferRate * Time.deltaTime;
            accum += delta;
            if (accum > 1f)
            {
                int amountToTransfer = (int) accum;
                staminaComponent.Hurt(amountToTransfer, transform.position);
                accum -= amountToTransfer;
            }
            yield return 0;
        }

        if (StaminaIncrease <= 0)
        {
            gameObject.SetActive(false);
            _HealFeedback.SetActive(false);
            StopCoroutine(TimeToReceibeStamina());
        }
    }
    

    private void OnTriggerExit(Collider collision)
    {
        if (staminaComponent == collision.gameObject.GetComponent<Stamina>())
        {
            StopCoroutine(TimeToReceibeStamina());
            Incontact = false;
            staminaComponent = null;
            _HealFeedback.SetActive(false);
        }
    }
}
