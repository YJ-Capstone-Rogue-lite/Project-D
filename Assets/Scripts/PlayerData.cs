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
    public string player_name = "";

    // 플레이어 최대 체력
    public float player_maxhp = 100;
    

    // 플레이어 최대 방어막
    public float player_maxshield = 100;


    // 플레이어 이동 속도
    public float player_movespeed = 3;

    // 캐릭터의 이동속도 최대치
    public float player_max_movementSpeed = 10;

    // 캐릭터의 이동속도 최소치
    public float player_min_movementSpeed = 200;


    //플레이어 최대 스태미나
    public float player_maxstamina = 100;

    // 플레이어 보호 시간
    public float player_protectionTime = 1f;

    // 스택을 저장할 변수
    public int player_damageUpStack; //데미지업 스택

    public int player_movement_SpeedUpStack; // 스피드업 스택

    public int player_max_hp_UPStack; // 최대 체력 스택

    //플레이어가 획득한 코인
    public int player_Coin_Count;
}
