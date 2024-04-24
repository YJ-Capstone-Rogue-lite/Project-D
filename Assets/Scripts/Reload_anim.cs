using UnityEngine;
using UnityEngine.UI;


public class Reload_anim : MonoBehaviour
{
    public float animSpeed = 1.0f;
    public float reloadSpeed;
    [SerializeField] private Weapon_Slot Weapon_Slot;
    
    public Animator animator;

    private void Update()
    {
        reload_anim();

    }
    public void reload_anim()
    {
        if (PlayerChar.single == null)
            return;

        if(Weapon_Slot == null)
        {
            Weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();
        }

        if (Weapon_Slot == null)
            return;

        reloadSpeed = Weapon_Slot.activeWeaponSlot.GetComponent<FIre_Test>().weapon.reload_time;
        animSpeed = 1 / reloadSpeed;
        animator.SetFloat("Reload", animSpeed);
    }
}
