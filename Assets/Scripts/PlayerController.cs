using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using UnityEngine;

namespace YourNamespace
{
    public class PlayerController : MonoBehaviour
    {
        // �÷��̾��� ����
        public Weapon playerWeapon;

        // Update �޼ҵ忡�� �÷��̾� ���� ����
        void Update()
        {
            // �߻� ��ư�� ������ �� ���� �߻�
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                // ������ �߻� �Լ� ȣ��
                StartCoroutine(playerWeapon.Fire(transform));
            }
        }
    }
}
