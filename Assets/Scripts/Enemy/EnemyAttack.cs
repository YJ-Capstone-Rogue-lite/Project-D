using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            // A 오브젝트 (Enemy)에게 피격 처리를 요청
            transform.parent.SendMessage("Attack_of_Enemy", SendMessageOptions.RequireReceiver);
        }
    }
}
