using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public Animator MainWeapon_Swap;
    public Animator SubWeapon_Swap;
    

    public bool current_Weapon; // true¸é MainWeapon false¸é SubWeapon

    private void Update()
    {
        if(Input.GetButtonDown("MainWeapon"))
        {
            Debug.Log("MainSwap");
            MainWeapon_Swap.SetBool("Current_Weapon", true);
            SubWeapon_Swap.SetBool("Current_Weapon", true);
        }
        else if (Input.GetButtonDown("SubWeapon"))
        {
            Debug.Log("SubSwap");
            MainWeapon_Swap.SetBool("Current_Weapon", false);
            SubWeapon_Swap.SetBool("Current_Weapon", false);
        }
    }
}
