using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Pig : MonoBehaviour
{
    public Transform targetTransform;
    public Coroutine moveCoroutine;
    public int boss_Skil_count;

    public float castingTime = 2.0f;
    private float castingProgress = 0.0f;
    private bool isCasting = false;

    [Header("적의 스탯")]
    public float enemy_speed = 3f;
    public float rotateSpeed = 0.25f;
    public float boss_HP = 1500;
    public float max_enemy_hp = 1500;
    public Vector3 endPosition;
    public bool followPlayer;
    [SerializeField] private float find_Playersecond;
    [SerializeField] GameObject hit;


    private CircleCollider2D circleCollider2D;
    private Animator boss_anim;
    private Rigidbody2D boss_rb;
    private SpriteRenderer spriteRenderer;
    private float currentAngle = 0;
    private bool Attack_the_Player = false;
    private int originalSortingOrder;

    [SerializeField] private GameObject casting_Bar;
    private Image castingBarImage;
    [SerializeField] private GameObject casting_Bar_BG;

    public GameObject shockWaveObject; // 이펙트 프리팹

    

    private void Start()
    {
        boss_rb = GetComponent<Rigidbody2D>();
        boss_anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        castingBarImage = casting_Bar.GetComponent<Image>();
        StartCoroutine(WanderRoutine());
        originalSortingOrder = spriteRenderer.sortingOrder;
    }

    private void Update()
    {
        Boss_Die();
        RaycastHit2D[] hit = Physics2D.RaycastAll(boss_rb.position, Vector2.down, 1, LayerMask.GetMask("Wall"));
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && !hit[i].collider.isTrigger)
            {
                Debug.Log("닿음");
                spriteRenderer.sortingOrder = -1;
                spriteRenderer.sortingLayerName = "Wall";
            }
            else
            {
                spriteRenderer.sortingOrder = originalSortingOrder;
                spriteRenderer.sortingLayerName = "Enemy";
            }
        }
        if (isCasting)
        {
            castingProgress += Time.deltaTime;
            castingBarImage.fillAmount = castingProgress / castingTime;

            if (castingProgress >= castingTime)
            {
                EndCasting();
            }
        }
    }

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
            moveCoroutine = StartCoroutine(FindPlayer(boss_rb, enemy_speed));

            yield return new WaitForSeconds(find_Playersecond); // 주기적으로 플레이어를 찾는 주기에 따라 대기
        }
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
                boss_anim.SetBool("FindPlayer", true);
                Vector3 currentPosition = new Vector3(rigbodyToMove.position.x, rigbodyToMove.position.y, 0f);
                Vector3 direction = (endPosition - currentPosition).normalized;

                float moveX = direction.x;
                float moveY = direction.y;

                boss_anim.SetFloat("MoveX", moveX);
                boss_anim.SetFloat("MoveY", moveY);

                Vector3 newPosition = Vector3.MoveTowards(currentPosition, endPosition, speed * Time.deltaTime);
                boss_rb.MovePosition(newPosition);
                if (moveX > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (moveX < 0)
                {
                    spriteRenderer.flipX = true;
                }

                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        boss_anim.SetBool("FindPlayer", false); // 플레이어를 찾지 못한 경우 상태를 원래대로 설정
    }

    public void ChooseNewEndPoint()
    {
        float maxDistance = 5f;
        currentAngle = Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition = transform.position + Vector3FromAngle(currentAngle) * maxDistance;
    }

    Vector3 Vector3FromAngle(float inputAngle)
    {
        float inputAngleRadians = inputAngle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
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
            moveCoroutine = StartCoroutine(FindPlayer(boss_rb, enemy_speed));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null)
                return;
            boss_HP -= bullet.Damage;

            boss_rb.velocity = Vector2.zero;
            boss_rb.angularVelocity = 0f;

            boss_rb.isKinematic = true;
            StartCoroutine(ResetKinematic());
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object"))
        {
            ChooseNewEndPoint();
        }
    }

    private IEnumerator ResetKinematic()
    {
        yield return new WaitForFixedUpdate();
        boss_rb.isKinematic = false;
    }

    private void Boss_Die()
    {
        if (boss_HP <= 0)
        {
            boss_anim.SetBool("State", false);
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            boss_rb.velocity = Vector2.zero;
            this.enabled = false;
            transform.parent.parent.GetComponent<Room>().EnemyTemp(-1);
        }
    }
    public void Attack_of_Enemy()
    {
        Debug.Log("공격");
        if (!Attack_the_Player)
        {
            boss_Skil_count += 1;
            enemy_speed = 0;
            boss_rb.velocity = Vector2.zero;
            Attack_the_Player = true;
            boss_anim.SetBool("Attack", true);

            if (boss_Skil_count >= 5) // 공격횟수가 5번이되고나서 다음공격은 스킬
            {
                StartCasting();
            }
        }
    }

    void End_Attack()
    {
        Attack_the_Player = false;
        hit.SetActive(false);
        enemy_speed = 3;
        StartCoroutine(FindPlayer(boss_rb, enemy_speed));
        boss_anim.SetBool("Attack", false);
    }

    public void Damage_of_Boss()
    {
        hit.SetActive(true);
    }


    private void Destroy_Enemy()
    {
        Destroy(gameObject);
    }
    private void StartCasting()
    {
        isCasting = true;
        castingProgress = 0.0f;
        casting_Bar_BG.SetActive(true);
        castingBarImage.gameObject.SetActive(true);
        boss_anim.SetBool("Skill", true);

    }

    private void EndCasting()
    {
        isCasting = false;
        casting_Bar_BG.SetActive(false);
        castingBarImage.gameObject.SetActive(false);
        boss_Skil_count = 0;

        Debug.Log("SpecialAttack 실행");
        shockWaveObject.SetActive(true);
    }

    void EndSkill() //애니메이션 이벤트
    {
        boss_anim.SetBool("Skill", false);
        shockWaveObject.SetActive(false);
    }

}
