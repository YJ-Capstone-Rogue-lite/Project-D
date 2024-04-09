using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    // OnCollisionEnter2D는 충돌이 발생했을 때 호출됩니다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 Rigidbody2D 컴포넌트를 가지고 있는지 확인합니다.
        Rigidbody2D rigidbody2D = collision.collider.GetComponent<Rigidbody2D>();

        if (rigidbody2D)
        {
            // Rigidbody2D에 힘을 추가합니다. 원하는 로직에 맞게 수정하세요.
            rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }
}
