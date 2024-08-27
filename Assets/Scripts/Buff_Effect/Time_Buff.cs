using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Buff : MonoBehaviour
{
    public static Character charsingle; //캐릭터 값 가져오고
    public Item_PickUp item_PickUp; //아이템 픽업 값 가져오기;

    // 캐릭터 상태 데이터
    [SerializeField]
    protected CharStateData charStateData;

    // 예를 들어, 공격력 증가 버프(임시용) 나중에 상호작용한 아이템의 값을 가져와야함
    public float damage_Up_Buff_Amount;
    public float duration_time;

    // 버프 적용 메서드
    public void ApplyBuff()
    {
        // 버프를 적용하고 60초 후에 제거
        StartCoroutine(BuffRoutine());
    }

    private IEnumerator BuffRoutine()
    {
        damage_Up_Buff_Amount = item_PickUp.buff.damage_up;
        duration_time = item_PickUp.buff.duration;
        // 버프를 적용
        Character.charsingle.m_buff_damage += damage_Up_Buff_Amount; //캐릭터의 버프 데미지 값에 공증 값 더함
        Debug.Log("버프 적용됨: 공격력 + " + damage_Up_Buff_Amount);

        // 60초 동안 버프 유지 (해당 초도 상호작용한 아이템의 값을 가져와야함
        yield return new WaitForSeconds(duration_time);

        // 버프를 제거
        Character.charsingle.m_buff_damage -= damage_Up_Buff_Amount;
        Debug.Log("버프 제거됨: 공격력 - " + damage_Up_Buff_Amount);
    }
}
