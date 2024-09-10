using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;



public class Enemy : MonoBehaviour
{
    public BehaviourTree.BehaviourTree behaviorTree;
    protected BehaviourTree.Blackboard blackboard;
    // 플레이어를 추적하기 위한 타겟
    public Transform targetTransform;
    public Coroutine moveCoroutine;

    [Header("적의 스탯")]
    // 적의 이동 속도와 회전 속도
    public float enemy_speed = 3f;
    public float rotateSpeed = 0.25f;
    // 적의 체력
    public float enemy_hp;

    //적의 최대 체력
    public float max_enemy_hp;


    // 적의 몸박 데미지
    public float enemy_body_damage;

    // 적이 이동할 목표 위치
    public Vector3 endPosition;

    // 플레이어를 추적하는지 여부
    public bool followPlayer;

    // 플레이어를 찾는 주기
    public float find_Playersecond;

    // 적의 충돌을 감지하는 Circle Collider
    public CircleCollider2D circleCollider2D;
    public float attackRange;
    public float damage;
    public Animator enemy_animator;
    public Rigidbody2D enemy_rb;
    public SpriteRenderer spriteRenderer;
    public float currentAngle = 0;
    public bool Attack_the_Player = false;
    public GameObject hit;
    public int originalSortingOrder;

    //몬스터 hpbar
    public GameObject enemy_hp_bar;

    public Item_drop item_Drop;

