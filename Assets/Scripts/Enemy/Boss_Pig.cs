using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Pig : MonoBehaviour
{
    public Coroutine moveCoroutine;

    [Header("적의 스탯")]
    public float enemy_speed = 3f;
    public float rotateSpeed = 0.25f;
    public float boss_HP = 1500;
    public float max_enemy_hp = 1500;
    public Vector3 endPosition;
    public bool followPlayer;
    [SerializeField] private float find_Playersecond;

    private CircleCollider2D circleCollider2D;
    private Animator Boss_anim;
    private Rigidbody2D boss_rb;
    private SpriteRenderer spriteRenderer;
    private float currentAngle = 0;
    private bool Attack_the_Player = false;
    private int originalSortingOrder;

    private void Start()
    {
        boss_rb = GetComponent<Rigidbody2D>();
        Boss_anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WanderRoutine());
        originalSortingOrder = spriteRenderer.sortingOrder;
    }

    private void Update()
    {
        Boss_Die();
        Debug.DrawRay(boss_rb.position, Vector2.down, new Color(1, 0, 0));
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
    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint();

            while (Vector3.Distance(transform.position, endPosition) > 0.1f)
            {
                Vector3 currentPosition = new Vector3(boss_rb.position.x, boss_rb.position.y, 0f);
                Vector3 direction = (endPosition - currentPosition).normalized;

                float moveX = direction.x;
                float moveY = direction.y;

                Boss_anim.SetFloat("MoveX", moveX);
                Boss_anim.SetFloat("MoveY", moveY);
                Boss_anim.SetBool("RunState", true);

                Vector3 newPosition = Vector3.MoveTowards(currentPosition, endPosition, enemy_speed * Time.deltaTime);
                boss_rb.MovePosition(newPosition);
                if (moveX > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (moveX < 0)
                {
                    spriteRenderer.flipX = true;
                }

                yield return new WaitForFixedUpdate();
            }

            Boss_anim.SetBool("RunState", false);
            yield return new WaitForSeconds(find_Playersecond);
        }
    }

    public void ChooseNewEndPoint()
    {
        float maxDistance = 5f;
        currentAngle = Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition = transform.position + Vector3FromAngle(currentAngle) * maxDistance;

        Debug.Log("New endPosition chosen: " + endPosition);
    }

    Vector3 Vector3FromAngle(float inputAngle)
    {
        float inputAngleRadians = inputAngle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.tag);
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
            Boss_anim.SetBool("State", false);
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            boss_rb.velocity = Vector2.zero;
            this.enabled = false;
            transform.parent.parent.GetComponent<Room>().EnemyTemp(-1);
        }
    }

    void Attack_of_Enemy()
    {
        if (!Attack_the_Player)
        {
            boss_rb.velocity = Vector2.zero;
            Attack_the_Player = true;
            Boss_anim.SetBool("Attack", true);
        }
    }

    void End_Attack()
    {
        Attack_the_Player = false;
        Boss_anim.SetBool("Attack", false);
    }

    private void Destroy_Enemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (circleCollider2D != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, circleCollider2D.radius);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, endPosition);
        Gizmos.DrawSphere(endPosition, 0.1f);
    }
}
