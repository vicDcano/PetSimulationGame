using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonExploring : MonoBehaviour
{
    [Header("Exploration Settings")]
    public float timerDuration = 10800f; // 3 hours in seconds (10800)
    public Transform caveEntrance; // Where the pet should walk to
    public GameObject pet; // Reference to the pet object
    public float walkingSpeed = 3.5f; // Walking speed to cave
    public float explorationSpeed = 1.0f; // Slower speed while exploring

    [Header("Reward Settings")]
    public GameObject[] possibleItems;
    public int minMoney = 10;
    public int maxMoney = 100;
    [Range(0f, 1f)] public float itemChance = 0.3f;

    private NavMeshAgent petAgent;
    private Vector3 originalPosition;
    private bool isWalkingToCave = false;
    private bool isExploring = false;

    private void Start()
    {
        if (pet != null)
        {
            petAgent = pet.GetComponent<NavMeshAgent>();
            originalPosition = pet.transform.position;
        }
    }

    public void StartJourneyToCave()
    {
        if (!isWalkingToCave && !isExploring && pet != null && caveEntrance != null && petAgent != null)
        {
            isWalkingToCave = true;
            petAgent.speed = walkingSpeed;
            petAgent.SetDestination(caveEntrance.position);
            StartCoroutine(MonitorJourneyToCave());
        }
    }

    private IEnumerator MonitorJourneyToCave()
    {
        Debug.Log("Pet started walking to cave...");

        // Wait until pet reaches the cave
        while (petAgent.pathPending || petAgent.remainingDistance > petAgent.stoppingDistance)
        {
            yield return null;
        }

        // Pet has arrived at cave
        Debug.Log("Pet arrived at cave entrance");
        isWalkingToCave = false;
        StartExploration();
    }

    private void StartExploration()
    {
        if (!isExploring)
        {
            isExploring = true;
            petAgent.speed = explorationSpeed; // Slow down while exploring
            StartCoroutine(ExplorationTimer());
        }
    }

    private IEnumerator ExplorationTimer()
    {
        Debug.Log("Pet started exploring the cave...");

        float startTime = Time.time;
        while (Time.time - startTime < timerDuration)
        {
            // Make the pet wander around inside the cave
            if (Random.value < 0.1f) // 10% chance each frame to change direction
            {
                Vector3 randomDirection = Random.insideUnitSphere * 5f;
                randomDirection += caveEntrance.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
                {
                    petAgent.SetDestination(hit.position);
                }
            }
            yield return null;
        }

        Debug.Log("Pet finished exploring!");
        GiveReward();
        ReturnHome();
    }

    private void GiveReward()
    {
        int moneyFound = Random.Range(minMoney, maxMoney + 1);
        Debug.Log("Pet found $" + moneyFound + " during exploration!");

        if (Random.value <= itemChance && possibleItems.Length > 0)
        {
            GameObject foundItem = possibleItems[Random.Range(0, possibleItems.Length)];
            Debug.Log("Pet found a rare item: " + foundItem.name);
        }
    }

    private void ReturnHome()
    {
        isExploring = false;
        petAgent.speed = walkingSpeed;
        petAgent.SetDestination(originalPosition);
        StartCoroutine(MonitorReturnHome());
    }

    private IEnumerator MonitorReturnHome()
    {
        Debug.Log("Pet is returning home...");

        while (petAgent.pathPending || petAgent.remainingDistance > petAgent.stoppingDistance)
        {
            yield return null;
        }

        Debug.Log("Pet has returned home");
    }

    // Public method to cancel the exploration
    public void CancelExploration()
    {
        if (isWalkingToCave || isExploring)
        {
            StopAllCoroutines();
            isWalkingToCave = false;
            isExploring = false;
            petAgent.SetDestination(originalPosition);
            Debug.Log("Exploration was cancelled");
        }
    }
}
