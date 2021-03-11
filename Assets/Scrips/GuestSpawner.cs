using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
public class GuestSpawner : MonoBehaviour
{
    [Header("Guests")]
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private Transform guestParent;
    [SerializeField] private Transform spawnpoint;

    [Header("Other")]
    [SerializeField] private Transform deskLocation;

    private LevelManager levelManager;

    private int guestsToServe;
    private Vector2 minMaxTimeNextGuest;

    /// <summary>
    /// Get the current level if there is one and set local variables
    /// </summary>
    private void Start()
    {
        //TODO SET UI (Guests to serve)

        levelManager = GetComponent<LevelManager>();
        if (levelManager.currentLevel)
        {
            guestsToServe = levelManager.currentLevel.amountOfGuests;
            minMaxTimeNextGuest = levelManager.currentLevel.minMaxTimeNextGuest;
            StartCoroutine(SpawnNextGuest());
        }
    }

    /// <summary>
    /// Spawn Guest and have the guest walk to the desk
    /// </summary>
    private void SpawnGuest()
    {
        guestsToServe -= 1;
        GameObject guest = Instantiate(guestPrefab, spawnpoint.position, Quaternion.Euler(Vector3.zero), guestParent);
        Guest guestScript = guest.GetComponent<Guest>();
        List<Vector2> positions = new List<Vector2>() { deskLocation.position };
        guestScript.WalkToAndInteractWith(positions, null);
    }

    /// <summary>
    /// Coroutine that spawns a guest and calls itself after waiting for a certain amount of time until no guests are left to spawn
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnNextGuest()
    {
        if (guestsToServe > 0)
        {
            SpawnGuest();
            float secondsToWait = Random.Range(minMaxTimeNextGuest.x, minMaxTimeNextGuest.y);
            yield return new WaitForSecondsRealtime(secondsToWait);
            StartCoroutine(SpawnNextGuest());
        }
    }
}
