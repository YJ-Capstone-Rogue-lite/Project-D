using UnityEngine;
using UnityEngine.UI;

public class Reload_anim : MonoBehaviour
{
    public static Reload_anim Reload_anim_single;

    public float animSpeed = 1.0f;
    public float reloadSpeed;
    public Animator animator;

    [SerializeField] private Weapon_Slot Weapon_Slot;
    [SerializeField] private GameObject reload_object;

    private void Start()
    {
        reload_object.SetActive(true);
        Weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();

    }


    public void reload_anim()
    {

        if (PlayerChar.single == null)
            return;

        if (Weapon_Slot == null)
            return;

        Debug.Log("재장전 애니메이션 실행");
     
        reloadSpeed = Weapon_Slot.activeWeaponSlot.GetComponent<Fire_Test>().weapon.reload_time;
        animSpeed = 1 / reloadSpeed;
        animator.SetFloat("Reload", animSpeed);
        
    }

    public void ReloadAnimationEnd()
    {
        reload_object.SetActive(false);
        Debug.Log("재장전 오브젝트 꺼짐");
        reload_object.SetActive(true);
        Debug.Log("재장전 오브젝트 켜짐");

    }
}
