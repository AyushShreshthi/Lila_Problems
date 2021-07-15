using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeapPickUpBehave : MonoBehaviour
{
    public PickupBase itemToPickup;
    WeapManager wm;
    Text UItext;
    bool initItem;

    WeaponToPick wpToPickup;

    public void Start()
    {
        
        wm = GetComponent<WeapManager>();
        UItext.gameObject.SetActive(false);
    }
    public void Update()
    {
        CheckItemType();

        ActualPickup();
    }

    private void ActualPickup()
    {
        if (Input.GetKey(KeyCode.X))
        {
            WeaponActualPickup();
        }
    }

    private void WeaponActualPickup()
    {
        if (wpToPickup != null)
        {
            WeaponReference targetWeapon = wm.ReturnWeaponWithID(wpToPickup.weaponID);

            if (targetWeapon != null)
            {
                wm.AvailableWeapons.Add(targetWeapon);
                if (wm.AvailableWeapons.Count > wm.maxWeapons)  //wm.maxweapons=3
                {
                    WeaponReference prevWeapon = wm.ReturnCurrentWeapon();

                    wm.AvailableWeapons.Remove(prevWeapon);

                    wm.SwitchWeaponWithTargetWeapon(targetWeapon);

                    if (prevWeapon.pickablePrefab != null)
                    {
                        Instantiate(prevWeapon.pickablePrefab,
                            (transform.position + transform.forward * 2 + Vector3.up),
                            Quaternion.Euler(0, 0, 90));
                    }
                }
            }
            Destroy(wpToPickup.gameObject);
            wpToPickup = null;
            itemToPickup = null;
        }
    }

    private void CheckItemType()
    {
        if (itemToPickup != null)
        {
            if (!initItem)
            {
                UItext.gameObject.SetActive(true);

                switch (itemToPickup.itemType)
                {
                    case PickupBase.ItemType.weapon:
                        WeaponItemPickup();
                        break;

                    
                    default:
                        break;
                }

                initItem = true;
            }
        }
        else
        {
            if (initItem)
            {
                initItem = false;
                wpToPickup = null;
                UItext.gameObject.SetActive(false);
            }
        }
    }

    

    private void WeaponItemPickup()
    {
        wpToPickup = itemToPickup.GetComponent<WeaponToPick>();

        string targetId = wpToPickup.weaponID;

        if (wm.AvailableWeapons.Count < wm.maxWeapons)
        {
            UItext.text = "Press X to Pick Up " + targetId;
        }
        else
        {
            UItext.text = "Press X to Switch " + wm.ReturnCurrentWeapon().weaponID + " with " + targetId;
        }
    }
}
