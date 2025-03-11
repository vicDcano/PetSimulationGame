using PetSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsPanel : MonoBehaviour
{
    [SerializeField] List<NeedsUI> children;
    [SerializeField] Pet testCharacter;

    // Update is called once per frame
    void Update()
    {
        if (testCharacter != null)
        {
            UpdatePanel(testCharacter);
        }
        else
        {
            //Debug.LogError("Test character is not assigned.");
        }
    }

    public void UpdatePanel(Pet testCharacter)
    {
        for(int i = 0; i < children.Count; i++) 
        {
            if (i < testCharacter.needs.Count)
            {
                children[i].SetStatus(testCharacter.needs[i].stats);
            }
            else
            {
                //Debug.LogError("Needs list is shorter than children list.");
            }
        }
    }
}
