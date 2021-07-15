using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    ShootStates states;

    [HideInInspector]
    public Animator weaponAnim;
    [HideInInspector]
    public Animator modelAnim;
    [HideInInspector]
    public float fireRate;

    float timer;

    [HideInInspector]
    public Transform bulletSpawnPoint;
    [HideInInspector]
    public GameObject smokeParticle;
    [HideInInspector]
    public ParticleSystem[] muzzle;


    public GameObject casingPrefab; // as we have only one casing prefab

    [HideInInspector]
    public Transform caseSpawn;

    WeapManager weaponManager;

    public int magazineBullets = 0;
    public int curBullets = 30;
    public int carryingAmmo;

    public void Start()
    {
        states = GetComponent<ShootStates>();
    }

    bool shoot;
    bool dontShoot;

    bool emptyGun;
    float mouse1;
    public void Update()
    {
        mouse1 = Input.GetAxis("Fire1");
        if (mouse1 > 0.5f && !states.reloading)
        {
            states.shoot = true;
        }
        else
        {
            states.shoot = false;
        }
        Shooting();
    }

    private void Shooting()
    {
        shoot = states.shoot;

        if (modelAnim != null)
        {
            modelAnim.SetBool("Shoot", false);

            if (curBullets > 0)
            {
                modelAnim.SetBool("Empty", false);
            }
            else
            {
                modelAnim.SetBool("Empty", true);
            }
        }

        if (shoot)
        {
            if (timer <= 0)
            {
                if (modelAnim != null)
                {
                    modelAnim.SetBool("Shoot", false);
                }

                weaponAnim.SetBool("Shoot", false);

                if (curBullets > 0)
                {
                    emptyGun = false;

                    if (modelAnim != null)
                    {
                        modelAnim.SetBool("Shoot", true);
                    }

                    weaponAnim.SetBool("Shoot", true); 

                    
                    states.actualShooting = true;

                    RaycastShoot();

                    curBullets -= 1;
                    states.weaponManager.ReturnCurrentWeapon().weaponStats.curBullets = curBullets;
                }
                else
                {
                    if (emptyGun)
                    {
                        if (carryingAmmo > 0)
                        {
                           
                            int targetBullets = 0;

                            if (magazineBullets < carryingAmmo)
                            {
                                targetBullets = magazineBullets;
                            }
                            else
                            {
                                targetBullets = carryingAmmo;
                            }

                            carryingAmmo -= targetBullets;

                            curBullets = targetBullets;

                            states.weaponManager.ReturnCurrentWeapon().weaponStats.curBullets = curBullets;
                            states.weaponManager.ReturnCurrentWeapon().carryingAmmo = carryingAmmo;

                        }
                    }
                    else
                    {
                       emptyGun = true;
                    }
                }
                timer = fireRate;

            }
            else
            {
                states.actualShooting = false;

                weaponAnim.SetBool("Shoot", false);

            }
        }
        else
        {
           
            weaponAnim.SetBool("Shoot", false);

            states.actualShooting = false;
        }
    }

    private void RaycastShoot()
    {
        Vector3 direction = states.lookHitPosition - bulletSpawnPoint.position;
        RaycastHit hit;

        if (Physics.Raycast(bulletSpawnPoint.position, direction, out hit, 100, states.layerMask))
        {

            GameObject go = Instantiate(smokeParticle, hit.point, Quaternion.identity) as GameObject;
            go.transform.LookAt(bulletSpawnPoint.position);
            Debug.DrawRay(bulletSpawnPoint.position, direction, Color.blue);


        }
    }
}
