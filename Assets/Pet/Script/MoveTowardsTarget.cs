using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = player.position;
    }
}