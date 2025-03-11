using UnityEngine;
using UnityEngine.UI;

public class SecondaryTraitSelector : MonoBehaviour
{
    public VirtualPet pet;

    public void SetEarlyBird()
    {
        pet.ChangeTrait(SecondaryTrait.EarlyBird);
    }

    public void SetDayWalker()
    {
        pet.ChangeTrait(SecondaryTrait.DayWalker);
    }

    public void SetNightOwl()
    {
        pet.ChangeTrait(SecondaryTrait.NightOwl);
    }
}
