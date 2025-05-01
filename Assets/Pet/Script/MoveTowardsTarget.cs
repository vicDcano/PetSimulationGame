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
    /*public Transform[] targets; // Array to hold the targets that are transparent spheres
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
    }*/

    public Transform[] targets; // Waypoints for normal routine
    public float foodDetectionRange = 5f; // How far the pet can smell food
    public float eatingDuration = 3f; // How long it takes to eat

    private NavMeshAgent agent;
    private int currentTargetIndex = 0;
    private Transform currentFoodTarget;
    private bool isEating = false;
    private float eatingTimer = 0f;
    private Vector3 lastRoutinePosition; // Where we were before going for food

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ValidateTargets();
        MoveToNextTarget();
    }

    void Update()
    {
        if (isEating)
        {
            HandleEating();
            return;
        }

        // Check for food first (higher priority than routine)
        if (currentFoodTarget == null)
        {
            currentFoodTarget = FindNearbyFood();
            if (currentFoodTarget != null)
            {
                lastRoutinePosition = agent.destination;
                agent.SetDestination(currentFoodTarget.position);
            }
        }

        // Check if reached current target (either food or waypoint)
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            if (currentFoodTarget != null)
            {
                StartEating();
            }
            else
            {
                MoveToNextTarget();
            }
        }
    }

    private void ValidateTargets()
    {
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("No waypoints assigned to the pet.");
            enabled = false;
        }
    }

    private Transform FindNearbyFood()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, foodDetectionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Apple") || hitCollider.CompareTag("Peach"))
            {
                return hitCollider.transform;
            }
        }
        return null;
    }

    private void StartEating()
    {
        isEating = true;
        eatingTimer = 0f;
        agent.isStopped = true;

        // Trigger eating animation or effects here
        Debug.Log("Pet is eating " + currentFoodTarget.tag);

        // Destroy the food object
        Destroy(currentFoodTarget.gameObject);
        FindObjectOfType<InventoryManager>()?.OnItemConsumed(currentFoodTarget.gameObject);
    }

    private void HandleEating()
    {
        eatingTimer += Time.deltaTime;
        if (eatingTimer >= eatingDuration)
        {
            isEating = false;
            agent.isStopped = false;
            currentFoodTarget = null;

            // Return to routine by going back to where we were
            agent.SetDestination(lastRoutinePosition);
        }
    }

    private void MoveToNextTarget()
    {
        currentTargetIndex = Random.Range(0, targets.Length);
        agent.SetDestination(targets[currentTargetIndex].position);
    }

    // Visualize food detection range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, foodDetectionRange);
    }
}