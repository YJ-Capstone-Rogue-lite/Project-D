using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public Animator MainWeapon_Swap;
    public Animator SubWeapon_Swap;
    public GameObject MainWeapon;
    public GameObject SubWeapon;
    

    public bool current_Weapon; // true¸é MainWeapon false¸é SubWeapon

    private void Update()
    {
        if(current_Weapon == true && Input.GetKeyDown("SubWeapon"))
        {
            current_Weapon = false;
            MainWeapon_Swap.SetFloat("MainWeapon_Swap", 1.0f);
            SubWeapon_Swap.SetFloat("SubWeapon_Swap", 1.0f);
        }
        else if (current_Weapon == false && Input.GetKeyDown("MainWeapon"))
        {
            current_Weapon = true;
            MainWeapon_Swap.SetFloat("MainWeapon_Swap", -1.0f);
            SubWeapon_Swap.SetFloat("SubWeapon_Swap", -1.0f);
        }
    }
}
