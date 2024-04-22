using UnityEngine;
using System.Collections;

public class Bullet_new : MonoBehaviour
{
    public Item_Manager item_Manager;

    // 총알의 연사속도
    public float BulletDuration;

    // 총알이 날아가는 속도
    public float BulletSpeed;

    // 총알의 피해량
    public float BulletDamage;

    public float SkinWidth;
    public LayerMask CollisionMask;

    Transform _transform;

    RaycastHit _raycastHit;
    Vector2 _collisionPoint;

    float _startTime;
    bool _exploding;
    Vector3 _lastPos;

    void Awake()
    {
        _transform = this.transform;
    }

    void Start()
    {
        _startTime = Time.time;

        // 아이템 매니저에서 총알 속성 가져오기
        if (item_Manager != null && item_Manager.stats.Count > 0)
        {
            BulletDamage = item_Manager.stats[0].damage;
            BulletDuration = item_Manager.stats[0].Fire_Rate;
            BulletSpeed = item_Manager.stats[0].bullet_speed;
            BulletDamage = item_Manager.stats[0].SkinWidth;
        }
    }

    void OnEnable()
    {
        _exploding = false;
    }

    void Update()
    {
        if (_exploding)
            return;

        _lastPos = _transform.position;
        _transform.Translate(Vector3.right * BulletSpeed * Time.deltaTime);

        if (Physics.Raycast(_lastPos, _transform.position - _lastPos, out _raycastHit, (_lastPos - _transform.position).magnitude + SkinWidth, CollisionMask))
        {
            _collisionPoint = _raycastHit.point;

            _transform.up = _raycastHit.normal;

            Collide();
        }

        if (Time.time - _startTime > BulletDuration)
            Disable();
    }

    void Collide()
    {
        _exploding = true;
        _transform.position = _collisionPoint;

        // 피격 대상에게 피해를 입히는 메시지 전달
        _raycastHit.collider.SendMessageUpwards("Hit", BulletDamage, SendMessageOptions.DontRequireReceiver);

        Disable();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
