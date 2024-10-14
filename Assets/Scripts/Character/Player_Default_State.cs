using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Default_State
{
    //플레이어의 JSON 파일(저장 파일이 없을때 즉 새로 시작할때) 사용할 기본 플레이어 스탯값

    // 플레이어 최대 체력
    public int Default_player_maxhp = 100;

    // 플레이어 현재 체력
    public int Default_player_hp = 100;

    // 플레이어 최대 방어막
    public int Default_player_maxshield = 100;

    // 플레이어 현재 방어막
    public int Default_player_shield = 100;

    // 플레이어 이동 속도
    public int Default_player_movespeed = 5;

    // 캐릭터의 이동속도 최대치
    public float Default_player_max_movementSpeed = 7;

    // 캐릭터의 이동속도 최소치
    public float Default_player_min_movementSpeed = 3;

    //플레이어 스태미나
    public int Default_player_stamina = 100;

    //플레이어 최대 스태미나
    public int Default_player_maxstamina = 100;

    // 플레이어 보호 시간
    public float Default_player_protectionTime = 1f;

    // 스택을 저장할 변수
    public float Default_player_damageUpStack = 0; //데미지업 스택

    public float Default_player_movement_SpeedUpStack = 0; // 스피드업 스택

    public float Default_player_max_hp_UPStack = 0; // 최대 체력 스택

    //플레이어가 획득한 코인
    public int Default_player_Coin_Count = 0;
}
