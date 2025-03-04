using PetSystem;
using UnityEngine;

public class VirtualPet : MonoBehaviour
{
    public PetPersonalityDataBase personality;

    // Status Needs
    public float hunger = 100f;       // Hunger level (0 = starving, 100 = full)
    public float happiness = 100f;   // Happiness level (0 = sad, 100 = happy)
    public float energy = 100f;      // Energy level (0 = exhausted, 100 = fully rested)

    void Start()
    {
        // Randomly assign a specific personality type (e.g., Foody)
        PersonalityType randomTrait = (PersonalityType)Random.Range(0, (int)PersonalityType.Playful);

        // Random trait is selected and goes to the script where the personality traits done to the needs
        personality = PetPersonalityDataBase.GeneratePersonality(randomTrait);

        // Debugging assigned traits and see the effects it is doing to the needs
        Debug.Log($"Personality Type: {randomTrait}");
        Debug.Log($"Hunger Rate: {personality.hungerRate}");
        Debug.Log($"Social Need: {personality.socialRate}");
        Debug.Log($"Energy Level: {personality.energyRate}");
        Debug.Log($"Playfulness: {personality.playRate}");
    }

    void Update()
    {
        // Update hunger based on hungerRate
        hunger -= Time.deltaTime * personality.hungerRate;

        // Update happiness based on socialNeed (pet loses happiness if ignored)
        if (IsIgnored())
        {
            happiness -= Time.deltaTime * personality.socialRate;
        }

        // Update energy based on energyLevel
        energy -= Time.deltaTime * (1f / personality.energyRate);

        // Clamp values to keep them within bounds
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);
        energy = Mathf.Clamp(energy, 0f, 100f);
    }

    // Pet is ignored if no interaction happens for a while
    private bool IsIgnored()
    {
        
        return true; // For now, assume the pet is always ignored
    }
}
