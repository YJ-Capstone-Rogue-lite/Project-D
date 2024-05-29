using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Transform targetTransform;
    public Coroutine moveCoroutine;
    public float enemy_speed = 3f;
    public float rotateSpeed = 0.25f;
    public float enemy_hp = 100;

    public Vector3 endPosition;

    public bool followPlayer;

    private float currentAngle = 0;
    private CircleCollider2D circleCollider2D;
    private Animator enemy_animator;
    private Rigidbody2D enemy_rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float find_Playersecond;

    private void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
        enemy_animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WanderRoutine());
    }

    private void Update()
    {
        Debug.DrawLine(enemy_rb.position, endPosition, Color.red);

        Enemy_die();
    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint();

            if (moveCoroutine != null)
            {
                // 이전 코루틴이 완료될 때까지 대기
                yield return moveCoroutine;
            }
            moveCoroutine = StartCoroutine(FindPlayer(enemy_rb, enemy_speed));

            yield return new WaitForSeconds(find_Playersecond);
        }
    }

    public void ChooseNewEndPoint()
    {
        float maxDistance = 3f; // 원하는 최대 이동 거리
        currentAngle = Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition = transform.position + Vector3FromAngle(currentAngle) * maxDistance;
    }

    Vector3 Vector3FromAngle(float inputAngle)
    {
        float inputAngleRadians = inputAngle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    public IEnumerator FindPlayer(Rigidbody2D rigbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;
        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigbodyToMove != null)
            {
                enemy_animator.SetBool("FindPlayer", true);
                Vector3 currentPosition = new Vector3(rigbodyToMove.position.x, rigbodyToMove.position.y, 0f);
                Vector3 direction = (endPosition - currentPosition).normalized;

                // 이동 방향에 따라 MoveX와 MoveY 설정
                float moveX = direction.x;
                float moveY = direction.y;

                // 애니메이션 파라미터 설정
                enemy_animator.SetFloat("MoveX", moveX);
                enemy_animator.SetFloat("MoveY", moveY);

                // 이동 로직...
                Vector3 newPosition = Vector3.MoveTowards(currentPosition, endPosition, speed * Time.deltaTime);
                enemy_rb.MovePosition(newPosition);
                if (moveX > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (moveX < 0)
                {
                    spriteRenderer.flipX = false;
                }

                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        enemy_animator.SetBool("FindPlayer", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) //Bullet에 접촉했을 때
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null)
                return;

            enemy_hp -= bullet.Damage;
        }
        else if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(FindPlayer(enemy_rb, enemy_speed));
        }
        else if (collision.gameObject.CompareTag("Wall")) // 벽에 접촉했을 때
        {
            // 벽에 충돌했을 때 새로운 이동 경로를 찾습니다.
            ChooseNewEndPoint();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy_animator.SetBool("FindPlayer", false);
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }
    }

    void OnDrawGizmos()
    {
        if (circleCollider2D != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider2D.radius);
        }
    }

    private void FixedUpdate()
    {
        //enemy_rb.velocity = transform.up * Enemy_speed;
    }

    private void Enemy_die()
    {
        if (enemy_hp <= 0)
        {
            transform.parent.parent.GetComponent<Room>().EnemyTemp(-1);
            Destroy(gameObject);
        }
    }
}
