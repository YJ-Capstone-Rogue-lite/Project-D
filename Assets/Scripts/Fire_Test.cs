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
    private bool isReloading = false; // 재장전 중인지 여부를 나타내는 변수

    float angle;
    private Vector2 mousePos; // 마우스 위치

    //public float player_hp = 100; // 플레이어 체력

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        weapon.magazine_capacity = weapon.backup_magazine_capacity; // 게임 시작시 한번 장탄수들 초기화
    }

    private void Update()
    {
        // Weapon 스크립터블 객체에서 fireRate 속성을 가져와 초기화
        fireRate = weapon.Fire_rate;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치 계산

        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f; // 플레이어가 마우스를 바라보도록 각도 계산

        transform.rotation = Quaternion.Euler(0, 0, angle); // Gun 회전 설정

        // 마우스 왼쪽 버튼을 누르고 발사 타이머가 0보다 작고, 장탄수가 0보다 크고, isReloading이 false일때 총알 발사
        if (Input.GetMouseButton(0) && fireTimer <= 0f && weapon.magazine_capacity > 0 && isReloading == false)
        {
            fireTimer = fireRate; // 발사 타이머 설정
            
            if(weapon.weaponType==Weapon.WeaponType.Shoot_Gun) //웨폰의 타입이 샷건이라면
            {
                Shoot_Gun_Shoot(); //샷건 총알 발사
            }
            else
            {
                Shoot(); // 일반 발사 함수 호출
            }

            weapon.magazine_capacity -= 1; // 쏠때마다 장탄수 값 1 감소

            if(weapon.magazine_capacity <= 0) //만약 장탄수 값이 0보다 작거나 같을때
            {
                Reload(); //isRealoding 값 true
            }

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
        Debug.Log("총을 쏨! " + "무기 이름 : " +weapon.name +" "+ weapon.Damage + " 데미지 " + " 아이템 번호: " + weapon.number + " 연사속도: " + weapon.Fire_rate + " 사거리 : " + weapon.bullet_range + " 무기 타입 : " + weapon.weaponType);
    }

    private void Shoot_Gun_Shoot()
    {
        for (int i = 0; i < 5; i++)
        {   
            // 플레이어의 에임 방향을 기준으로 발사 각도 설정
            float spreadAngle = -20f + i * (40f / 4f); //  사이의 각도를 고르게 분배

            // 에임 방향에 따라 회전을 적용하여 발사 각도 조정
            Quaternion spreadRotation = Quaternion.Euler(0, 0, spreadAngle);

            // 총알 생성
            var temp = Instantiate(bulletPrefebs, firingPoint.position, firingPoint.rotation * spreadRotation);
            temp.GetComponent<Bullet>().setup(weapon);
        }

        Debug.Log("총을 쏨! " + "무기 이름 : " + weapon.name + weapon.Damage +" " + " 데미지 " + " 아이템 번호: " + weapon.number + " 연사속도: " + weapon.Fire_rate + " 사거리 : " + weapon.bullet_range + " 무기 타입 : " + weapon.weaponType);
    }
    
    private void Reload() //재장전
    {
            isReloading = true; // 재장전 중임을 표시
            StartCoroutine(ReloadCoroutine()); // 재장전 코루틴 시작
            Debug.Log(weapon.reload_time + "초 만큼 재장전");        
    }
    private IEnumerator ReloadCoroutine()
    {
        // 재장전 시간만큼 대기
        yield return new WaitForSeconds(weapon.reload_time);

        weapon.magazine_capacity = weapon.backup_magazine_capacity; //장탄수 값을 백업용 탄수 값으로 돌려놓음
        // 재장전이 끝났으므로 재장전 중인 상태 해제
        isReloading = false;
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
