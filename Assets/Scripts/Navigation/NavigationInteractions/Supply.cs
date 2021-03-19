using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : NavigationInteraction
{
    [Range(0, 1)]
    [SerializeField] private float secondsToWait;

    public override void NavInteract(GameObject gameObject)
    {
        Bellhop bellhop = gameObject.GetComponent<Bellhop>();
        StartCoroutine(GetSupplies(bellhop));
    }

    private IEnumerator GetSupplies(Bellhop bellhop)
    {
        yield return new WaitForSeconds(secondsToWait);
        bellhop.itemManager.AddItemToHands(ItemType.CleaningSupplies);
        bellhop.Interacted.Invoke();
    }
}
