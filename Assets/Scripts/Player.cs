using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float defaultspeed = 5f;
    public float moveSpeed = 5f;
    public float dashSpeed;
    public float dashDuration = 0.5f; // �뽬 ���� �ð� (�� ����)
    public float dashCoolTime = 1.5f; // �뽬 ��Ÿ�� (�� ����)

    private Rigidbody2D rigid2d;
    private Vector3 movement;
    private bool isDashing = false;
    private float dashCoolTimer = 0f; // �뽬 ��Ÿ���� �����ϱ� ���� Ÿ�̸�

    private void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Dash();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveVelocity = new Vector3(x, 0, z) * moveSpeed * Time.deltaTime;
        transform.position += moveVelocity;
    }

    void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isDashing && dashCoolTimer <= 0)
        {
            StartCoroutine(DoDash());
            isDashing = true;
        }

        // �뽬 ��Ÿ�� Ÿ�̸� ����
        if (dashCoolTimer > 0)
        {
            dashCoolTimer -= Time.deltaTime;
        }
    }

    IEnumerator DoDash()
    {
        moveSpeed *= 3; // �뽬 �ӵ� ����
        yield return new WaitForSeconds(dashDuration);
        moveSpeed /= 3; // ���� �̵� �ӵ��� �ǵ���
        Debug.Log("Dash Ended");
        StartCoroutine(StartDashCoolTime());
    }

    IEnumerator StartDashCoolTime()
    {
        Debug.Log("Dash Cool Time Start");
        dashCoolTimer = dashCoolTime;
        yield return new WaitForSeconds(dashCoolTime);
        isDashing = false;
    }
}
