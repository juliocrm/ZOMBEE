using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : MonoBehaviour
{
    public TrapDef trapDefinition;
    public GameObject TrapPrefab;

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
        //EjecuteTrap
    }

}
