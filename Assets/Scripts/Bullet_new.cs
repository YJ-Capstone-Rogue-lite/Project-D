using UnityEngine;
using System.Collections;

public class Bullet_new : MonoBehaviour
{
    public Item_Manager item_Manager;

    // �Ѿ��� ����ӵ�
    public float BulletDuration;

    // �Ѿ��� ���ư��� �ӵ�
    public float BulletSpeed;

    // �Ѿ��� ���ط�
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

        // ������ �Ŵ������� �Ѿ� �Ӽ� ��������
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

        // �ǰ� ��󿡰� ���ظ� ������ �޽��� ����
        _raycastHit.collider.SendMessageUpwards("Hit", BulletDamage, SendMessageOptions.DontRequireReceiver);

        Disable();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
