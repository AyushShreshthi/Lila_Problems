using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhud : MonoBehaviour
{
    WeapManager wm;

    public Image image; // current weapon image in the scene canvas gameobject
    public Text ammos; // current weapon's ammo in the scene canvas gameobject

    private void Start()
    {
        wm = GetComponent<WeapManager>();
    }
    /// <summary>
    /// if we want to use gui 
    /// </summary>
    private void OnGUI()
    {
        GUIContent content = new GUIContent();
        content.image = wm.ReturnCurrentWeapon().image.mainTexture;
        content.text = wm.ReturnCurrentWeapon().weaponStats.curBullets.ToString() + " / " +
            wm.ReturnCurrentWeapon().weaponStats.maxBullets.ToString();

        GUI.Box(new Rect(Screen.width - 200f, Screen.height - 200f, 150f, 70f), content);
    }

    /// <summary>
    /// if we want to use UI canvas image and text
    /// </summary>
    /*private void Update()
    {
        image = wm.ReturnCurrentWeapon().image;
        ammos.text = wm.ReturnCurrentWeapon().weaponStats.curBullets.ToString() + " / " +
            wm.ReturnCurrentWeapon().weaponStats.maxBullets.ToString();

    }*/
}
