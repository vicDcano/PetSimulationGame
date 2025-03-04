using UnityEngine;

[System.Serializable]
public class PetPersonalityDataBase
{
    public float hungerRate;    // How quickly the pet gets hungry
    public float socialRate;    // How much social interaction the pet needs
    public float energyRate;   // How active the pet is
    public float playRate;   // How much the pet likes to play
    

    // Method to acces a personality personality trait that will affect the needs
    public static PetPersonalityDataBase GeneratePersonality(PersonalityType randomTrait)
    {

        PetPersonalityDataBase personality = new PetPersonalityDataBase();

        switch (randomTrait)
        {
            case PersonalityType.Foody:
                personality.hungerRate = 2f; // Foody pets get hungry faster
                break;
            case PersonalityType.Clingy:
                personality.socialRate = 2f; // Clingy pets need more social interaction
                break;
            case PersonalityType.Lazy:
                personality.energyRate = 1f; // Lazy pets energy drain a bit faster
                break;
            case PersonalityType.Playful:
                personality.playRate = 2f; // Playful pets love to play
                break;
        }

        return personality;
    }
}