using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIA : MonoBehaviour
{

    public GameObject enemyPref;
    public Transform targetTransform;
    public Transform[] patrolPoints;
    public EnemyAI.EnemyType EnemyType = EnemyAI.EnemyType.Assasin;

    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = Instantiate(enemyPref, transform);
        enemy.GetComponent<EnemyAI>().Init(EnemyType, targetTransform, patrolPoints);
        enemy.transform.parent = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
