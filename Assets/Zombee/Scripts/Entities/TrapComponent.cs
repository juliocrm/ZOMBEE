using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : MonoBehaviour
{
    public TrapDef trapDefinition;

    private bool TrapIsActive = false;
    private bool TrapIsEnable = false;
    private List<GameObject> EnemiesAfected = new List<GameObject>();
    public Animator _anim;
    ExplodeRigidbody Explode;

    private void Awake()
    {
        Explode = GetComponentInChildren<ExplodeRigidbody>();
        if (Explode != null) {
            Explode.gameObject.SetActive(false);
        }
    }

    public void InstantiateTrap(Vector3 Position) {
        Instantiate(gameObject, Position+new Vector3(0,2.5f,0), new Quaternion()); 
    }

    private void OnTriggerEnter(Collider collision)
    {
       

        EnemyAI Enemi = collision.gameObject.GetComponent<EnemyAI>();
        if (Enemi != null)
        {
            Debug.Log("EnimiEntry");
            EnemiesAfected.Add(Enemi.gameObject);
            if (!TrapIsEnable)
            {
                TrapIsEnable = true;
                StartCoroutine(TimeToExecuteTrap(trapDefinition.TimeToActive));
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        EnemyAI Enemi = collision.gameObject.GetComponent<EnemyAI>();
        if (Enemi != null)
        {
            EnemiesAfected.Remove(Enemi.gameObject);
        }
    }

    private IEnumerator TimeToExecuteTrap(float Time) {
        if (trapDefinition.TrapType == TrapType.Weak) {
            gameObject.transform.Translate(0, 0, .3f);
        }
        yield return new WaitForSeconds(Time);
        TrapIsActive = true;
        
        switch (trapDefinition.TrapType) {
            case TrapType.Damage:
                _anim.SetTrigger("Execute");
                ExecuteDamageTrap();
                break;
            case TrapType.Slow:
                ExecuteSlowTrap();
                break;
            case TrapType.Weak:
                _anim.SetTrigger("Execute");
                ExecuteWeakTrap();
                    break;
            case TrapType.Turn:
                _anim.SetTrigger("Execute");
                ExecuteTurnTrap();
                break;

        }
        yield return new WaitForSeconds(trapDefinition.TimeActive);
        TrapIsActive = false;
        Destroy(gameObject);
    }
    private void ExecuteDamageTrap() {

       

        foreach (GameObject enemi in EnemiesAfected) {
            enemi.GetComponent<EnemyHP>().Hurt(trapDefinition.Damage, transform.position);
        }
    }
    private void ExecuteSlowTrap() {
        foreach (GameObject enemi in EnemiesAfected)
        {
            enemi.GetComponent<EnemyAI>().MakeSlow();
        }
        
    }
    private void ExecuteTurnTrap() {
        foreach (GameObject enemi in EnemiesAfected)
        {
            enemi.GetComponent<EnemyAI>().TurnAgainst();
        }
    }
    private void ExecuteWeakTrap() {
        Explode.gameObject.SetActive(true);
        _anim.gameObject.SetActive(false);
        GetComponentInChildren<ExplodeRigidbody>().Explode(10);
    }


   



}
