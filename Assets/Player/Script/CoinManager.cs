using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int currentCoins = 100; // Starting coins

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasEnoughCoins(int amount)
    {
        return currentCoins >= amount;
    }

    public void RemoveCoins(int amount)
    {
        currentCoins = Mathf.Max(0, currentCoins - amount);
        // Update UI here if needed
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        // Update UI here if needed
    }
}