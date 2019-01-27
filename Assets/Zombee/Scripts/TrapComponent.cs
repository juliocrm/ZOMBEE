using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : MonoBehaviour
{
    public TrapDef trapDefinition;
    public GameObject TrapPrefab;

    private bool TrapIsActive = false;
    private bool TrapIsEnable = false;

    [SerializeField]
    public Transform _overlapSphereTransform;
    [SerializeField]
    public float _sphereRadious;

    public Vector3 OverlapSpherePosition { get { return _overlapSphereTransform.position; } }


    public void InstantiateTrap(Vector3 Position) {
        //Create a prefab copy in the point 
        //Instantiate in position
        Instantiate(TrapPrefab, Position, new Quaternion()); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("toca");
        if (!TrapIsEnable && collision.gameObject.GetComponent<EnemyAI>())
        {
            Debug.Log("activa");
            TrapIsEnable = true;
            StartCoroutine(TimeToExecuteTrap(trapDefinition.TimeToActive));
        }
    }
    private IEnumerator TimeToExecuteTrap(float Time) {
        yield return new WaitForSeconds(Time);
        TrapIsActive = true;
        yield return new WaitForSeconds(trapDefinition.TimeActive);
        TrapIsActive = false;
        Destroy(TrapPrefab);
    }

    private List<EnemyHP> EnemiesAfected=new List<EnemyHP>();
    private void Update()
    {
        if (TrapIsActive)
        {
            Physics.OverlapSphere(_overlapSphereTransform.position,
                _sphereRadious);
            Collider[] _collider = Physics.OverlapSphere(transform.position, 2f);
            foreach (var contact in _collider)
            {
                switch (trapDefinition.TrapType) {
                    case TrapType.Damage:
                        EnemyHP Enemi = contact.GetComponent<EnemyHP>();
                        if (Enemi = null) {
                            if (!EnemiesAfected.Contains(Enemi)) {
                                Enemi.Hurt(trapDefinition.Damage);
                            }
                        }
                        break;
                    case TrapType.Slow:
                        //Ejecutar lentitud
                        break;
                    case TrapType.Turn:
                        //Ejecutar turn
                        break;
                    case TrapType.Weak:
                        EnemyHP Enemi2 = contact.GetComponent<EnemyHP>();
                        if (Enemi2 = null)
                        {
                            Enemi2.SetDamageMultiplier(trapDefinition.Damage, trapDefinition.TimeActive);
                        }
                        break;
                }
            }
        }
    }



}
