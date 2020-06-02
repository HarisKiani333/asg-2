using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_FSM : MonoBehaviour
{

    public enum EMENY_STATE { PATROL, CHASE, ATTACK }



    [SerializeField]
    private EMENY_STATE currentState;

    public EMENY_STATE CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            StopAllCoroutines();

            switch (currentState)
            {
                case EMENY_STATE.PATROL:
                    StartCoroutine(EnemyPatrol());
                    break;
                case EMENY_STATE.CHASE:
                    StartCoroutine(EnemyChase());
                    break;
                case EMENY_STATE.ATTACK:
                    StartCoroutine(EnemyAttack());
                    break;


            }


        }
    }
    private CheckMyVision checkVision;
    private UnityEngine.AI.NavMeshAgent agent = null;
    private Transform playerTransform = null;
    private Transform patrolDestination = null;

    private void Awake()
    {
        checkVision = GetComponent<CheckMyVision>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Dest");
        patrolDestination = destinations[Random.Range(0, destinations.Length)].GetComponent<Transform>();

        currentState = EMENY_STATE.PATROL;

    }

    public IEnumerator EnemyPatrol()
    {
        while (currentState == EMENY_STATE.PATROL)
        {
            checkVision.Sensitity = CheckMyVision.enmSensitity.HIGH;

            agent.isStopped = false;
            agent.SetDestination(patrolDestination.position);
            while (agent.pathPending)
            {
                yield return null;
            }
            if (checkVision.targetInSight)
            {
                agent.isStopped = true;
                currentState = EMENY_STATE.CHASE;
                yield break;
            }
            yield break;
        }
    }
    public IEnumerator EnemyChase()
    {


        yield break;
    }
    public IEnumerator EnemyAttack()
    {
        yield break;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
