using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_SlimeKing : Enemy
{
    public float castingTime = 3f;
    private float tpCastingTime = 2.0f;
    private float castingProgress = 0.0f;
    [SerializeField] private int [] magictype = {2}; //0 = Idle, 1 = fireBall, 2 = Trap
    public bool posChange = false;
    public bool isCasting = false;
    public GameObject playertarget;
    public bool firstAtk;

    [Header("마법종류")]
    public GameObject slimeBallPrefeb;
    public GameObject trapPrefab;
    [SerializeField] private GameObject casting_Bar;

    public int slimeBallCount; // 생성할 파이어볼의 개수
    public float radius;      // 원형으로 배치할 반지름
    public float slimeBallSpeed; // 파이어볼의 속도
    public float spawnDelay = 0.1f;  // 파이어볼 생성 간격



    private void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
        enemy_animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
        behaviorTree.blackboard.thisUnit = GetComponent<Enemy>();

        behaviorTree = behaviorTree.Clone();
        blackboard = behaviorTree.blackboard;
    }

    protected override void Update()
    {
        if (playertarget == null)
        {
            playertarget = GameObject.FindWithTag("Player");
        }

        var hit = Physics2D.CircleCast(transform.position, playerFindRange, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Player"));
        if (hit && hit.transform.gameObject.CompareTag("Player"))
        {
            blackboard.target = hit.transform;
            blackboard.state = BehaviourTree.Blackboard.State.Aggro;
            int randomMagicType = magictype[Random.Range(0, magictype.Length)];

            if (!isCasting && firstAtk)
            {
                isCasting = true;
                casting_Bar.SetActive(true);
                enemy_animator.SetTrigger("Casting");

                if (randomMagicType == 1)
                {
                    StartCoroutine(SlimeBallCreate());
                }
                else if (randomMagicType == 2)
                {
                    StartCoroutine(TrapCreate());
                }
            }
        }

        behaviorTree.Update();
    }
    public void Creat_slimeBall()
    {

        StartCoroutine(SpawnSlimeBall());
    }

    private IEnumerator SpawnSlimeBall()
    {
        float angleStep = 360f / slimeBallCount;
        float angle = 0f;

        List<GameObject> slimeBalls = new List<GameObject>(); // 생성된 파이어볼을 저장할 리스트

        for (int i = 0; i < slimeBallCount; i++)
        {
            float slimeballDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float slimeballDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 slimeballPosition = new Vector3(slimeballDirX, slimeballDirY, 0) * radius + firePoint.position;

            GameObject slimeball = Instantiate(slimeBallPrefeb, slimeballPosition, Quaternion.identity);
            slimeBalls.Add(slimeball);

            angle += angleStep;

            yield return new WaitForSeconds(spawnDelay);
        }

        SlimeAllBall(slimeBalls);
        isCasting = false;
        casting_Bar.SetActive(false);
        foreach (GameObject slimeball in slimeBalls)
        {
            if (slimeball != null)
            {
                Destroy(slimeball, 2.5f);
            }
        }
    }

    private void SlimeAllBall(List<GameObject> slimeballs)
    {
        foreach (GameObject slimeball in slimeballs)
        {
            if (slimeball != null)
            {
                Vector3 slimeballDirection = (slimeball.transform.position - firePoint.position).normalized;

                Rigidbody2D rb = slimeball.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = slimeballDirection * slimeBallSpeed;
                }
            }
        }
    }
    private IEnumerator SlimeBallCreate()
    {
        Creat_slimeBall();
        yield return new WaitForSeconds(castingTime);
        EndCasting();
    }
    private IEnumerator TrapCreate()
    {
        yield return new WaitForSeconds(castingTime);
        CreateTrap();
        EndCasting();
    }
    private void CreateTrap()
    {
        if (trapPrefab == null)
        {
            return;
        }

        Vector3 playerPosition = playertarget.transform.position;
        float trapSpacing = 1.0f;
        int gridSize = 5;

        List<GameObject> traps = new List<GameObject>();

        // 트랩 생성
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 spawnPosition = playerPosition + new Vector3((x - 2) * trapSpacing, (y - 2) * trapSpacing, 0);
                GameObject trap = Instantiate(trapPrefab, spawnPosition, Quaternion.identity);
                traps.Add(trap);
            }
        }

        StartCoroutine(DestroyTraps(traps, 5f));
    }
    private IEnumerator DestroyTraps(List<GameObject> traps, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject trap in traps)
        {
            if (trap != null)
            {
                Destroy(trap);
            }
        }
    }

    private void EndCasting()
    {
        firstAtk = false;
        StartCoroutine(ReturnCasting());
    }


    public void Boss_clear_check()
    {
        Debug.Log("작동함");
        IngameUI.single.test_clear_boolCheck = true;
    }
    private IEnumerator ReturnCasting()
    {
        isCasting = false;
        casting_Bar.SetActive(false);
        enemy_animator.SetTrigger("Idle");

        yield return new WaitForSeconds(1.5f); //Idle 0.75초 대기

        int randomMagicType = magictype[Random.Range(0, magictype.Length)];

        if (randomMagicType == 1 && !firstAtk)
        {
            isCasting = true;
            casting_Bar.SetActive(true);
            enemy_animator.SetTrigger("Casting");
            StartCoroutine(SlimeBallCreate());
        }

        else if (randomMagicType == 2 && !firstAtk)
        {
            isCasting = true;
            casting_Bar.SetActive(true);
            enemy_animator.SetTrigger("Casting");
            StartCoroutine(TrapCreate());
        }
    }
}
