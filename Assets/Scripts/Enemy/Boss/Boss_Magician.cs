using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Magician : Enemy
{
    public bool setTP = false; //true면 텔레포트 불가능 false면 가능
    public int attackCount = 1;
    

    public float meteorCasting;
    public float fireballCasting;
    private float tpCastingTime = 2.0f;
    private float castingProgress = 0.0f;
    private bool isCasting;
    public bool posChange = false;
    

    [Header("마법종류")]
    public GameObject fireballPrefeb;
    public GameObject meteorPrefeb;
    [Header("텔레포트위치")]
    public Vector2 [] teleport;
    public Vector3 nextPostion;

    [SerializeField] private GameObject casting_Bar;

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
        }
        behaviorTree.Update();
    }

    //public void Creat_fireBall()
    //{
    //    //GameObject fireball = Instantiate(fireballPrefeb, firePoint.position, transform.rotation); // 보스주위로 원형생성후 공격
    //}

    //public void Creat_Meteor()
    //{
    //    GameObject meteor = Instantiate(meteorPrefeb, firePoint.position, transform.rotation);
    //}

    public void TeleportEnd()
    {
        Debug.Log("안들어감");
        if (!setTP && attackCount == 1)
        {
            enemy_animator.SetTrigger("TPTrigger");
            setTP = true;
            attackCount = 0;
        }

    }
    
    public void Teleport()
    {
        casting_Bar.SetActive(false);
        nextPostion = teleport[Random.Range(0, teleport.Length)];
        while (nextPostion.x < transform.position.x + 1 && nextPostion.x > transform.position.x - 1 && nextPostion.y < transform.position.y + 1 && nextPostion.y > transform.position.y - 1)
        {
            nextPostion = teleport[Random.Range(0, teleport.Length)];
        }
        transform.position = nextPostion;
        TeleportEnd();
    }

    public void TeleportCasting()
    {
        if (setTP && attackCount == 1)
        {
            casting_Bar.SetActive(true);
            enemy_animator.SetTrigger("TPTrigger");
            setTP = false;
        }
    }

}
