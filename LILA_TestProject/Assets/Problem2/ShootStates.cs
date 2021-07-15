using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStates : MonoBehaviour
{
    public int weaponAnimType;
    public WeapManager weaponManager;

    public bool shoot;
    public bool reloading;
    public bool actualShooting;


    public Vector3 lookHitPosition;

    public LayerMask layerMask;


}
