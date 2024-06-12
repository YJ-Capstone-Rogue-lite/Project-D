using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool isActive = false; // 트랩의 활성화 여부를 나타내는 변수

    private Animator animator; // 트랩에 연결된 애니메이터 컴포넌트

    void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트를 가져옴
        // 애니메이터가 없다면 오류를 출력
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the Trap object");
        }
        animator.SetBool("Active", false); // 애니메이션 파라미터 "Active"를 false로 초기화
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DamageData damageData = GetComponent<DamageData>();
            if (damageData == null)
            {
                Debug.LogError("DamageData component is missing on the Trap object");
                return;
            }

            // 플레이어를 감지하면 애니메이션을 재생
            if (animator != null)
            {
                animator.SetBool("Active", true); // "Active" 애니메이션 파라미터를 true로 설정하여 애니메이션을 재생
                ActivateTrap();
            }
           
        }
    }

    // 플레이어가 콜라이더를 떠날 때 호출됨
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 콜라이더를 떠나면 애니메이션을 중지
            if (animator != null)
            {
                animator.SetBool("Active", false); // "Active" 애니메이션 파라미터를 false로 설정하여 애니메이션을 중지
                DeactivateTrap();

            }
        }
    }

    // 트랩을 활성화하는 메서드
    public void ActivateTrap()
    {
        isActive = true; // 트랩을 활성화 상태로 설정
        gameObject.GetComponent<BoxCollider2D>().enabled = true; // 트랩의 충돌체를 활성화
    }

    // 트랩을 비활성화하는 메서드
    public void DeactivateTrap()
    {
        isActive = false; // 트랩을 비활성화 상태로 설정
        //gameObject.GetComponent<BoxCollider2D>().enabled = false; // 트랩의 충돌체를 비활성화
    }
}
