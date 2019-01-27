using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeRigidbody : MonoBehaviour
{
    private const float _multiplier = 4;
    // Start is called before the first frame update
    public void Explode(float multiplier)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (var contact in hitColliders)
        {
            if (contact.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = contact.transform.position -
                    transform.position;
                dir *= multiplier;

                contact.GetComponent<Rigidbody>().AddForce(
                    dir,ForceMode.Impulse
                    );
                
            }
        }
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (var contact in hitColliders)
        {
            if (contact.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = contact.transform.position -
                    transform.position;
                dir *= _multiplier;

                contact.GetComponent<Rigidbody>().AddForce(
                    dir, ForceMode.Impulse
                    );

            }
        }
    }
}
