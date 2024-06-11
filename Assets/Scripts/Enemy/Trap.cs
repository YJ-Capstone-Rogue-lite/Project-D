using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;

        if (collider.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            //충돌한 오브젝트가 데미지 오브젝트인 경우 피해를 입음
            Character.charsingle.Damaged(GetComponent<DamageData>());
            return; 
        }
    }
}
