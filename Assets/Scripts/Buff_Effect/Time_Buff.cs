using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Buff : MonoBehaviour
{
    public static Character charsingle; //캐릭터 값 가져오고
    public 

    // 캐릭터 상태 데이터
    [SerializeField]
    protected CharStateData charStateData;

    // 예를 들어, 공격력 증가 버프(임시용) 나중에 상호작용한 아이템의 값을 가져와야함
    public float attackBuffAmount = 10f;

    // 버프 적용 메서드
    public void ApplyBuff()
    {
        // 버프를 적용하고 60초 후에 제거
        StartCoroutine(BuffRoutine());
    }

    private IEnumerator BuffRoutine()
    {
        // 버프를 적용
        Character.charsingle.m_buff_damage += Buff.; //캐릭터의 버프 데미지 값에 공증 값 더함
        Debug.Log("버프 적용됨: 공격력 + " + attackBuffAmount);

        // 60초 동안 버프 유지 (해당 초도 상호작용한 아이템의 값을 가져와야함
        yield return new WaitForSeconds(60f);

        // 버프를 제거
        Character.charsingle.m_buff_damage -= attackBuffAmount; 
        Debug.Log("버프 제거됨: 공격력 - " + attackBuffAmount);
    }
}
