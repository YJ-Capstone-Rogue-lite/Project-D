using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float defaultspeed = 5f;
    public float moveSpeed = 5f;
    public float dashSpeed;
    public float dashDuration = 0.5f; // 대쉬 지속 시간 (초 단위)
    public float dashCoolTime = 1.5f; // 대쉬 쿨타임 (초 단위)

    private Rigidbody2D rigid2d;
    private Vector3 movement;
    private bool isDashing = false;
    private float dashCoolTimer = 0f; // 대쉬 쿨타임을 추적하기 위한 타이머

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

        // 대쉬 쿨타임 타이머 감소
        if (dashCoolTimer > 0)
        {
            dashCoolTimer -= Time.deltaTime;
        }
    }

    IEnumerator DoDash()
    {
        moveSpeed *= 3; // 대쉬 속도 증가
        yield return new WaitForSeconds(dashDuration);
        moveSpeed /= 3; // 원래 이동 속도로 되돌림
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
