using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeapManager : MonoBehaviour
{
    public int maxWeapons = 3;  // here we can add more to add maximum weapons player can hold
    public List<WeaponReference> AvailableWeapons = new List<WeaponReference>(); // available weapons at this time

    public int weaponIndex;
    public List<WeaponReference> Weapons = new List<WeaponReference>();
    WeaponReference currentWeapon;// current weapons right now
    ShootHandler handleShooting;
    [HideInInspector]
    public ShootStates states;


    public bool startUnarmed;
    public void Start()
    {
        states = GetComponent<ShootStates>();
        handleShooting = GetComponent<ShootHandler>();

        AvailableWeapons.Add(Weapons[weaponIndex]);
        weaponIndex = 0;

        CloseAllWeapons();
        SwitchWeapon(weaponIndex);

    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))   // input to switch weapons
        {
            if (weaponIndex < AvailableWeapons.Count - 1)
            {
                weaponIndex++;
            }
            else
            {
                weaponIndex = 0;
            }
            SwitchWeapon(weaponIndex);
        }

        
        foreach (WeaponReference w in Weapons)   // for holester weapons
        {
            if (w.holsterWeapon)
            {
                if (w.holsterWeapon.activeInHierarchy && !AvailableWeapons.Contains(w))
                {
                    w.holsterWeapon.SetActive(false);
                }
            }
        }

    }
    public void SwitchWeapon(int desiredIndex)
    {
        if (desiredIndex > AvailableWeapons.Count - 1)
        {
            desiredIndex = 0;
            weaponIndex = 0;
        }

        WeaponReference targetWeapon = ReturnWeaponWithID(AvailableWeapons[desiredIndex].weaponID);

        SwitchWeaponWithTargetWeapon(targetWeapon);

        weaponIndex = desiredIndex;
    }


    /// <summary>
    /// this is function can be used for inventory system , as we pick up any weapon we call this function with targetweapon parameter
    /// </summary>
    /// <param name="targetWeapon"></param>
    public void SwitchWeaponWithTargetWeapon(WeaponReference targetWeapon)
    {
        if (currentWeapon != null)
        {
            if (currentWeapon.weaponModel != null)
            {
                currentWeapon.weaponModel.SetActive(false);
                
            }
            if (currentWeapon.holsterWeapon)
            {
                currentWeapon.holsterWeapon.SetActive(true);
            }
        }

        WeaponReference newWeapon = targetWeapon;     //test


        if (newWeapon.holsterWeapon)
        {
            newWeapon.holsterWeapon.SetActive(false);
        }

        
        if (newWeapon.modelAnimator)
        {
            handleShooting.modelAnim = newWeapon.modelAnimator;
        }
        else
        {
            handleShooting.modelAnim = null;
        }

        handleShooting.fireRate = newWeapon.weaponStats.fireRate;

        
        handleShooting.bulletSpawnPoint = newWeapon.bulletSpawner;
        handleShooting.curBullets = newWeapon.weaponStats.curBullets;
        handleShooting.magazineBullets = newWeapon.weaponStats.maxBullets;

        handleShooting.carryingAmmo = newWeapon.carryingAmmo;

        if (newWeapon.weaponModel)
        {
            newWeapon.weaponModel.SetActive(true);
        }
        

        states.weaponAnimType = newWeapon.animType;

        currentWeapon = newWeapon;      //test
    }  
    private void CloseAllWeapons()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            
            if (Weapons[i].weaponModel)
            {
                Weapons[i].weaponModel.SetActive(false);
                

                if (Weapons[i].holsterWeapon)
                    Weapons[i].holsterWeapon.SetActive(false);
            }
        }
    }

    public WeaponReference ReturnWeaponWithID(string weaponID)
    {
        WeaponReference retVal = null;

        for (int i = 0; i < Weapons.Count; i++)
        {
            if (string.Equals(Weapons[i].weaponID, weaponID))
            {
                retVal = Weapons[i];
                break;
            }
        }
        return retVal;
    }

    public WeaponReference ReturnCurrentWeapon()
    {
        return currentWeapon;
    }


}
[System.Serializable]
public class WeaponReference
{
    public string weaponID;
    public Image image;
    public GameObject weaponModel;
    public Animator modelAnimator;
    public Transform lookTarget;
    public Transform bulletSpawner;
    public WeaponStat weaponStats;
    public int animType;

    public int carryingAmmo = 60;
    public int maxAmmo = 60;
    public GameObject pickablePrefab;

    public GameObject holsterWeapon;
}
[System.Serializable]
public class WeaponStat
{
    public int curBullets;
    public int maxBullets;
    public float fireRate;
    public AudioClip shootSound;
    //etc
}
