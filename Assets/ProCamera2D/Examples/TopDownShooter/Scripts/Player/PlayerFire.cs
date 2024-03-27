using UnityEngine;
using System.Collections;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{


    public class PlayerFire : MonoBehaviour
    {
        // 총알을 관리하는 객체
        public Pool BulletPool;
        // 총알이 발사될 위치
        public Transform WeaponTip;

        // 발사 속도
        public float FireRate = .3f;

        // 발사 시 화면 흔들림 효과
        public float FireShakeForce = .2f;
        public float FireShakeDuration = .05f;

        Transform _transform;

        void Awake()
        {
            // 캐시: 자주 사용되는 transform을 캐시하여 성능을 향상시킵니다.
            _transform = transform;
        }

        void Update()
        {
            // 스페이스바를 누르거나 마우스 왼쪽 버튼을 클릭했을 때 발사
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                // 발사 코루틴 시작
                StartCoroutine(Fire());
            }
        }

        IEnumerator Fire()
        {
            // 스페이스바를 누르거나 마우스 왼쪽 버튼을 누르고 있는 동안 반복
            while (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                // 총알 풀에서 다음 사용 가능한 총알을 가져옴
                var bullet = BulletPool.nextThing;
                // 총구의 위치에 총알 위치 설정
                bullet.transform.position = WeaponTip.position;
                // 총구의 회전값으로 총알의 회전값 설정
                bullet.transform.rotation = _transform.rotation;

                // 총알이 발사될 각도를 계산하여 화면을 흔들어줄 힘을 계산
                var angle = _transform.rotation.eulerAngles.y - 90;
                var radians = angle * Mathf.Deg2Rad;
                var vForce = new Vector2((float)Mathf.Sin(radians), (float)Mathf.Cos(radians)) * FireShakeForce;

                // 화면 흔들림 효과 적용
                ProCamera2DShake.Instance.ApplyShakesTimed(new Vector2[] { vForce }, new Vector3[] { Vector3.zero }, new float[] { FireShakeDuration });

                // 발사 속도에 따라 대기
                yield return new WaitForSeconds(FireRate);
            }
        }
    }
}
