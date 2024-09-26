using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Passive_buff : MonoBehaviour
{
    //영구적으로 상승되는 스탯을 위한 버프 스크립트
    public Character charsingle; //캐릭터 값 가져오고
    public Item_PickUp item_PickUp; //아이템 픽업 값 가져오기;


    //스탯증가 아이템과 상호작용 할시 캐릭터의 스탯이 변경
    //하지만 최대치 또는 최소치를 넘어갈시 스탯 증감코드가 작동 안하도록 제작

    //만약 상호작용한다면?

    //스크립터블 오브젝트의 값을 받아와서 해당 스탯을 증감

    //이 코드를 상호작용 코드에 넣어서 플레이어에게 적용시킬거임



    //데미지 업 패시브 버프
    public void Damage_Up_Passive_Buff()
    {
        //버프는 최대 5스택까지, 1스택 올라갈때마다 아이콘 오른쪽 하단 숫자 표기 변경
        if (charsingle.damageUpStack < 5) // 최대 5스택까지 증가 가능
        {
            charsingle.damageUpStack++;
            //캐릭터의 버프 데미지 값에 아이템 픽업의 버프 데미지 값 받아와서 더해주기
            charsingle.m_damage += item_PickUp.buff.damage_up;

            UpdateBuffIcon(); // 아이콘에 스택 수 업데이트
        }
    }

    //스피드 업 패시브 버프
    public void Movement_Speed_Up_Passive_Buff()
    {
        //버프는 최대 5스택까지, 1스택 올라갈때마다 아이콘 오른쪽 하단 숫자 표기 변경
        //이동속도 증가 스택이 5보다 작거나 캐릭터의 현재 이동속도가 최대칩다 작거나 같다면
        if (charsingle.movement_SpeedUpStack < 5 || charsingle.m_movementSpeed <= charsingle.m_max_movementSpeed)
        {
            charsingle.movement_SpeedUpStack++;
            //캐릭터의 이동속도 값 더해주기
            charsingle.m_movementSpeed += item_PickUp.buff.movement_Speed_up;

            UpdateBuffIcon(); // 아이콘에 스택 수 업데이트
        }
    }

    private void UpdateBuffIcon()
    {
        // 버프 아이콘 오른쪽 하단에 숫자 표기 변경 로직 추가
    }
}
