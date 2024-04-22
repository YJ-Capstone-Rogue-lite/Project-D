using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public Animator MainWeapon_Swap;
    public Animator SubWeapon_Swap;

    [SerializeField] private GameObject reloading;
    [SerializeField] private GameObject player;
    


    public bool MainWeapon = false; // true면 MainWeapon false면 SubWeapon
    public bool SubWeapon = true; // true면 MainWeapon false면 SubWeapon

    private void Update()
    {
        if (Input.GetButtonDown("MainWeapon") && MainWeapon == true)
        {
            MainWeapon_Swap.SetBool("MainWeapon_Up", true);
            SubWeapon_Swap.SetBool("SubWeapon_Down", true);
            MainWeapon_Swap.SetBool("MainWeapon_Down", false);
            SubWeapon_Swap.SetBool("SubWeapon_Up", false);
            MainWeapon = false;
            SubWeapon = true;
            Debug.Log("MainClick");
        }
        else if (Input.GetButtonDown("SubWeapon") && SubWeapon == true)
        {
            MainWeapon_Swap.SetBool("MainWeapon_Down", true);
            SubWeapon_Swap.SetBool("SubWeapon_Up", true);
            MainWeapon_Swap.SetBool("MainWeapon_Up", false);
            SubWeapon_Swap.SetBool("SubWeapon_Down", false);
            MainWeapon = true;
            SubWeapon = false;
            Debug.Log("SubClick");
        }
        reloading.transform.position = player.transform.position;
    }
    
}
