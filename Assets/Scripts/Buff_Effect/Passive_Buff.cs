using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class passive_buff : MonoBehaviour
{
    //영구적으로 상승되는 스탯을 위한 버프 스크립트
    public static Character charsingle; //캐릭터 값 가져오고

    // 캐릭터 상태 데이터 가져오고
    [SerializeField] protected CharStateData charStateData;





    //스탯증가 아이템과 상호작용 할시 캐릭터의 스탯이 변경
    //하지만 최대치 또는 최소치를 넘어갈시 스탯 증감코드가 작동 안하도록 제작

    //만약 상호작용한다면?

    //스크립터블 오브젝트의 값을 받아와서 해당 스탯을 증감
    
    //이 코드를 상호작용 코드에 넣어서 플레이어에게 적용시킬거임
    //데미지 업 패시브 버프
    public void Damage_Up_Passive_Buff()
    {

    }

}
