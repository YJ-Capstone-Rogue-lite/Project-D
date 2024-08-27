using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Scriptable Object/BuffData")]

public class Buff : ScriptableObject
{
    public GameObject BuffPrefab;//해당 버프 값을 가지고있는 프리팹
    public enum Buff_Time { duration, passive } //버프유형(시간형, 영구 지속형)

    public enum BuffType {Attack, MoveSpeed, Health_Up} //버프 타입(공격력, 이동속도, 체력증가 등)

    [Header(" # 버프 기본 정보")]
    public int Buff_number; //버프 번호
    public string Buff_name; //버프 이름
    public Sprite Buff_Icon_sprite; //버프 아이콘

    [Header(" # 버프 능력치 정보")]
    public float damage_up; //데미지 업 수치
    public float movement_up; //이동속도 증가
    public float health_up; //체력 최대치 증가
    public float duration; //만약 시간형 버프일 경우 적용 될 시간

    [Header(" # 아이템 설명")]
    [TextArea(3, 10)] // 첫 번째 매개변수는 텍스트 입력창의 높이를 결정하고, 두 번째 매개변수는 텍스트 입력창의 너비를 결정합니다.
    public string info; //버프 정보(설명) 
}
