using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Slot : MonoBehaviour
{
    public GameObject weaponSlot1; // 무기 슬롯 1 게임 오브젝트
    public GameObject weaponSlot2; // 무기 슬롯 2 게임 오브젝트

    private FIre_Test fire_test;

    public GameObject activeWeaponSlot; // 활성화된 무기 슬롯 게임 오브젝트


    public float magazineCapacitySlot1; // 슬롯 1의 장탄수
    public float magazineCapacitySlot2; // 슬롯 2의 장탄수

    public bool isReloadingSlot1 = false;
    public bool isReloadingSlot2 = false;


    private void Start()
    {
        // 게임 시작시 기본 무기 슬롯 설정
        EnableWeaponSlot(weaponSlot1);
    }

   

    private void Update()
    {
        slotchange();

        if (Input.GetButtonDown("Reloading"))
        {
            Reload(activeWeaponSlot);
            Debug.Log("R 키 누름" + activeWeaponSlot.name);
            
        }

    }

    // 무기를 슬롯에 할당하는 메서드
    public void ReceiveWeapon(Weapon weapon)
    {
        // 활성화된 무기 슬롯이 있는지 확인
        //만약 웨폰슬롯2의 타입이 None이 아니라면
        if (activeWeaponSlot != null && weaponSlot2.GetComponent<FIre_Test>().weapon.weaponType == Weapon.WeaponType.None)
        {

            // 우선적으로 웨폰슬롯2에 무기를 할당함
            weaponSlot2.GetComponent<FIre_Test>().weapon = weapon;

            // 활성화된 슬롯의 무기를 가져와서 장탄수를 설정합니다.
            UpdateMagazineCapacity();


        }
        else if(weaponSlot2.GetComponent<FIre_Test>().weapon.weaponType != Weapon.WeaponType.None)
        {
            // 활성화된 무기 슬롯에만 무기를 할당합니다.
            activeWeaponSlot.GetComponent<FIre_Test>().weapon = weapon;

            // 활성화된 슬롯의 무기를 가져와서 장탄수를 설정합니다.
            UpdateMagazineCapacity();
        }
        else
        {
            // 에러 메시지 출력
            Debug.LogError("활성화된 무기 슬롯이 없습니다.");
        }
        
    }

    //슬롯 변경 기능
    void slotchange()
    {
        // 마우스 휠을 사용하여 무기 슬롯 변경
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel > 0f)
        {
            // 마우스 휠을 위로 스크롤할 때
            SwitchToNextSlot();
        }
        else if (mouseWheel < 0f)
        {
            // 마우스 휠을 아래로 스크롤할 때
            SwitchToPreviousSlot();
        }

        // 숫자 1, 2 키를 사용하여 무기 슬롯 변경
        if (Input.GetKeyDown(("1")))
        {
            EnableWeaponSlot(weaponSlot1);
        }
        else if (Input.GetKeyDown(("2")))
        {
            EnableWeaponSlot(weaponSlot2);
        }
    }

    // 다음 무기 슬롯으로 변경
    private void SwitchToNextSlot()
    {
        if (activeWeaponSlot == weaponSlot1)
        {
            EnableWeaponSlot(weaponSlot2);
        }
        else if (activeWeaponSlot == weaponSlot2)
        {
            EnableWeaponSlot(weaponSlot1);
        }
    }

    // 이전 무기 슬롯으로 변경
    private void SwitchToPreviousSlot()
    {
        if (activeWeaponSlot == weaponSlot1)
        {
            EnableWeaponSlot(weaponSlot2);
        }
        else if (activeWeaponSlot == weaponSlot2)
        {
            EnableWeaponSlot(weaponSlot1);
        }
    }

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

    //장탄수 업데이트
    public void UpdateMagazineCapacity()
    {
        // 현재 활성화된 슬롯의 장탄수를 설정
        if (activeWeaponSlot == weaponSlot1)
        {
            magazineCapacitySlot1 = activeWeaponSlot.GetComponent<FIre_Test>().weapon.backup_magazine_capacity;
        }
        else if (activeWeaponSlot == weaponSlot2)
        {
            magazineCapacitySlot2 = activeWeaponSlot.GetComponent<FIre_Test>().weapon.backup_magazine_capacity;
        }
    }

    // 무기 슬롯의 장탄수 감소 메서드
    public void DecreaseMagazineCapacity(GameObject slot)
    {
        if (slot == weaponSlot1)
        {
            magazineCapacitySlot1 -= 1;
            Debug.Log("무기 슬롯 1의 장탄수 감소: " + magazineCapacitySlot1);

            if (magazineCapacitySlot1 <= 0)
            {
                Reload(slot);
            }
        }
        else if (slot == weaponSlot2)
        {
            magazineCapacitySlot2 -= 1;
            Debug.Log("무기 슬롯 2의 장탄수 감소: " + magazineCapacitySlot2);

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

    }

    private IEnumerator ReloadCoroutine(GameObject slot, int slotNumber)
    {
        

        // 코루틴 실행 상태 업데이트
        if (slotNumber == 1) isReloadingSlot1 = true;
        else if (slotNumber == 2) isReloadingSlot2 = true;

        Debug.Log("무기 슬롯 재장전 시작: " + slot.name);
        yield return new WaitForSeconds(slot.GetComponent<FIre_Test>().weapon.reload_time);

        if (slot == weaponSlot1)
        {
            magazineCapacitySlot1 = slot.GetComponent<FIre_Test>().weapon.backup_magazine_capacity;
            Debug.Log("무기 슬롯 1 재장전 완료: " + slot.name + ", 장탄수: " + magazineCapacitySlot1);
        }
        else if (slot == weaponSlot2)
        {
            magazineCapacitySlot2 = slot.GetComponent<FIre_Test>().weapon.backup_magazine_capacity;
            Debug.Log("무기 슬롯 2 재장전 완료: " + slot.name + ", 장탄수: " + magazineCapacitySlot2);
        }

        // 코루틴 실행 상태 업데이트
        if (slotNumber == 1) isReloadingSlot1 = false;
        else if (slotNumber == 2) isReloadingSlot2 = false;



    }
}
