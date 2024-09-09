using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Enemy : MonoBehaviour
{
    // 플레이어를 추적하기 위한 타겟
    public Transform targetTransform;
    public Coroutine moveCoroutine;

    [Header("적의 스탯")]
    // 적의 이동 속도와 회전 속도
    public float enemy_speed = 3f;
    public float rotateSpeed = 0.25f;
    // 적의 체력
    public float enemy_hp = 100;

    //적의 최대 체력
    public float max_enemy_hp = 100;


    // 적의 몸박 데미지
    public float enemy_body_damage;

    // 적이 이동할 목표 위치
    public Vector3 endPosition;

    // 플레이어를 추적하는지 여부
    public bool followPlayer;

    // 플레이어를 찾는 주기
    [SerializeField] private float find_Playersecond;

    // 적의 충돌을 감지하는 Circle Collider
    private CircleCollider2D circleCollider2D;
    private Animator enemy_animator;
    private Rigidbody2D enemy_rb;
    private SpriteRenderer spriteRenderer;
    private float currentAngle = 0;
    private bool Attack_the_Player = false;
    [SerializeField] GameObject hit;
    private int originalSortingOrder;

    //몬스터 hpbar
    public GameObject enemy_hp_bar;

    public Item_drop item_Drop;

    private void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
        enemy_animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WanderRoutine()); // 적의 무작위 이동 시작
        originalSortingOrder = spriteRenderer.sortingOrder;
    }

    private void Update()
    {
        Debug.DrawLine(enemy_rb.position, endPosition, Color.red); // 적의 현재 위치와 목표 위치 사이에 빨간색 선을 그림

        Enemy_die(); // 적의 사망 체크
        Debug.DrawRay(enemy_rb.position, Vector2.down, new Color(1, 0, 0));
        RaycastHit2D[] hit_ray = Physics2D.RaycastAll(enemy_rb.position, Vector2.down, 1, LayerMask.GetMask("Wall"));
        for (int i = 0; i < hit_ray.Length; i++)
        {
            if (hit_ray[i].collider != null && !hit_ray[i].collider.isTrigger)
            {
                // 레이캐스트가 "Wall" 레이어에 닿은 경우 "닿음" 메시지를 출력하고 sortingOrder를 0으로 설정합니다.
                spriteRenderer.sortingOrder = -1;
                spriteRenderer.sortingLayerName = "Wall";
            }
            // 레이캐스트가 아무 콜라이더에도 닿지 않은 경우
            else
            {
                // 원래 sortingOrder 값을 복원합니다.
                spriteRenderer.sortingOrder = originalSortingOrder;
                spriteRenderer.sortingLayerName = "Enemy";
            }
        }
    }

    // 무작위로 이동하는 코루틴
    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint(); // 새로운 목표 위치 선택

            if (moveCoroutine != null)
            {
                // 이전 코루틴이 완료될 때까지 대기
                yield return moveCoroutine;
            }
            // 플레이어를 찾는 코루틴 시작
            moveCoroutine = StartCoroutine(FindPlayer(enemy_rb, enemy_speed));

            yield return new WaitForSeconds(find_Playersecond); // 주기적으로 플레이어를 찾는 주기에 따라 대기
        }
    }

    // 무작위로 목표 위치를 선택하는 메서드
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

                // 이동 로직
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
        enemy_animator.SetBool("FindPlayer", false); // 플레이어를 찾지 못한 경우 상태를 원래대로 설정
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // 총알에 접촉했을 때
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null)
                return;
            enemy_hp -= bullet.Damage; // 적의 체력 감소
            enemy_hpbar_update();//적의 체력바 업데이트

            // 적이 밀려나지 않도록 속도를 0으로 설정
            enemy_rb.velocity = Vector2.zero;
            enemy_rb.angularVelocity = 0f;

            // 적의 위치를 고정하여 밀리지 않도록 설정
            enemy_rb.isKinematic = true;
            StartCoroutine(ResetKinematic()); // 일정 시간 후에 다시 isKinematic을 false로 설정
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object")) // 벽에 접촉했을 때
        {
            // 벽에 충돌했을 때 새로운 이동 경로를 찾습니다.
            ChooseNewEndPoint();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && followPlayer) // 플레이어에 접촉했을 때
        {
            Debug.Log("플레이어를 찾음");
            targetTransform = collider.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // 플레이어를 추적하는 코루틴 시작
            moveCoroutine = StartCoroutine(FindPlayer(enemy_rb, enemy_speed));
            
        }
    }

    private IEnumerator ResetKinematic()
    {
        yield return new WaitForFixedUpdate(); // 물리 업데이트 후에 다시 false로 설정
        enemy_rb.isKinematic = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어와 접촉을 끊었을 때
        {
            enemy_animator.SetBool("FindPlayer", false); // 플레이어를 찾지 못한 상태로 변경
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null; // 타겟을 초기화
        }
    }

    // Circle Collider를 그리는 메서드
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

    // 적의 사망을 체크하고 필요한 처리를 하는 메서드
    private void Enemy_die()
    {
        if (enemy_hp <= 0) // 적의 체력이 0 이하일 때
        {
            enemy_animator.SetBool("State", false);
            enemy_speed = 0;
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            enemy_rb.velocity = Vector2.zero; // 움직임 멈춤
            this.enabled = false; // 스크립트 비활성화하여 다른 업데이트 중지
            transform.parent.parent.GetComponent<Room>().EnemyTemp(-1); // 적이 속한 방에서 적 개수를 줄임
            item_Drop.enemy_item_drop();

            GameManager.Instance.enemyDestoryCount += 1;
        }
    }

    void Attack_of_Enemy() //EnemyAttack에서 SendMessage로 불러와서 코드가 활성화됨
    {
        if (!Attack_the_Player)
        {
            enemy_speed = 0;
            Attack_the_Player = true;
            enemy_animator.SetBool("Attack", true);
        }
    }
    public void Damage_of_Enemy()
    {
        hit.SetActive(true);
    }
    void End_Attack() // 공격 애니메이션에 추가
    {
        Attack_the_Player = false;
        hit.SetActive(false);
        enemy_speed = 3;
        enemy_animator.SetBool("Attack", false);
        StartCoroutine(FindPlayer(enemy_rb, enemy_speed));
    }

    private void Destroy_Enemy()
    {
        Destroy(gameObject); // 적 오브젝트 제거
    }

    public void enemy_hpbar_update() // 체력 바 업데이트
    {
        Image enemy_hp_bar_img = enemy_hp_bar.GetComponent<Image>();
        enemy_hp_bar_img.fillAmount = enemy_hp / max_enemy_hp;
        if(enemy_hp <= 0)
        {
            enemy_hp_bar.SetActive(false);
        }
    }


}
