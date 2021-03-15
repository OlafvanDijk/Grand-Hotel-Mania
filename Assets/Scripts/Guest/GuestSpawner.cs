using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
public class GuestSpawner : MonoBehaviour
{
    [Header("Guests")]
    [Tooltip("Prefab of the Guest GameObject")]
    [SerializeField] private GameObject guestPrefab;
    [Tooltip("GameObject that will act as the Parent of the Guest GameObject")]
    [SerializeField] private Transform guestParent;
    [Tooltip("Spawnpoint of the guests")]
    [SerializeField] private NavigationPoint spawnpoint;

    [Header("Other")]
    [Tooltip("Location to walk towards when a guest spawns")]
    [SerializeField] private Transform deskLocation;

    private LevelManager levelManager;

    private int guestsToServe;
    private Vector2 minMaxTimeNextGuest;
    private List<Guest> guests = new List<Guest>();

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
        }
    }

    /// <summary>
    /// Start Spawning guests by calling the SpawnNextGuest Coroutine
    /// </summary>
    public void StartSpawningGuests()
    {
        StartCoroutine(SpawnNextGuest());
    }

    /// <summary>
    /// Start Spawning guests by stopping all the Coroutines
    /// </summary>
    public void StopGuests()
    {
        StopAllCoroutines();
        foreach (Guest guest in guests)
        {
            guest.StopFromMoving();
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
        guests.Add(guestScript);
        guestScript.SetCurrentPosition(spawnpoint.position);
        List<Vector2> positions = new List<Vector2>() { deskLocation.position };
        guestScript.SetRoute(positions);
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
