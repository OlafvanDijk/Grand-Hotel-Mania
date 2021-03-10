using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private int guestsToServe = 3;

    private void Awake()
    {
        
    }
}
