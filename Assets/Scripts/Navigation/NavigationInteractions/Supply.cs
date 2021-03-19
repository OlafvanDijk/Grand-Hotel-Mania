using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : NavigationInteraction
{
    [Tooltip("Time it takes to get the supplies.")]
    [Range(0, 1)]
    [SerializeField] private float secondsToWait;

    /// <summary>
    /// Give the bellhop cleaning supplies
    /// </summary>
    /// <param name="gameObject">GameObject Interacting with the NavigationInteraction.</param>
    public override void NavInteract(GameObject gameObject)
    {
        Bellhop bellhop = gameObject.GetComponent<Bellhop>();
        StartCoroutine(GetSupplies(bellhop));
    }

    /// <summary>
    /// Get supplies after waiting for a certain amount of time.
    /// </summary>
    /// <param name="bellhop">Bellhop script interacting with the supplies.</param>
    /// <returns></returns>
    private IEnumerator GetSupplies(Bellhop bellhop)
    {
        yield return new WaitForSeconds(secondsToWait);
        bellhop.itemManager.AddItemToHands(ItemType.CleaningSupplies);
        bellhop.Interacted.Invoke();
    }
}
