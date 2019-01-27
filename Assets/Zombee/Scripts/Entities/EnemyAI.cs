using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class EnemyAI : Entity
{
    public enum EnemyType
    {
        Patrol,
        Destroyer,
        Assasin
    }

    [SerializeField]
    float enemyScope = 15;

    private Transform playerTransform;
    private readonly int layerMask = 1 << 9;

    public Transform[] PatrolPoints { get; set; }
    public Transform TargetTransform { get; set; }

    public UnityEvent ChasingEnemy;

    Transform enemyTransform;
    AttackComp attackComp;
    NavMeshAgent enemyAgent;
    EnemyType enemyType;

    bool isAlive = true;
    bool playerOnSight;


    private void Awake()
    {
        enemyTransform = transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        ChasingEnemy = new UnityEvent();
        attackComp = GetComponent<AttackComp>();
    }

    public void Init(EnemyType enemyType, Transform t, Transform[] pp)
    {
        PlayerController playerCtrl = transform.root.gameObject.GetComponentInChildren<PlayerController>(true);
        //Transform playerCtrl = transform.root.Find("Player");
        Assert.IsNotNull(playerCtrl, "AI no encontro jugador, esta todo bajo root?");
        playerTransform = playerCtrl.gameObject.transform;

        this.enemyType = enemyType;


        switch (enemyType)
        {
            case EnemyType.Patrol:
                PatrolPoints = pp;
                ChangeTarget();
                break;
            case EnemyType.Destroyer:
                TargetTransform = t;
                break;
            case EnemyType.Assasin:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
        }
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
                        case EnemyType.Assasin:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            yield return waitUntilNextDecition;
        }
    }

    private void Update()
    {
        if (playerOnSight)
        {
            ChasingEnemy.Invoke();
        }
    }
    
    void FollowPlayer()
    {
        enemyAgent.destination = playerTransform.position;
        enemyAgent.autoBraking = true;
        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < 2f)
            attackComp.Attack();

    }

    private bool CheckForPlayer()
    {
        Vector3 toPlayer = (playerTransform.position - enemyTransform.position);
        if (toPlayer.magnitude < enemyScope)
        {
            Vector3 toPlayerNormalized = toPlayer.normalized;
            if (Vector3.Dot(enemyTransform.forward, toPlayerNormalized) >= 0.4f)
                if (Physics.Raycast(enemyTransform.position, toPlayerNormalized, out RaycastHit hit, enemyScope, layerMask))
                    if (hit.collider.GetComponent<PlayerController>())
                        return true;
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
    public void SlowDown() {
    }
}
