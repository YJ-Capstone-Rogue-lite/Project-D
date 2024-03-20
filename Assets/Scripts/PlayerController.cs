using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using UnityEngine;

namespace YourNamespace
{
    public class PlayerController : MonoBehaviour
    {
        // 플레이어의 무기
        public Weapon playerWeapon;

        // Update 메소드에서 플레이어 동작 제어
        void Update()
        {
            // 발사 버튼을 눌렀을 때 무기 발사
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                // 무기의 발사 함수 호출
                StartCoroutine(playerWeapon.Fire(transform));
            }
        }
    }
}
