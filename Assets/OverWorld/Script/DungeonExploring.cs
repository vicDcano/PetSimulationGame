using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExploring : MonoBehaviour
{
    public float timerDuration = 10800f; // 3 hours in seconds
    public Transform caveDestination; // Where the player/NPC should go
    public GameObject player; // Reference to the player object
    public GameObject npc; // Reference to the NPC with NavMesh

    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pet"))
        {
            isPlayerInside = true;
            Debug.Log("Pet entered the cave.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plet"))
        {
            isPlayerInside = false;
            Debug.Log("Plet exited the cave.");
        }
    }

    public void StartTimer()
    {
        /* if (isPlayerInside)
         {
             Debug.Log("Timer started. Player is inside the cave.");
             StartCoroutine(WaitAndTeleport(timerDuration));
         }*/
        if (player != null && caveDestination != null)
        {
            // Teleport the player to the cave destination
            player.transform.position = caveDestination.position;
            player.transform.rotation = caveDestination.rotation;
            player.SetActive(false);
            Debug.Log("Player has been teleported to the cave.");
        }
        else
        {
            Debug.LogError("Player or cave destination is not assigned.");
        }
    }

    private System.Collections.IEnumerator WaitAndTeleport(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Teleport player to destination
        player.transform.position = caveDestination.position;
        player.transform.rotation = caveDestination.rotation;

        // Move NPC to destination using NavMesh
        if (npc != null)
        {
            npc.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(caveDestination.position);
        }

    }
}
