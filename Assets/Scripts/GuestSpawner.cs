using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuestSpawner : MonoBehaviour
{
    [Header("Guests")]
    [Tooltip("Prefab of the Guest GameObject.")]
    [SerializeField] private GameObject guestPrefab;
    [Tooltip("GameObject that will act as the Parent of the Guest GameObject.")]
    [SerializeField] private Transform guestParent;
    [Tooltip("Spawnpoint of the guests.")]
    [SerializeField] private NavigationInteraction spawnpoint;
    [Tooltip("Displays how many more guests will spawn.")]
    [SerializeField] private TextMeshProUGUI guestsText;

    [Header("Other")]
    [Tooltip("Location to walk towards when a guest spawns.")]
    [SerializeField] private NavigationInteraction desk;
    [Tooltip("Navigator to navigate the guests.")]
    [SerializeField] private Navigator navigator;
    [Tooltip("Used for current level information.")]
    [SerializeField] private LevelManager levelManager;

    private int guestsToServe;
    private Vector2 minMaxTimeNextGuest;
    private List<Guest> guests = new List<Guest>();

    #region Unity methods
    /// <summary>
    /// Get the current level if there is one and set local variables.
    /// </summary>
    private void Start()
    {
        if (levelManager.currentLevel)
        {
            guestsToServe = levelManager.currentLevel.amountOfGuests;
            guestsText.text = guestsToServe.ToString();
            minMaxTimeNextGuest = levelManager.currentLevel.minMaxTimeNextGuest;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Start Spawning guests by calling the SpawnNextGuest Coroutine.
    /// </summary>
    public void StartSpawningGuests()
    {
        StartCoroutine(SpawnNextGuest());
    }
    
    /// <summary>
    /// Stop the spawining of the guests.
    /// </summary>
    public void StopSpawningGuests()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Enable the guests to move.
    /// </summary>
    public void StartGuests()
    {
        foreach (Guest guest in guests)
        {
            guest.EnableMoving();
        }
    }

    /// <summary>
    /// Stop all the guests from moving.
    /// </summary>
    public void StopGuests()
    {
        foreach (Guest guest in guests)
        {
            guest.StopFromMoving();
        }
    }

    /// <summary>
    /// Removes Guest from the guest list.
    /// Check if the game can be ended.
    /// </summary>
    /// <param name="guest">Guest to Remove.</param>
    public void GuestLeft(Guest guest)
    {
        guests.Remove(guest);
        if (guests.Count <= 0)
        {
            levelManager.EndGame();
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Spawn Guest and have the guest walk to the desk.
    /// </summary>
    private void SpawnGuest()
    {
        guestsToServe -= 1;
        Vector2 position = spawnpoint.navigationPoint.position;
        GameObject guestObject = Instantiate(guestPrefab, position, Quaternion.Euler(Vector3.zero), guestParent);
        Guest guestScript = guestObject.GetComponent<Guest>();
        guests.Add(guestScript);
        guestScript.InitializedGuest(navigator, spawnpoint, this);
        guestScript.currentPosition = position;
        List<Vector2> positions = new List<Vector2>() { desk.navigationPoint.position };
        guestScript.SetRoute(positions, desk);
        guestsText.text = guestsToServe.ToString();
    }
    #endregion

    #region IEnumerator Methods
    /// <summary>
    /// Coroutine that spawns a guest and calls itself after waiting for a certain amount of time until no guests are left to spawn.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnNextGuest()
    {
        if (guestsToServe > 0)
        {
            SpawnGuest();
            float secondsToWait = Random.Range(minMaxTimeNextGuest.x, minMaxTimeNextGuest.y);
            yield return new WaitForSeconds(secondsToWait);
            StartCoroutine(SpawnNextGuest());
        }
    }
    #endregion
}
