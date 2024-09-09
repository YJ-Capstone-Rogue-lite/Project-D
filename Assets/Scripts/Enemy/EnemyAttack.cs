using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject enemyTag;
    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Player")) // 플레이어와 충돌했을 때
        // {
        //     Debug.Log("인식");
        //     if (enemyTag.CompareTag("Boss"))
        //     {
        //         transform.parent.SendMessage("Attack_of_Boss", SendMessageOptions.RequireReceiver);
        //         Debug.Log("보스공격");
        //     }
        //     else if(enemyTag.CompareTag("Enemy"))
        //     {
        //         // A 오브젝트 (Enemy)에게 피격 처리를 요청
        //         transform.parent.SendMessage("Attack_of_Enemy", SendMessageOptions.RequireReceiver);
        //         Debug.Log("에너미공격");
        //     }
        // }
    }
}
