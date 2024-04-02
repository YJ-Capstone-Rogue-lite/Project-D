using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데미지 정보를 저장하는 구조체
[System.Serializable]
public struct DamageData
{
    public Transform ownerTransform; // 데미지를 가한 개체의 Transform 정보
    public LayerMask ignoreLayer; // 데미지를 입힐 때 무시할 레이어 정보
    public float damage; // 입힐 데미지 양
    public EffectData effectData; // 데미지에 연관된 효과 데이터
}
