using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Magician : Enemy
{
    public bool setTP = false;
    public int attackCount = 1;

    public float castingTime = 3f;
    private float tpCastingTime = 2.0f;
    private float castingProgress = 0.0f;
    [SerializeField] private int [] magictype = {2}; //0 = Idle, 1 = fireBall, 2 = Meteor
    public bool posChange = false;
    public bool isCasting = false;
    public float dangerCount = 0.5f;

    [Header("마법종류")]
    public GameObject fireballPrefeb;
    public GameObject dangerPrefeb;
    [Header("텔레포트위치")]
    public Vector2[] teleport;
    public Vector3 nextPostion;

    [SerializeField] private GameObject casting_Bar;

    public int fireballCount = 8; // 생성할 파이어볼의 개수
    public float radius = 5f;      // 원형으로 배치할 반지름
    public float fireballSpeed = 5f; // 파이어볼의 속도
    public float spawnDelay = 0.1f;  // 파이어볼 생성 간격

    private void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
        enemy_animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
        behaviorTree.blackboard.thisUnit = GetComponent<Enemy>();
        if (originPos == null)
        {
            var posObj = new GameObject()
            {
                name = transform.name + " Pos",
                layer = LayerMask.NameToLayer("Ignore Raycast"),
            };
            originPos = posObj.transform;
        }
        originPos.transform.position = transform.position;
        behaviorTree = behaviorTree.Clone();
        blackboard = behaviorTree.blackboard;

        teleport = new Vector2[6];
        teleport[0] = new Vector2(originPos.position.x + 15, originPos.position.y - 10);
        teleport[1] = new Vector2(originPos.position.x + 15, originPos.position.y - 8);
        teleport[2] = new Vector2(originPos.position.x, originPos.position.y - 14);
        teleport[3] = new Vector2(originPos.position.x - 15, originPos.position.y - 10);
        teleport[4] = new Vector2(originPos.position.x - 15, originPos.position.y + 8);
        teleport[5] = new Vector2(originPos.position.x + 15, originPos.position.y + 8);

    }

    private void Update()
    {
        TeleportCasting();
        var hit = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Player"));
        if (hit && hit.transform.gameObject.CompareTag("Player"))
        {
            blackboard.target = hit.transform;
            blackboard.state = BehaviourTree.Blackboard.State.Aggro;
            int randomMagicType = magictype[Random.Range(0, magictype.Length)];
            if (!isCasting && attackCount != 1)
            {
                casting_Bar.SetActive(true);
                if (randomMagicType == 1)
                {
                    enemy_animator.SetTrigger("Fireball");
                    isCasting = true;
                    StartCoroutine(FireBallCreate());
                }
                else if (randomMagicType == 2)
                {
                    enemy_animator.SetTrigger("Meteor");
                    isCasting = true;
                    StartCoroutine(CreateDanger());
                }
            }
        }
        behaviorTree.Update();
    }

    public void Creat_fireBall()
    {

        StartCoroutine(SpawnFireballs());
    }

    private IEnumerator SpawnFireballs()
    {
        float angleStep = 360f / fireballCount;
        float angle = 0f;

        List<GameObject> fireballs = new List<GameObject>(); // 생성된 파이어볼을 저장할 리스트

        for (int i = 0; i < fireballCount; i++)
        {
            float fireballDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float fireballDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 fireballPosition = new Vector3(fireballDirX, fireballDirY, 0) * radius + firePoint.position;

            GameObject fireball = Instantiate(fireballPrefeb, fireballPosition, Quaternion.identity);
            fireballs.Add(fireball);

            angle += angleStep;

            yield return new WaitForSeconds(spawnDelay);
        }

        FireAllBalls(fireballs);
        setTP = true;
        attackCount = 1;
        isCasting = false;
        casting_Bar.SetActive(false);
        enemy_animator.SetTrigger("Attack");
        foreach (GameObject fireball in fireballs)
        {
            if (fireball != null)
            {
                Destroy(fireball, 3f);
            }
        }
    }

    private void FireAllBalls(List<GameObject> fireballs)
    {
        foreach (GameObject fireball in fireballs)
        {
            if (fireball != null)
            {
                Vector3 fireballDirection = (fireball.transform.position - firePoint.position).normalized;

                Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = fireballDirection * fireballSpeed;
                }
            }
        }
    }

    public void TeleportEnd()
    {
        if (!setTP && attackCount == 1)
        {
            enemy_animator.SetTrigger("TPTrigger");
            setTP = true;
            attackCount = 0;
        }
    }

    public void Teleport()
    {
        nextPostion = teleport[Random.Range(0, teleport.Length)];
        while (nextPostion.x < transform.position.x + 1 && nextPostion.x > transform.position.x - 1 && nextPostion.y < transform.position.y + 1 && nextPostion.y > transform.position.y - 1)
        {
            nextPostion = teleport[Random.Range(0, teleport.Length)];
        }
        transform.position = nextPostion;
        enemy_animator.SetTrigger("TPTrigger");
    }

    public void TeleportCasting()
    {
        if (setTP && attackCount == 1)
        {
            enemy_animator.SetTrigger("TPTrigger");
            setTP = false;
        }
    }

    private IEnumerator FireBallCreate()
    {
        Creat_fireBall();
        yield return new WaitForSeconds(castingTime);
    }

    private IEnumerator CreateDanger()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject danger = Instantiate(dangerPrefeb, GameManager.Instance.player.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f); // 각 Danger 생성 후 0.5초 대기
        }

        // 대기 시간 후의 추가 작업
        yield return new WaitForSeconds(dangerCount);
        setTP = true;
        attackCount = 1;
        isCasting = false;
        casting_Bar.SetActive(false);
        enemy_animator.SetTrigger("Attack");
    }
}
