using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Test : MonoBehaviour
{
    public Weapon weapon; // Weapon1 스크립터블 객체(플레이어 무기값)
    public Weapon_Slot weapon_Slot;
    public Gun_Sprite_Change Gun_Sprite_Change;

    public Weapon default_weapon; //기본 무기(피스톨)

    [Header("총알 프리펩")]
    // 총 변수
    [SerializeField] private GameObject Handgun_bulletPrefebs; // 권총 총알 프리팹
    [SerializeField] private GameObject AR_bulletPrefebs; // 돌격소총 총알 프리팹
    [SerializeField] private GameObject SG_bulletPrefebs; // 샷건 총알 프리팹
    [SerializeField] private GameObject SR_bulletPrefebs; // 스나 총알 프리팹

    [Header("총알 발사 지점")]
    //[SerializeField] private Transform firingPoint; // 발사 지점
    [SerializeField] private Transform PistolfiringPoint; // 권총발사 지점
    [SerializeField] private Transform ARfiringPoint; // 라이플 발사 지점
    [SerializeField] private Transform SGfiringPoint; // 샷건 발사 지점
    [SerializeField] private Transform SRfiringPoint; // 스나 발사 지점

    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private float fireTimer; // 발사 타이머
    private float fireRate; // 발사 속도

    

    float angle;
    public Vector2 mousePos; // 마우스 위치

    //public float player_hp = 100; // 플레이어 체력
    public Animator[] shot_animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        weapon = default_weapon;
        Debug.Log(default_weapon.name + " 로 기본 무기 변경");
        weapon_Slot.UpdateMagazineCapacity(); // 게임 시작시 한번 장탄수들 초기화
        // Reload_anim 스크립트 가져오기
        shot_animator = GetComponentsInChildren<Animator>();

    }

    public void Init()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        weapon = default_weapon;
        Debug.Log(default_weapon.name + " 로 기본 무기 변경");
        weapon_Slot.UpdateMagazineCapacity(); // 게임 시작시 한번 장탄수들 초기화
        // Reload_anim 스크립트 가져오기
        shot_animator = GetComponentsInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        // Weapon 스크립터블 객체에서 fireRate 속성을 가져와 초기화
        fireRate = weapon.Fire_rate;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치 계산
        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90; // 플레이어가 마우스를 바라보도록 각도 계산
        var angleX = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg; // 플레이어가 마우스를 바라보도록 각도 계산
        var angleY = angle >= -180 ? angle : 360 + angle;
        // transform.rotation = Quaternion.Euler(0, 0, angle); // Gun 회전 설정

        var slotRenders = transform.GetComponentsInChildren<SpriteRenderer>();
        var rcts = transform.GetComponentsInChildren<RectTransform>();
        foreach (var r in slotRenders)
        {
            if(Mathf.Sign(angleY) < 0) r.flipY = false;
            else r.flipY = true;
        }
        foreach(var r in rcts)
        {
            r.localEulerAngles = new Vector3(0, 0, angleY+90);
            var x = -(Mathf.Abs(angleX)/90-1);
            var y = -(Mathf.Abs(angleY)/90-1);
            r.localPosition = new Vector3(Mathf.Clamp(x, -0.5f, 0.5f), y);

        }

        All_Shooting_Code();
    }


    void All_Shooting_Code()
    {
        // 마우스 왼쪽 버튼을 누르고 발사 타이머가 0보다 작고, 무기가 할당되어 있을 때 총알 발사. 무기타입이 None일 경우 작동안함
        if (Input.GetMouseButton(0) && fireTimer <= 0f && weapon != null && weapon.weaponType != Weapon.WeaponType.None)
        {
            // 한 슬롯이 재장전 중이면서 현재 활성화된 슬롯이 재장전 중이 아닌 경우에만 발사
            // - 현재 활성화된 슬롯이 슬롯 1인 경우, 슬롯 1이 재장전 중인지 확인합니다.
            // - 현재 활성화된 슬롯이 슬롯 2인 경우, 슬롯 2가 재장전 중인지 확인합니다.
            // 위 두 조건 중 하나라도 만족하지 않는 경우에만 아래의 코드 블록이 실행됩니다.
            if (!((weapon_Slot.isReloadingSlot1 && weapon_Slot.activeWeaponSlot == weapon_Slot.weaponSlot1) ||
                  (weapon_Slot.isReloadingSlot2 && weapon_Slot.activeWeaponSlot == weapon_Slot.weaponSlot2)))
            {
                fireTimer = fireRate; // 발사 타이머 설정

                if (weapon.weaponType == Weapon.WeaponType.Shoot_Gun) // 무기의 타입이 샷건이라면
                {
                    Shoot_Gun_Shoot(); // 샷건 총알 발사
                }
                else
                {
                    Shoot(); // 일반 발사 함수 호출
                }

                weapon_Slot.DecreaseMagazineCapacity(weapon_Slot.activeWeaponSlot); // 쏠 때마다 장탄수 값 1 감소
            }
        }
        else
        {
            fireTimer -= Time.deltaTime; // 발사 타이머 감소

        }
    }



    private void Shoot()
    {
        if(weapon.weaponType == Weapon.WeaponType.Pistol)
        {
            var temp = Instantiate(Handgun_bulletPrefebs, PistolfiringPoint.position, PistolfiringPoint.rotation); // 총알 생성
            temp.GetComponent<Bullet>().setup(weapon);
            Debug.Log("총을 쏨! " + "무기 이름 : " + weapon.name + " " + weapon.Damage + " 데미지 " + " 아이템 번호: " + weapon.number + " 연사속도: " + weapon.Fire_rate + " 사거리 : " + weapon.bullet_range + " 무기 타입 : " + weapon.weaponType);
            shot_animator[3].SetTrigger("Shot");
        }
        else if (weapon.weaponType == Weapon.WeaponType.Assaultt_Rifle)
        {
            var temp = Instantiate(AR_bulletPrefebs, ARfiringPoint.position, ARfiringPoint.rotation); // 총알 생성
            temp.GetComponent<Bullet>().setup(weapon);
            Debug.Log("총을 쏨! " + "무기 이름 : " + weapon.name + " " + weapon.Damage + " 데미지 " + " 아이템 번호: " + weapon.number + " 연사속도: " + weapon.Fire_rate + " 사거리 : " + weapon.bullet_range + " 무기 타입 : " + weapon.weaponType);
            shot_animator[1].SetTrigger("Shot");
        }
        else
        {
            var temp = Instantiate(SR_bulletPrefebs, SRfiringPoint.position, SRfiringPoint.rotation); // 총알 생성
            temp.GetComponent<Bullet>().setup(weapon);
            Debug.Log("총을 쏨! " + "무기 이름 : " + weapon.name + " " + weapon.Damage + " 데미지 " + " 아이템 번호: " + weapon.number + " 연사속도: " + weapon.Fire_rate + " 사거리 : " + weapon.bullet_range + " 무기 타입 : " + weapon.weaponType);
            shot_animator[0].SetTrigger("Shot");
        }
    }


    private void Shoot_Gun_Shoot()
    {
        shot_animator[2].SetTrigger("Shot");
        for (int i = 0; i < 5; i++)
        {   
            // 플레이어의 에임 방향을 기준으로 발사 각도 설정
            float spreadAngle = -20f + i * (40f / 4f); //  사이의 각도를 고르게 분배

            // 에임 방향에 따라 회전을 적용하여 발사 각도 조정
            Quaternion spreadRotation = Quaternion.Euler(0, 0, spreadAngle);

           

            // 총알 생성
            var temp = Instantiate(SG_bulletPrefebs, SGfiringPoint.position, SGfiringPoint.rotation * spreadRotation);
            temp.GetComponent<Bullet>().setup(weapon);
        }
        Debug.Log("총을 쏨! " + "무기 이름 : " + weapon.name + weapon.Damage +" " + " 데미지 " + " 아이템 번호: " + weapon.number + " 연사속도: " + weapon.Fire_rate + " 사거리 : " + weapon.bullet_range + " 무기 타입 : " + weapon.weaponType);
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
