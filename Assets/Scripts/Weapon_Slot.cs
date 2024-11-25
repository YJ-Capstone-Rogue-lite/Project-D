using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Slot : MonoBehaviour
{
    public static Weapon_Slot weapon_slot_instance;
    public GameObject weaponSlot1; // 무기 슬롯 1 게임 오브젝트
    public GameObject weaponSlot2; // 무기 슬롯 2 게임 오브젝트


    public Fire_Test weapon_slot2_weapon;
    public Fire_Test activeWeaponSlot_Component;


    public GameObject activeWeaponSlot; // 활성화된 무기 슬롯 게임 오브젝트
    
    public float magazineCapacitySlot1; // 슬롯 1의 장탄수
    public float magazineCapacitySlot2; // 슬롯 2의 장탄수


    public bool isReloadingSlot1 = false;
    public bool isReloadingSlot2 = false;

    public float animSpeed = 1.0f;
    public float reloadSpeed;
    public AudioClip Reload_AudioClip;
    public Animator reload_animator;

    [SerializeField] private GameObject reload_object;
    public PlayerChar playerchar;

    [Header("오디오 소스")]
    public AudioClip Slot1_Swap_Souond;
    public AudioClip Slot2_Swap_Souond;
    private AudioSource audioSource;

    private void Start()
    {
        Debug.Log(weapon_slot2_weapon);
        Debug.Log(activeWeaponSlot_Component);
        // 게임 시작시 기본 무기 슬롯 설정
        EnableWeaponSlot(weaponSlot1);
        reload_object.SetActive(true);

        // weaponSlot2에서 Fire_Test 컴포넌트를 가져와 fire_test 변수에 할당합니다.
        weapon_slot2_weapon = weaponSlot2.GetComponent<Fire_Test>();
        activeWeaponSlot_Component = activeWeaponSlot.GetComponent<Fire_Test>();
        activeWeaponSlot_Component.Init();
        weapon_slot2_weapon.Init();

        //게임 시작시 장탄수 한번 초기화
        UpdateMagazineCapacity();

        audioSource = gameObject.AddComponent<AudioSource>();


    }

    private void Update()
    {
        slotchange();
        Reload_Weapon();
    }

 

    public void Reload_Weapon()
    {
        if (Input.GetButtonDown("Reloading"))
        {
            Reload(activeWeaponSlot);
            Debug.Log("R 키 누름" + activeWeaponSlot.name);
            //리로드 스크립트 실행시 리로드 애니메이션 작동
            IngameUI.single.activate_Magazine_Update();

        }
    }

    // 무기를 슬롯에 할당하는 메서드
    public void ReceiveWeapon(Weapon weapon)
    {
        // 활성화된 무기 슬롯이 있는지 확인
        if (activeWeaponSlot != null)
        {
            // 슬롯 2이 비어있는 경우에만 무기를 할당합니다.
            if (weaponSlot2.GetComponent<Fire_Test>().weapon.weaponType == Weapon.WeaponType.None)
            {
                weaponSlot2.GetComponent<Fire_Test>().weapon = weapon;
                slot2_whenPickup_magazinUpdate();
            }
            else
            {
                // 무기를 현재 활성화된 슬롯에 할당
                activeWeaponSlot.GetComponent<Fire_Test>().weapon = weapon;
                UpdateMagazineCapacity();
            }
        }
        else
        {
            Debug.LogError("활성화된 무기 슬롯이 없습니다.");
        }
    }

 
    //슬롯 변경 기능
    void slotchange()
    {
        // 마우스 휠을 사용하여 무기 슬롯 변경(좀 이상해서 비활성화)
        //float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        //if (mouseWheel > 0f)
        //{
        //    // 마우스 휠을 위로 스크롤할 때
        //    SwitchToNextSlot();
        //}
        //else if (mouseWheel < 0f)
        //{
        //    // 마우스 휠을 아래로 스크롤할 때
        //    SwitchToPreviousSlot();
        //}

        // 숫자 1, 2 키를 사용하여 무기 슬롯 변경
        if (Input.GetKeyDown(("1")))
        {
            EnableWeaponSlot(weaponSlot1);
            audioSource.PlayOneShot(Slot1_Swap_Souond);

        }
        else if (Input.GetKeyDown(("2")))
        {
            EnableWeaponSlot(weaponSlot2);
            audioSource.PlayOneShot(Slot2_Swap_Souond);

        }
    }

    // 다음 무기 슬롯으로 변경
    //private void SwitchToNextSlot()
    //{
    //    if (activeWeaponSlot == weaponSlot1)
    //    {
    //        EnableWeaponSlot(weaponSlot2);
    //    }
    //    else if (activeWeaponSlot == weaponSlot2)
    //    {
    //        EnableWeaponSlot(weaponSlot1);
    //    }
    //}

    //// 이전 무기 슬롯으로 변경
    //private void SwitchToPreviousSlot()
    //{
    //    if (activeWeaponSlot == weaponSlot1)
    //    {
    //        EnableWeaponSlot(weaponSlot2);
    //    }
    //    else if (activeWeaponSlot == weaponSlot2)
    //    {
    //        EnableWeaponSlot(weaponSlot1);
    //    }
    //}

    // 무기 슬롯을 활성화하는 메서드
    public void EnableWeaponSlot(GameObject weaponSlot)
    {
        // 선택된 무기 슬롯을 활성화하고 다른 슬롯은 비활성화
        weaponSlot.SetActive(true);
        activeWeaponSlot = weaponSlot; // 활성화된 무기 슬롯을 업데이트합니다.
        if (weaponSlot == weaponSlot1)
        {
            weaponSlot2.SetActive(false);
        }
        else if (weaponSlot == weaponSlot2)
        {
            weaponSlot1.SetActive(false);
        }

    }

    public void slot2_whenPickup_magazinUpdate()
    {
        magazineCapacitySlot2 = weapon_slot2_weapon.weapon.backup_magazine_capacity;

    }

    //장탄수 업데이트
    public void UpdateMagazineCapacity()
    {
        // 현재 활성화된 슬롯의 장탄수를 설정
        if (activeWeaponSlot == weaponSlot1)
        {
            magazineCapacitySlot1 = activeWeaponSlot_Component.weapon.backup_magazine_capacity;
        }
        else if (activeWeaponSlot == weaponSlot2)
        {
            magazineCapacitySlot2 = weapon_slot2_weapon.weapon.backup_magazine_capacity;
        }
    }

    // 무기 슬롯의 장탄수 감소 메서드
    public void DecreaseMagazineCapacity(GameObject slot)
    {
    
        if (slot == weaponSlot1)
        {
            magazineCapacitySlot1 -= 1;

            if (magazineCapacitySlot1 <= 0)
            {

                Reload(slot);

            }
        }
        else if (slot == weaponSlot2)
        {
            magazineCapacitySlot2 -= 1;

            if (magazineCapacitySlot2 <= 0)
            {
                Reload(slot);
            }
        }
        else
        {
            Debug.LogError("해당 무기 슬롯의 장탄수를 찾을 수 없습니다.");
        }
    }

    


    // 재장전 메서드
    public void Reload(GameObject slot)
    {

        // slot1에 대한 재장전 중복 방지
        if (slot == weaponSlot1 && !isReloadingSlot1)
        {
            StartCoroutine(ReloadCoroutine(slot, 1));

        }
        // slot2에 대한 재장전 중복 방지
        else if (slot == weaponSlot2 && !isReloadingSlot2)
        {
            StartCoroutine(ReloadCoroutine(slot, 2));
        }

        reloadSpeed = activeWeaponSlot.GetComponent<Fire_Test>().weapon.reload_time;
        Reload_AudioClip = activeWeaponSlot.GetComponent<Fire_Test>().weapon.AudioClip;
        animSpeed = 1 / reloadSpeed;
        reload_animator.SetFloat("Reload", animSpeed);
        audioSource.PlayOneShot(Reload_AudioClip);
        Debug.Log(Reload_AudioClip.name + " 재장전 소리 재생");

        Debug.Log(reloadSpeed + " 초 동안 재장전 애니메이션 실행");

    }


    private IEnumerator ReloadCoroutine(GameObject slot, int slotNumber)
    {
        Fire_Test slot_componet = slot.GetComponent<Fire_Test>();
        reload_object.SetActive(true);
        Debug.Log("재장전 오브젝트 켜짐");

        // 코루틴 실행 상태 업데이트
        if (slotNumber == 1) isReloadingSlot1 = true;
        else if (slotNumber == 2) isReloadingSlot2 = true;

        Debug.Log("무기 슬롯 재장전 시작: " + slot.name);
        yield return new WaitForSeconds(slot_componet.weapon.reload_time);

        if (slot == weaponSlot1)
        {
            magazineCapacitySlot1 = slot_componet.weapon.backup_magazine_capacity;
            Debug.Log("무기 슬롯 1 재장전 완료: " + slot.name + ", 장탄수: " + magazineCapacitySlot1);
        }
        else if (slot == weaponSlot2)
        {
            magazineCapacitySlot2 = slot_componet.weapon.backup_magazine_capacity;
            Debug.Log("무기 슬롯 2 재장전 완료: " + slot.name + ", 장탄수: " + magazineCapacitySlot2);
        }
        

        // 코루틴 실행 상태 업데이트
        if (slotNumber == 1) isReloadingSlot1 = false;
        else if (slotNumber == 2) isReloadingSlot2 = false;
        reload_object.SetActive(false);

        Debug.Log("재장전 오브젝트 꺼짐");
    }
}
