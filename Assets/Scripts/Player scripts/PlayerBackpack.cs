using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    public int maxNumberOfFruitsToStore = 50;
    public int currentNumberOfStoredFruits;

    public void AddFruits(int amount)
    {
        currentNumberOfStoredFruits += amount;
        if (currentNumberOfStoredFruits > maxNumberOfFruitsToStore)
        {
            currentNumberOfStoredFruits = maxNumberOfFruitsToStore;
        }
    }
    public int TakeFruits()
    {
        int takenFruits = currentNumberOfStoredFruits;
        currentNumberOfStoredFruits = 0;
        return takenFruits;
    }
}
