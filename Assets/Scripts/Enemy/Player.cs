using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    //Gun 변수
    [SerializeField] private GameObject bulletPrefebs;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    private Rigidbody2D rb;

    private float mx;
    private float my;

    private float fireTimer;

    private Vector2 mousePos;

    public float player_hp = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y,mousePos.x - transform.position.x) * Mathf.Rad2Deg -90f;

        transform.localRotation = Quaternion.Euler(0,0,angle);

        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
        player_die();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx,my).normalized * speed;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefebs,firingPoint.position,firingPoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) //Bullet에 접촉했을 때
        {
            player_hp -= 20;
        }
    }
    private void player_die()
    {
        if (player_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
