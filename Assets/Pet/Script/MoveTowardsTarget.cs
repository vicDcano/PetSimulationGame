using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsTarget : MonoBehaviour
{
    /*public Transform player;
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = player.position;
    }*/
    public Transform[] targets; // Array to hold the targets that are transparent spheres
    private NavMeshAgent agent; //the pet that has his navmeshagent
    private int currentTargetIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("No waypoints assigned to the pet.");
            return;
        }

        // Start by moving to the first target
        MoveToNextTarget();
    }

    void Update()
    {
        // Check if the pet has reached the current target
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            MoveToNextTarget();
        }
    }

    void MoveToNextTarget()
    {
        // Choose a random target from the array
        currentTargetIndex = Random.Range(0, targets.Length);

        // Set the destination for the NavMeshAgent
        agent.SetDestination(targets[currentTargetIndex].position);
    }
}