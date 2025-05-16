using PetSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class VirtualPet : MonoBehaviour
{
    public PetPersonalityDataBase personality;
    private Pet pet;

    string personalityCopy;

    public SecondaryTrait currentTrait;

    public TMP_Text stringPersonality;

    private int wakeUpTime;
    private int sleepTime;

    // Sleep system variables
    public Transform bedLocation; // Assign this in the inspector
    private NavMeshAgent agent;
    private MoveTowardsTarget movementScript;
    private bool isSleeping = false;
    private bool isGoingToBed = false;
    private Vector3 lastPositionBeforeSleep;

    // Need restoration parameters
    public float energyRestoreMultiplier = 2f; // Multiplier for energy restoration while sleeping
    public float hungerDecayReduction = 0.5f; // Reduces hunger decay while sleeping
    public float funDecayReduction = 0.3f; // Reduces fun decay while sleeping
    private float[] originalDecayRates = new float[6]; // To store original decay rates
    private float needUpdateInterval = 1f; // Update needs every second
    private float needUpdateTimer = 0f;

    void Start()
    {
        // Randomly assign a specific personality type (e.g., Foody)
        PersonalityType randomTrait = (PersonalityType)Random.Range(0, (int)PersonalityType.CleanFreak);

        // Random trait is selected and goes to the script where the personality traits done to the needs
        personality = PetPersonalityDataBase.GeneratePersonality(randomTrait);

        pet = GetComponent<Pet>();
        pet.InItNeeds();
        pet.ApplyPersonality(personality);

        // Get required components
        agent = GetComponent<NavMeshAgent>();
        movementScript = GetComponent<MoveTowardsTarget>();

        // Store original decay rates
        StoreOriginalDecayRates();

        UpdateSleepSchedule();

        // Debugging assigned traits and see the effects it is doing to the needs
        Debug.Log($"Personality Type: {randomTrait}");

        personalityCopy = randomTrait.ToString();

        string copyPersona = "Personality: " + personalityCopy;
        stringPersonality.text = copyPersona;
    }

    void StoreOriginalDecayRates()
    {
        for (int i = 0; i < pet.needs.Count; i++)
        {
            originalDecayRates[i] = pet.needs[i].decayRate;
        }
    }

    void Update()
    {
        // Check if it's sleep time and handle sleep behavior
        if (!IsAwake() && !isSleeping && !isGoingToBed)
        {
            StartGoingToBed();
        }
        else if (IsAwake() && isSleeping)
        {
            WakeUp();
        }

        // If we're going to bed, check if we've arrived
        if (isGoingToBed && !isSleeping && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartSleeping();
        }

        // Handle need restoration while sleeping
        if (isSleeping)
        {
            needUpdateTimer += Time.deltaTime;
            if (needUpdateTimer >= needUpdateInterval)
            {
                AdjustSleepNeeds();
                needUpdateTimer = 0f;
            }
        }
    }

    void AdjustSleepNeeds()
    {
        // Modify decay rates while sleeping
        foreach (var need in pet.needs)
        {
            switch (need.needType)
            {
                case Needs.Energy:
                    // Energy restores faster while sleeping
                    need.satisfationRate = originalDecayRates[(int)Needs.Energy] * energyRestoreMultiplier;
                    break;

                case Needs.Hunger:
                    // Hunger decays slower while sleeping
                    need.decayRate = originalDecayRates[(int)Needs.Hunger] * hungerDecayReduction;
                    break;

                case Needs.Fun:
                    // Fun decays slower while sleeping
                    need.decayRate = originalDecayRates[(int)Needs.Fun] * funDecayReduction;
                    break;

                default:
                    // Reset other needs to original rates
                    need.decayRate = originalDecayRates[(int)need.needType];
                    need.satisfationRate = 0f;
                    break;
            }
        }

        Debug.Log($"Sleeping - Energy satisfaction: {pet.needs[(int)Needs.Energy].satisfationRate}, " +
                 $"Hunger decay: {pet.needs[(int)Needs.Hunger].decayRate}");
    }

    void ResetNeedRates()
    {
        // Restore all needs to their original rates
        for (int i = 0; i < pet.needs.Count; i++)
        {
            pet.needs[i].decayRate = originalDecayRates[i];
            pet.needs[i].satisfationRate = 0f;
        }
    }

    void StartGoingToBed()
    {
        if (bedLocation == null)
        {
            Debug.LogWarning("No bed location assigned!");
            return;
        }

        // Disable normal movement script
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        // Remember where we were
        lastPositionBeforeSleep = transform.position;

        // Set destination to bed
        agent.SetDestination(bedLocation.position);
        isGoingToBed = true;

        Debug.Log("It's bedtime! Going to bed...");
    }

    void StartSleeping()
    {
        isGoingToBed = false;
        isSleeping = true;

        // Stop movement
        agent.isStopped = true;

        // Adjust need rates for sleep
        AdjustSleepNeeds();

        Debug.Log("Pet is now sleeping and needs are adjusting");
    }

    void WakeUp()
    {
        isSleeping = false;

        // Reset all need rates to normal
        ResetNeedRates();

        // Enable movement script
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }

        // Resume movement
        agent.isStopped = false;

        // Play wake animation (you'll need to implement this)
        // animator.SetTrigger("WakeUp");

        Debug.Log("Pet woke up! Need rates restored to normal");
    }

    void UpdateSleepSchedule()
    {
        switch (currentTrait)
        {
            case SecondaryTrait.EarlyBird:
                wakeUpTime = 4;  // 4 AM
                sleepTime = 16;  // 8 PM (20 in 24-hour format)
                break;
            case SecondaryTrait.DayWalker:
                wakeUpTime = 6;  // 6 AM
                sleepTime = 22;  // 10 PM (22 in 24-hour format)
                break;
            case SecondaryTrait.NightOwl:
                wakeUpTime = 9;  // 9 AM
                sleepTime = 1;   // 1 AM (1 in 24-hour format)
                break;
        }

        Debug.Log($"Pet's wake-up time: {wakeUpTime}:00, Sleep time: {sleepTime}:00");
    }

    public void ChangeTrait(SecondaryTrait newTrait)
    {
        currentTrait = newTrait;
        UpdateSleepSchedule();
    }

    public bool IsAwake()
    {
        int currentHour = System.DateTime.Now.Hour;

        if (sleepTime > wakeUpTime)
        {
            return currentHour >= wakeUpTime && currentHour < sleepTime;
        }
        else
        {
            return currentHour >= wakeUpTime || currentHour < sleepTime;
        }
    }

    // Pet is ignored if no interaction happens for a while
    private bool IsIgnored()
    {
        return true; // For now, assume the pet is always ignored
    }
}