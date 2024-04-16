using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FIre_Test : MonoBehaviour
{
    public Weapon weapon; // Weapon 스크립터블 객체

    // 총 변수
    [SerializeField] private GameObject bulletPrefebs; // 총알 프리팹
    [SerializeField] private Transform firingPoint; // 발사 지점
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트

    private float fireTimer; // 발사 타이머
    private float fireRate; // 발사 속도
    float angle;
    private Vector2 mousePos; // 마우스 위치

    //public float player_hp = 100; // 플레이어 체력

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
    }

        private void Update()
    {
        // Weapon 스크립터블 객체에서 fireRate 속성을 가져와 초기화
        fireRate = weapon.Fire_rate;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치 계산

        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f; // 플레이어가 마우스를 바라보도록 각도 계산

        transform.rotation = Quaternion.Euler(0, 0, angle); // Gun 회전 설정

        // 마우스 왼쪽 버튼을 누르고 발사 타이머가 0보다 작거나 같으면 발사
        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot(); // 발사 함수 호출
            fireTimer = fireRate; // 발사 타이머 설정
        }
        else
        {
            fireTimer -= Time.deltaTime; // 발사 타이머 감소
        }
        //player_die(); // 플레이어 체력 확인 함수 호출

    }

    private void Shoot()
    {
        var temp = Instantiate(bulletPrefebs, firingPoint.position, firingPoint.rotation ); // 총알 생성
        temp.GetComponent<Bullet>().setup(weapon);

        Debug.Log("총을 쏨! " + "무기 이름 : " +weapon.name + weapon.Damage + " 데미지 " + " 아이템 번호: " + weapon.number + " 연사속도: " + weapon.Fire_rate + " 사거리 : " + weapon.bullet_range);
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Enemy")) // 적과 충돌했을 때
    //    {
    //        player_hp -= 20; // 플레이어 체력 감소
    //    }
    //}
    
    //private void player_die()
    //{
    //    if (player_hp <= 0) // 플레이어 체력이 0보다 작거나 같으면
    //    {
    //        Destroy(gameObject); // 게임 오브젝트 파괴
    //    }
    //}
}
