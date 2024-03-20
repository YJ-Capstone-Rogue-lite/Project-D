using UnityEngine;
using System.Collections;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
    // 무기 클래스
    [System.Serializable]
    public class Weapon : EquipmentItem
    {
        // 총알 풀과 총구 위치
        public Pool bulletPool;
        public Transform weaponTip;

        // 발사 속도 및 화면 흔들림 효과
        public float fireRate = .3f;
        public float fireShakeForce = .2f;
        public float fireShakeDuration = .05f;

        // 생성자
        public Weapon(string name, int grade, string description, string effect, EquipmentType type, Pool bulletPool, Transform weaponTip, float fireRate, float fireShakeForce, float fireShakeDuration)
            : base(name, grade, description, effect, type)
        {
            this.bulletPool = bulletPool;
            this.weaponTip = weaponTip;
            this.fireRate = fireRate;
            this.fireShakeForce = fireShakeForce;
            this.fireShakeDuration = fireShakeDuration;
        }

        // 발사 함수
        public IEnumerator Fire(Transform playerTransform)
        {
            Transform _transform = playerTransform;

            // 스페이스바를 누르거나 마우스 왼쪽 버튼을 누르고 있는 동안 발사 반복
            while (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                // 다음 사용 가능한 총알 가져오기
                var bullet = bulletPool.nextThing;
                // 총구의 위치에 총알 위치 설정
                bullet.transform.position = weaponTip.position;
                // 총구의 회전값으로 총알의 회전값 설정
                bullet.transform.rotation = _transform.rotation;

                // 총알이 발사될 각도를 계산하여 화면을 흔들어줄 힘을 계산
                var angle = _transform.rotation.eulerAngles.y - 90;
                var radians = angle * Mathf.Deg2Rad;
                var vForce = new Vector2((float)Mathf.Sin(radians), (float)Mathf.Cos(radians)) * fireShakeForce;

                // 화면 흔들림 효과 적용
                ProCamera2DShake.Instance.ApplyShakesTimed(new Vector2[] { vForce }, new Vector3[] { Vector3.zero }, new float[] { fireShakeDuration });

                // 발사 속도에 따라 대기
                yield return new WaitForSeconds(fireRate);
            }
        }
    }
}
