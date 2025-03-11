using UnityEngine;

[System.Serializable]
public class PetPersonalityDataBase
{
    public float hungerRate;    // How quickly the pet gets hungry
    public float energyRate;   // How active the pet is
    public float playRate;   // How much the pet likes to play
    public float bladderRate;
    public float thirstRate;
    public float cleanRate;
    

    // Method to acces a personality personality trait that will affect the needs
    public static PetPersonalityDataBase GeneratePersonality(PersonalityType randomTrait)
    {

        PetPersonalityDataBase personality = new PetPersonalityDataBase();

        switch (randomTrait)
        {
            case PersonalityType.Foody:
                personality.hungerRate = 0.02f; // Foody pets get hungry faster
                break;

            case PersonalityType.Lazy:
                personality.energyRate = 0.02f; // Lazy pet's energy drain a bit faster
                break;

            case PersonalityType.Playful:
                personality.playRate = 0.07f; // Playful pets love to play
                break;
            case PersonalityType.Bedwetter:
                personality.bladderRate = 0.04f; // Bedwetter pet has a short bladder
                break;
            case PersonalityType.DryMouth:
                personality.thirstRate = 0.02f; // DryMouth pet gets easier of wanting water
                break;
            case PersonalityType.CleanFreak:
                personality.cleanRate = 0.4f; // CleanFreak pet loves to be clean
                break;
        }

        return personality;
    }
}