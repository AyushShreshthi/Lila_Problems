using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    public ItemType itemType;

    public enum ItemType
    {
        weapon,
        etc
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<WeapPickUpBehave>())
            other.transform.GetComponent<WeapPickUpBehave>().itemToPickup = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<WeapPickUpBehave>())
            other.transform.GetComponent<WeapPickUpBehave>().itemToPickup = null;
    }
}
