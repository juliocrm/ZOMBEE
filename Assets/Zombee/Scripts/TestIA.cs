using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIA : MonoBehaviour
{

    public GameObject enemyPref;
    public Transform targetTransform;
    public Transform[] patrolPoints;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPref).GetComponent<EnemyAI>().Init(targetTransform, patrolPoints);
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
