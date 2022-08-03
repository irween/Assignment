using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform[] points;

    private int destPoint = 0;
    private bool seenPlayer = false;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);       

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            seenPlayer = true;

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

        if (distance >= lookRadius)
        {
            seenPlayer = false;
        } 

        if (!agent.pathPending && agent.remainingDistance < 0.5f && seenPlayer == false)
                GoToNextPoint();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void GoToNextPoint() {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.SetDestination(points[destPoint].position);

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
