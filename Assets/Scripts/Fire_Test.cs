using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FIre_Test : MonoBehaviour
{
    public Weapon weapon; // Weapon ��ũ���ͺ� ��ü

    [SerializeField] private float speed = 5f; // �÷��̾� �̵� �ӵ�

    // �� ����
    [SerializeField] private GameObject bulletPrefebs; // �Ѿ� ������
    [SerializeField] private Transform firingPoint; // �߻� ����
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ

    private float mx; // ���� �Է�
    private float my; // ���� �Է�

    private float fireTimer; // �߻� Ÿ�̸�
    private float fireRate; // �߻� �ӵ�

    private Vector2 mousePos; // ���콺 ��ġ

    public float player_hp = 100; // �÷��̾� ü��

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������

       
    }

    private void Update()
    {
        // Weapon ��ũ���ͺ� ��ü���� fireRate �Ӽ��� ������ �ʱ�ȭ
        fireRate = weapon.Fire_rate;

        mx = Input.GetAxisRaw("Horizontal"); // ���� �Է�
        my = Input.GetAxisRaw("Vertical"); // ���� �Է�
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ ���

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f; // �÷��̾ ���콺�� �ٶ󺸵��� ���� ���

        transform.localRotation = Quaternion.Euler(0, 0, angle); // �÷��̾� ȸ�� ����

        // ���콺 ���� ��ư�� ������ �߻� Ÿ�̸Ӱ� 0���� �۰ų� ������ �߻�
        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot(); // �߻� �Լ� ȣ��
            fireTimer = fireRate; // �߻� Ÿ�̸� ����
        }
        else
        {
            fireTimer -= Time.deltaTime; // �߻� Ÿ�̸� ����
        }
        player_die(); // �÷��̾� ü�� Ȯ�� �Լ� ȣ��
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my).normalized * speed; // �÷��̾� �̵� ����
    }

    private void Shoot()
    {
        var temp = Instantiate(bulletPrefebs, firingPoint.position, firingPoint.rotation ); // �Ѿ� ����
        temp.GetComponent<Bullet>().setup(weapon);

        Debug.Log("���� ��! " + "���� �̸� : " +weapon.name + weapon.Damage + " ������ " + " ������ ��ȣ: " + weapon.number + " ����ӵ�: " + weapon.Fire_rate + " ��Ÿ� : " + weapon.bullet_range);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) // ���� �浹���� ��
        {
            player_hp -= 20; // �÷��̾� ü�� ����
        }
    }
    
    private void player_die()
    {
        if (player_hp <= 0) // �÷��̾� ü���� 0���� �۰ų� ������
        {
            Destroy(gameObject); // ���� ������Ʈ �ı�
        }
    }
}
