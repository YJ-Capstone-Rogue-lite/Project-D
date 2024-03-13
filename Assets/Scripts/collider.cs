using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    // OnCollisionEnter2D�� �浹�� �߻����� �� ȣ��˴ϴ�.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� Rigidbody2D ������Ʈ�� ������ �ִ��� Ȯ���մϴ�.
        Rigidbody2D rigidbody2D = collision.collider.GetComponent<Rigidbody2D>();

        if (rigidbody2D)
        {
            // Rigidbody2D�� ���� �߰��մϴ�. ���ϴ� ������ �°� �����ϼ���.
            rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }
}