    public GameObject enemyTag;
    protected Transform originPos;
    private void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
        enemy_animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // StartCoroutine(WanderRoutine()); // 적의 무작위 이동 시작
        originalSortingOrder = spriteRenderer.sortingOrder;
        behaviorTree.blackboard.thisUnit = GetComponent<Enemy>();
        if(originPos == null)
        {
            var posObj = new GameObject(){
                name = transform.name+" Pos",
                layer = LayerMask.NameToLayer("Ignore Raycast"),
            };
            originPos = posObj.transform;
        }
        originPos.transform.position = transform.position;
        behaviorTree = behaviorTree.Clone();
        blackboard = behaviorTree.blackboard;
        ChooseNewEndPoint();
    }

    private void Update()
    {
        var hit =  Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, 1<<LayerMask.NameToLayer("Player"));
        if (hit && hit.transform.gameObject.CompareTag("Player"))
        {
            blackboard.target = hit.transform;
            blackboard.state = BehaviourTree.Blackboard.State.Aggro;
        }
        behaviorTree.Update();
        // return;


        // Debug.DrawLine(enemy_rb.position, endPosition, Color.red); // Enemy가 움직일 목표지점 표시

        // Enemy_die(); // 적의 사망 체크
        // Debug.DrawRay(enemy_rb.position, Vector2.down, new Color(1, 0, 0));
        // RaycastHit2D[] hit_ray = Physics2D.RaycastAll(enemy_rb.position, Vector2.down, 1, LayerMask.GetMask("Wall")); // 오더레이어 업데이트
        // for (int i = 0; i < hit_ray.Length; i++)
        // {
        //     if (hit_ray[i].collider != null && !hit_ray[i].collider.isTrigger)
        //     {
        //         // 레이캐스트가 "Wall" 레이어에 닿은 경우 "닿음" 메시지를 출력하고 sortingOrder를 0으로 설정합니다.
        //         spriteRenderer.sortingOrder = -1;
        //         spriteRenderer.sortingLayerName = "Wall";
        //     }
        //     // 레이캐스트가 아무 콜라이더에도 닿지 않은 경우
        //     else
        //     {
        //         // 원래 sortingOrder 값을 복원합니다.
        //         spriteRenderer.sortingOrder = originalSortingOrder;
        //         spriteRenderer.sortingLayerName = "Enemy";
        //     }
        // }
    }

    
    // protected IEnumerator WanderRoutine() // Enemy가 무작위로 배회를 하는 코루틴
    // {
    //     while (true)
    //     {
    //         ChooseNewEndPoint(); // 새로운 목표 위치 선택

    //         if (moveCoroutine != null)
    //         {
    //             // 이전 코루틴이 완료될 때까지 대기
    //             yield return moveCoroutine;
    //         }
    //         // 플레이어를 찾는 코루틴 시작
    //         moveCoroutine = StartCoroutine(FindPlayer(enemy_rb, enemy_speed));

    //         yield return new WaitForSeconds(find_Playersecond); // 주기적으로 플레이어를 찾는 주기에 따라 대기
    //     }
    // }

    public void ChooseNewEndPoint() //Enemy가 도착할 목표위치를 정하는 메소드
    {
        float maxDistance = 3f; // 원하는 최대 이동 거리
        currentAngle = Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        originPos.position = transform.position + Vector3FromAngle(currentAngle) * maxDistance;
        blackboard.target = originPos;
    }
    private Vector3 Vector3FromAngle(float inputAngle)
    {
        float inputAngleRadians = inputAngle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    // public IEnumerator FindPlayer(Rigidbody2D rigbodyToMove, float speed)  //플레이어를 찾아서 추적하는 코루틴
    // {
    //     float remainingDistance = (transform.position - endPosition).sqrMagnitude;
    //     while (remainingDistance > float.Epsilon)
    //     {
    //         if (targetTransform != null) // 플레이어 추적
    //         {
    //             endPosition = targetTransform.position;
    //         }

    //         if (rigbodyToMove != null) //Enemy 움직임
    //         {
    //             enemy_animator.SetBool("FindPlayer", true);
    //             Vector3 currentPosition = new Vector3(rigbodyToMove.position.x, rigbodyToMove.position.y, 0f);
    //             Vector3 direction = (endPosition - currentPosition).normalized;

    //             // 이동 방향에 따라 MoveX와 MoveY 설정
    //             float moveX = direction.x;
    //             float moveY = direction.y;

    //             // 애니메이션 파라미터 설정
    //             enemy_animator.SetFloat("MoveX", moveX);
    //             enemy_animator.SetFloat("MoveY", moveY);

    //             // 이동 로직
    //             Vector3 newPosition = Vector3.MoveTowards(currentPosition, endPosition, speed * Time.deltaTime);
    //             enemy_rb.MovePosition(newPosition);
    //             if (enemyTag.CompareTag("Boss"))
    //             {
    //                 if (moveX < 0)
    //                 {
    //                     spriteRenderer.flipX = true;
    //                 }
    //                 else if (moveX > 0)
    //                 {
    //                     spriteRenderer.flipX = false;
    //                 }
    //             }
    //             else
    //             {
    //                 if (moveX > 0)
    //                 {
    //                     spriteRenderer.flipX = true;
    //                 }
    //                 else if (moveX < 0)
    //                 {
    //                     spriteRenderer.flipX = false;
    //                 }
    //             }

    //             remainingDistance = (transform.position - endPosition).sqrMagnitude;
    //         }
    //         yield return new WaitForFixedUpdate();
    //     }
    //     enemy_animator.SetBool("FindPlayer", false); // 플레이어를 찾지 못한 경우 상태를 원래대로 설정
    // }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))  // Enemy가 데미지를 받는 코드 
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null)
                return;
            enemy_hp -= bullet.Damage; // 적의 체력 감소
            enemy_hpbar_update();//적의 체력바 업데이트
            Enemy_die();

            // 적이 밀려나지 않도록 속도를 0으로 설정
            enemy_rb.velocity = Vector2.zero;
            enemy_rb.angularVelocity = 0f;

            // 적의 위치를 고정하여 밀리지 않도록 설정
            enemy_rb.isKinematic = true;
            StartCoroutine(ResetKinematic()); // 일정 시간 후에 다시 isKinematic을 false로 설정
        }
        // else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object")) // 벽에 접촉했을 때 새로운 이동경로 찾는 코드
        // {
        //     Player temp = null;
        //     if(blackboard.target != null) blackboard.target.TryGetComponent<Player>(out temp);
        //     if(temp != null) attackRange *= 10;
        //     ChooseNewEndPoint();
        // }
    }
    
    // public void OnTriggerEnter2D(Collider2D collider) 
    // {
    //     return;
    //     if (collider.gameObject.CompareTag("Player") && followPlayer) // 플레이어에 접촉했을 때
    //     {
    //         Debug.Log("플레이어를 찾음");
    //         targetTransform = collider.gameObject.transform;
    //         if (moveCoroutine != null)
    //         {
    //             StopCoroutine(moveCoroutine);
    //         }
    //         // 플레이어를 추적하는 코루틴 시작
    //         moveCoroutine = StartCoroutine(FindPlayer(enemy_rb, enemy_speed));
    //     }
    // }

    public IEnumerator ResetKinematic()
    {
        yield return new WaitForFixedUpdate(); // 물리 업데이트 후에 다시 false로 설정
        enemy_rb.isKinematic = false;
    }

    // public void OnTriggerExit2D(Collider2D collider)
    // {
    //     return;
    //     if (collider.gameObject.CompareTag("Player")) // 플레이어와 접촉을 끊었을 때
    //     {
    //         enemy_animator.SetBool("FindPlayer", false); // 플레이어를 찾지 못한 상태로 변경
    //         if (moveCoroutine != null)
    //         {
    //             StopCoroutine(moveCoroutine);
    //         }
    //         targetTransform = null; // 타겟을 초기화
    //         StartCoroutine(WanderRoutine());
    //     }
    // }

    // Circle Collider를 그리는 메서드
    void OnDrawGizmos()
    {
        if (circleCollider2D != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider2D.radius);
        }
    }

    // 적의 사망을 체크하고 필요한 처리를 하는 메서드
    public void Enemy_die()
    {
        if (enemy_hp <= 0) // 적의 체력이 0 이하일 때
        {
            blackboard.state = BehaviourTree.Blackboard.State.Death;
            enemy_animator.SetBool("State", false);
            enemy_speed = 0;
            // if (moveCoroutine != null)
            //     StopCoroutine(moveCoroutine);
            enemy_rb.velocity = Vector2.zero; // 움직임 멈춤
            this.enabled = false; // 스크립트 비활성화하여 다른 업데이트 중지
            transform.parent.parent.GetComponent<Room>().EnemyTemp(-1); // 적이 속한 방에서 적 개수를 줄임
            item_Drop.enemy_item_drop();

            GameManager.Instance.enemyDestoryCount += 1;
        }
    }

    public virtual void Attack_of_Enemy() //EnemyAttack에서 SendMessage로 불러와서 코드가 활성화됨
    {
        if (!Attack_the_Player)
        {
            enemy_speed = 0;
            Attack_the_Player = true;
            enemy_animator.SetTrigger("Attack");
            StartCoroutine(End_Attack(1));
        }
    }

    public void Damage_of_Enemy()
    {
        hit.SetActive(true);
    }

    protected IEnumerator End_Attack(float time) // 공격 애니메이션에 추가
    {
        yield return new WaitForSeconds(time);
        Attack_the_Player = false;
        hit.SetActive(false);
        enemy_speed = 3;
    }

    public void Destroy_Enemy()
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
