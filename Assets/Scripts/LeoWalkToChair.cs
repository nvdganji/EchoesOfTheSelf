using UnityEngine;
using UnityEngine.AI;

public class LeoWalkToChair : MonoBehaviour
{
    public Transform[] waypoints;        // Waypoints Leo will walk to first
    public Transform chairTarget;        // Empty GameObject at chair seat (final sit position)
    public Animator animator;            // Animator with walk/idle/sit animations
    public string sitTriggerName = "Sit";

    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    private bool isSitting = false;
    private bool goingToChair = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
        else
        {
            // If no waypoints, go straight to the chair
            GoToChair();
        }
    }

    void Update()
    {
        if (isSitting) return;

        // Move to next waypoint or chair
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!goingToChair && currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
                agent.SetDestination(waypoints[currentWaypoint].position);
            }
            else if (!goingToChair)
            {
                GoToChair();
            }
            else
            {
                SitDown();
            }
        }

        // Set Speed parameter for walk animation
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    void GoToChair()
    {
        goingToChair = true;
        Debug.Log("🪑 Heading to chair...");
        agent.SetDestination(chairTarget.position);
    }

    void SitDown()
    {
        isSitting = true;
        agent.isStopped = true;

        // Snap Leo to exact position and rotation of chairTarget
        transform.position = chairTarget.position;
        transform.rotation = chairTarget.rotation;

        Debug.Log("🧍➡️🪑 Sitting down...");
        animator.SetTrigger(sitTriggerName); // Make sure your Animator has this trigger
    }
}
