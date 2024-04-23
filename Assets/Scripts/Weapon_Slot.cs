using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Slot : MonoBehaviour
{
    public GameObject weaponSlot1; // 무기 슬롯 1 게임 오브젝트
    public GameObject weaponSlot2; // 무기 슬롯 2 게임 오브젝트

    private GameObject activeWeaponSlot; // 활성화된 무기 슬롯 게임 오브젝트

    private float magazineCapacity; // 현재 활성화된 슬롯의 장탄수


    // 무기를 슬롯에 할당하는 메서드
    public void ReceiveWeapon(Weapon weapon)
    {
        // 활성화된 무기 슬롯이 있는지 확인
        if (activeWeaponSlot != null)
        {
            // 활성화된 무기 슬롯에만 무기를 할당합니다.
            activeWeaponSlot.GetComponent<FIre_Test>().weapon = weapon;
        }
        else
        {
            // 에러 메시지 출력
            Debug.LogError("활성화된 무기 슬롯이 없습니다.");
        }
    }

    private void Start()
    {
        // 게임 시작시 기본 무기 슬롯 설정
        EnableWeaponSlot(weaponSlot1);
    }

    // 무기 슬롯을 활성화하는 메서드
    private void EnableWeaponSlot(GameObject weaponSlot)
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

        // 활성화된 슬롯의 무기를 가져와서 장탄수를 설정합니다.
        magazineCapacity = weaponSlot.GetComponent<FIre_Test>().weapon.magazine_capacity;
    }




    private void Update()
    {
        slotchange();
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableWeaponSlot(weaponSlot1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
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
}
