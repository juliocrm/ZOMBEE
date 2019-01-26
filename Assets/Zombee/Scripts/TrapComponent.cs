using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : MonoBehaviour
{
    public TrapDef trapDefinition;
    public GameObject TrapPrefab;
    private bool TrapIsActive = false;

    public void InstantiateTrap(Vector3 Position) {
        //Create a prefab copy in the point 
        //Instantiate in position
        Instantiate(TrapPrefab, Position, new Quaternion()); 
        TimeToExecuteTrap(trapDefinition.TimeToActive);
    }
    private IEnumerator TimeToExecuteTrap(float Time) {
        yield return new WaitForSeconds(Time);
        ExecuteTrap();
    }
    private void ExecuteTrap() {
        TrapIsActive = true;
        StartCoroutine(DeactiveTrap());
    }
    private IEnumerator DeactiveTrap() {
        yield return new WaitForSeconds(trapDefinition.TimeActive);
        TrapIsActive = false;
        Destroy(TrapPrefab);
    }
    private void OnCollisionEnter(Collision collision)
    {
        EnemyHP enemyHP = collision.gameObject.GetComponent<EnemyHP>();
        if (enemyHP != null) {
            switch (trapDefinition.TrapType) {
                case TrapType.Damage:
                    //Damage to enemy
                    break;
                case TrapType.Slow:
                    //enemi is slow
                    break;
                case TrapType.Turn:
                    //fight enemies
                    break;
                case TrapType.Weak:
                    //...
                    break;

            }
        }
    }
}
