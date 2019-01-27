using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Entity
{
    enum EnemyType
    {
        Patrol,
        Destroyer,
        Assasin
    }

    [SerializeField]
    float enemyScope = 15;

    public Transform playerTransform;

    public Transform[] PatrolPoints { get; set; }
    public Transform TargetTransform { get; set; }

    Transform enemyTransform;
    NavMeshAgent enemyAgent;
    EnemyType enemyType;

    bool isAlive = true;
    bool playerOnSight;


    private void Awake()
    {
        enemyTransform = transform;
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    public void Init(Transform t, Transform[] pp)
    {
        float randomType = UnityEngine.Random.Range(0f, 1f);
        if (randomType < 0.45f)
        {
            enemyType = EnemyType.Patrol;
            PatrolPoints = pp;
            ChangeTarget();
        }
        else if (randomType < 9)
        {
            enemyType = EnemyType.Destroyer;
            TargetTransform = t;
        }
        else
            enemyType = EnemyType.Assasin;

        print(enemyType);
        StartCoroutine(Decide());
    }

    readonly WaitForSeconds waitUntilNextDecition = new WaitForSeconds(0.1f);

    IEnumerator Decide()
    {
        while (isAlive)
        {
            if (enemyType == EnemyType.Assasin)
                FollowPlayer();
            else
            {
                playerOnSight = CheckForPlayer();
                if (playerOnSight)
                    FollowPlayer();
                else
                {
                    switch (enemyType)
                    {
                        case EnemyType.Patrol:
                            enemyAgent.autoBraking = false;
                            enemyAgent.destination = TargetTransform.position;
                            if (!enemyAgent.pathPending && enemyAgent.remainingDistance < 0.5f)
                                ChangeTarget();
                            break;
                        case EnemyType.Destroyer:
                            enemyAgent.destination = TargetTransform.position;
                            enemyAgent.autoBraking = true;
                            break;
                    }
                }
            }
            yield return waitUntilNextDecition;
        }
    }
    
    void FollowPlayer()
    {
        print("Following");
        enemyAgent.destination = playerTransform.position;
        enemyAgent.autoBraking = true;
    }

    private bool CheckForPlayer()
    {
        Vector3 toPlayer = (playerTransform.position - enemyTransform.position);
        //print("toPlayer.magnitude < enemyScope " + (toPlayer.magnitude < enemyScope));
        if (toPlayer.magnitude < enemyScope)
        {
            Vector3 toPlayerNormalized = toPlayer.normalized;
            //print("Vector3.Dot(enemyTransform.forward, toPlayerNormalized) >= 0.5f " + (Vector3.Dot(enemyTransform.forward, toPlayerNormalized) >= 0.5f));
            if (Vector3.Dot(enemyTransform.forward, toPlayerNormalized) >= 0f)
                if (Physics.Raycast(enemyTransform.position, toPlayerNormalized, out RaycastHit hit, enemyScope))
                {
                    if(playerOnSight)
                        Debug.Log(1);
                    if (hit.collider.GetComponent<PlayerController>())
                    {
                        if (playerOnSight)
                            Debug.Log(2);
                        return true;
                    }
                }
        }
        return false;
    }

    private int destPoint = 0;

    void ChangeTarget()
    {
        // Returns if no points have been set up or just have 1
        if (PatrolPoints.Length <= 1)
            return;
        // Set the agent to go to the currently selected destination.
        TargetTransform = PatrolPoints[destPoint];
        enemyAgent.destination = TargetTransform.position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        int newDest = UnityEngine.Random.Range(0, PatrolPoints.Length);

        while(newDest == destPoint)
            newDest = UnityEngine.Random.Range(0, PatrolPoints.Length);

        destPoint = newDest;
    }

    public override void Die()
    {
        isAlive = false;
    }
}
