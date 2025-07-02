using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;             // Invisible follow target behind player
    public Transform playerToLookAt;     // Actual player object (e.g., PlayerCapsule)

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }

        // 🔁 Rotate to face player (ignore vertical Y axis)
        if (playerToLookAt != null)
        {
            Vector3 direction = playerToLookAt.position - transform.position;
            direction.y = 0; // Keeps Leo upright

            if (direction.magnitude > 0.01f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }
}
