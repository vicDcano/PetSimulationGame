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
        public float stats;
        public Needs needType;

        public NeedsContainer(Needs needs) 
        {
            needType = needs;
        }
    }

    public enum Needs
    {
        Hunger,
        Energy,
        Bladder,
        Hygiene,
        Social,
        Fun
    }

    public class Pet: MonoBehaviour
    {
        public string Name;

        public List<NeedsContainer> needs;

        public const int CharacterNeedsCount = 6;

        [ContextMenu("Initialize Needs List")]
        public void InItNeeds()
        {
            needs = new List<NeedsContainer>();

            for(int i = 0; i < CharacterNeedsCount; i++)
            {
                needs.Add(new NeedsContainer((Needs)i));
            }
        }
    }
}
