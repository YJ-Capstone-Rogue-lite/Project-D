using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Pig : Enemy
{
    public int boss_Skil_count;

    public float castingTime = 2.0f;
    private float castingProgress = 0.0f;
    private bool isCasting = false;

    [SerializeField] private GameObject casting_Bar;
    private Image castingBarImage;
    [SerializeField] private GameObject casting_Bar_BG;

    public GameObject shockWaveObject; // 이펙트 프리팹
    

    private void Start()
    {
        castingBarImage = casting_Bar.GetComponent<Image>();
        enemy_rb = GetComponent<Rigidbody2D>();
        enemy_animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    public override void Attack_of_Enemy() 
    {
        Debug.Log("공격");
        if (!Attack_the_Player)
        {
            enemy_speed = 0;
            enemy_rb.velocity = Vector2.zero;
            Attack_the_Player = true;
            enemy_animator.SetTrigger("Attack");
            boss_Skil_count += 1;

            if (boss_Skil_count >= 5) // 공격횟수가 5번이되고나서 다음공격은 스킬
            {
                StartCasting();
            }
            else StartCoroutine(End_Attack(1));
        }
    }

    private void StartCasting()
    {
        isCasting = true;
        castingProgress = 0.0f;
        casting_Bar_BG.SetActive(true);
        castingBarImage.gameObject.SetActive(true);
        enemy_animator.SetBool("Skill", true);

    }

    private void EndCasting()
    {
        isCasting = false;
        casting_Bar_BG.SetActive(false);
        castingBarImage.gameObject.SetActive(false);
        boss_Skil_count = 0;

        Debug.Log("SpecialAttack 실행");
        shockWaveObject.SetActive(true);
        StartCoroutine(End_Attack(3));
        StartCoroutine(EndSkill(3));
    }

    IEnumerator EndSkill(float time) //애니메이션 이벤트
    {
        yield return new WaitForSeconds(time);
        enemy_animator.SetBool("Skill", false);
        shockWaveObject.SetActive(false);
    }

}
