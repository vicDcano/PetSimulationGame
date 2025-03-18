using PetSystem;
using UnityEngine;
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

    void Start()
    {
        // Randomly assign a specific personality type (e.g., Foody)
        PersonalityType randomTrait = (PersonalityType)Random.Range(0, (int)PersonalityType.CleanFreak);

        // Random trait is selected and goes to the script where the personality traits done to the needs
        personality = PetPersonalityDataBase.GeneratePersonality(randomTrait);

        pet = GetComponent<Pet>();
        pet.InItNeeds();
        pet.ApplyPersonality(personality);

        UpdateSleepSchedule();

        // Debugging assigned traits and see the effects it is doing to the needs
        Debug.Log($"Personality Type: {randomTrait}");

        personalityCopy = randomTrait.ToString();

        string copyPersona = "Personality: " + personalityCopy;
        stringPersonality.text = copyPersona;
    }



    void UpdateSleepSchedule()
    {
        switch (currentTrait)
        {
            case SecondaryTrait.EarlyBird:
                wakeUpTime = 4;  // 4 AM
                sleepTime = 20;  // 8 PM (20 in 24-hour format)
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

        if(sleepTime > wakeUpTime)
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
