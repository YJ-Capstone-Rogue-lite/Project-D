using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //플레이어 ID
    public string player_ID;

    //플레이어 PW
    public string player_PW;

    // 플레이어 닉네임
    public string player_name;

    // 플레이어 최대 체력
    public int player_maxhp = 100;

    // 플레이어 현재 체력
    public int player_hp = 100;

    // 플레이어 최대 방어막
    public int player_maxshield = 100;

    // 플레이어 현재 방어막
    public int player_shield = 0;

    // 플레이어 이동 속도
    public int player_movespeed = 5;

    // 플레이어 보호 시간
    public float player_protectionTime = 0.2f;
}
