using UnityEngine;
using System.Collections;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
    // ���� Ŭ����
    [System.Serializable]
    public class Weapon : EquipmentItem
    {
        // �Ѿ� Ǯ�� �ѱ� ��ġ
        public Pool bulletPool;
        public Transform weaponTip;

        // �߻� �ӵ� �� ȭ�� ��鸲 ȿ��
        public float fireRate = .3f;
        public float fireShakeForce = .2f;
        public float fireShakeDuration = .05f;

        // ������
        public Weapon(string name, int grade, string description, string effect, EquipmentType type, Pool bulletPool, Transform weaponTip, float fireRate, float fireShakeForce, float fireShakeDuration)
            : base(name, grade, description, effect, type)
        {
            this.bulletPool = bulletPool;
            this.weaponTip = weaponTip;
            this.fireRate = fireRate;
            this.fireShakeForce = fireShakeForce;
            this.fireShakeDuration = fireShakeDuration;
        }

        // �߻� �Լ�
        public IEnumerator Fire(Transform playerTransform)
        {
            Transform _transform = playerTransform;

            // �����̽��ٸ� �����ų� ���콺 ���� ��ư�� ������ �ִ� ���� �߻� �ݺ�
            while (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                // ���� ��� ������ �Ѿ� ��������
                var bullet = bulletPool.nextThing;
                // �ѱ��� ��ġ�� �Ѿ� ��ġ ����
                bullet.transform.position = weaponTip.position;
                // �ѱ��� ȸ�������� �Ѿ��� ȸ���� ����
                bullet.transform.rotation = _transform.rotation;

                // �Ѿ��� �߻�� ������ ����Ͽ� ȭ���� ������ ���� ���
                var angle = _transform.rotation.eulerAngles.y - 90;
                var radians = angle * Mathf.Deg2Rad;
                var vForce = new Vector2((float)Mathf.Sin(radians), (float)Mathf.Cos(radians)) * fireShakeForce;

                // ȭ�� ��鸲 ȿ�� ����
                ProCamera2DShake.Instance.ApplyShakesTimed(new Vector2[] { vForce }, new Vector3[] { Vector3.zero }, new float[] { fireShakeDuration });

                // �߻� �ӵ��� ���� ���
                yield return new WaitForSeconds(fireRate);
            }
        }
    }
}
