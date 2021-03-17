using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : NavigationInteraction
{
    public override void NavInteract(GameObject gameObject)
    {
        Bellhop bellhop = gameObject.GetComponent<Bellhop>();
        StartCoroutine(GetSupplies(bellhop));
    }

    private IEnumerator GetSupplies(Bellhop bellhop)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        bellhop.AddItemToHands(ItemType.CleaningSupplies);
        bellhop.Interacted.Invoke();
    }
}
