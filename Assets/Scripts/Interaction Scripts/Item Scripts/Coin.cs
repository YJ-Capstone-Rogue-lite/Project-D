using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "Scriptable Object/CoinData")]

public class Coin : ScriptableObject
{
    public GameObject CoinPrefab;//해당 코인 값을 가지고있는 프리펩 게임 오브젝트


    [Header(" # 코인 기본 정보")]
    public int Coin_number; //코인 번호
    public string Coin_name; //코인 이름
    public Sprite Coin_Icon_sprite; //코인 아이콘

    [Header(" # 아이템 설명")]
    [TextArea(5, 10)] // 첫 번째 매개변수는 텍스트 입력창의 높이를 결정하고, 두 번째 매개변수는 텍스트 입력창의 너비를 결정합니다.
    public string info; //버프 정보(설명) 
}
