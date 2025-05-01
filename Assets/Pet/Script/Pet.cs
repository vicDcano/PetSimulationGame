using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetSystem
{
    //Creating a serialazable field to be able to manage the pets needs at a base level
    [Serializable]
    public class NeedsContainer
    {

        [Range(0f, 100f)]
        public float stats = 100f;
        public Needs needType;
        public float decayRate;
        public float bonusDecayRate = 0f;
        public float satisfationRate;

        public NeedsContainer(Needs needs) 
        {
            needType = needs;
        }

        public void AdjustDecayRate(PetPersonalityDataBase personality)
        {

            bonusDecayRate = 0f;

            // Apply bonus decay rate only to the specific need affected by the personality
            switch (needType)
            {
                case Needs.Hunger:
                    bonusDecayRate = personality.hungerRate;
                    break;

                case Needs.Energy:
                    bonusDecayRate = personality.energyRate;
                    break;

                case Needs.Fun:
                    bonusDecayRate = personality.playRate;
                    break;

                case Needs.Bladder:
                    bonusDecayRate = personality.bladderRate;
                    break;

                case Needs.Thirst:
                    bonusDecayRate = personality.thirstRate;
                    break;

                case Needs.Hygiene:
                    bonusDecayRate = personality.cleanRate;
                    break;
            }
        }

        public void Decay()
        {
            //stats -= decayRate * Time.deltaTime;
            stats -= (decayRate + bonusDecayRate) * Time.deltaTime;
            ClampState();
            //Debug.Log($"Decay: {needType}, Stats: {stats}, Decay Rate: {decayRate}");
        }

        private void ClampState()
        {
            stats = Mathf.Clamp(stats, 0f, 100f);
        }

        internal void Satisfaction()
        {
            stats += satisfationRate * Time.deltaTime;
            ClampState();
            //Debug.Log($"Satisfaction: {needType}, Stats: {stats}, Satisfaction Rate: {satisfationRate}");
        }
    }

    public enum Needs
    {
        Hunger,
        Energy,
        Bladder,
        Hygiene,
        Thirst,
        Fun
    }

    public class Pet: MonoBehaviour
    {
        public string Name;

        public List<NeedsContainer> needs;

        public const int CharacterNeedsCount = 6;

        [ContextMenu("Initialize Needs List")]
        /*public void InItNeeds()
        {
            needs = new List<NeedsContainer>();

            for(int i = 0; i < CharacterNeedsCount; i++)
            {
                needs.Add(new NeedsContainer((Needs)i));
            }
        }*/
        public void InItNeeds()
        {
            needs = new List<NeedsContainer>();

            for (int i = 0; i < CharacterNeedsCount; i++)
            {
                NeedsContainer need = new NeedsContainer((Needs)i);

                // Set individual decay rates for each need to go down over time
                switch ((Needs)i)
                {
                    case Needs.Hunger:
                        need.decayRate = 0.2f; 
                        break;
                    case Needs.Energy:
                        need.decayRate = 0.02f;
                        break;
                    case Needs.Fun:
                        need.decayRate = 0.09f; 
                        break;
                    case Needs.Bladder:
                        need.decayRate = 0.3f; 
                        break;
                    case Needs.Thirst:
                        need.decayRate = 0.08f; 
                        break;
                    case Needs.Hygiene:
                        need.decayRate = 0.07f; 
                        break;
                }

                needs.Add(need);
            }
        }

        private void Update()
        {
            NeedsProcess();
        }

        private void NeedsProcess()
        {
            for(int i = 0; i < needs.Count; i++) 
            {
                needs[i].Decay();
                needs[i].Satisfaction();
            }
        }

        public void ApplyPersonality(PetPersonalityDataBase personality)
        {
            foreach (var need in needs)
            {
                need.AdjustDecayRate(personality);
            }
        }
    }
}
