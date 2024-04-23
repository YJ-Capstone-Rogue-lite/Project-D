using UnityEngine;
using UnityEngine.UI;


public class Reload_anim : MonoBehaviour
{
    public float animSpeed = 1.0f;
    public Weapon_Slot Weapon_Slot;
    public Animator animator;


  
    void Update()
    {
       

    }

    void reload_anim()
    {
        animSpeed = Weapon_Slot.activeWeaponSlot.GetComponent<FIre_Test>().weapon.reload_time;
        animator.SetFloat("Reload", animSpeed);
    }
}
