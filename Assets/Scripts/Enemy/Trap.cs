using UnityEngine;

public class Trap : MonoBehaviour
{
   [SerializeField] private bool isActive = false; // 트랩의 활성화 여부를 나타내는 변수

    private Animator animator; // 트랩에 연결된 애니메이터 컴포넌트
    [SerializeField] private BoxCollider2D damageCollider; // 데미지를 줄 콜라이더
    [SerializeField] private GameObject hit_radius;


    void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트를 가져옴
        damageCollider = GetComponent<BoxCollider2D>(); // BoxCollider2D 컴포넌트 가져오기
        // 애니메이터가 없다면 오류를 출력
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the Trap object");
        }
        animator.SetBool("Active", false); // 애니메이션 파라미터 "Active"를 false로 초기화

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 콜라이더의 태그가 "FootCollider"인지 확인
        if (other.CompareTag("FootCollider"))
        {
            // 애니메이션 재생 및 트랩 활성화
            if (animator != null)
            {
                animator.SetBool("Active", true); // "Active" 애니메이션 파라미터를 true로 설정
               // ActivateTrap();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 충돌한 콜라이더의 태그가 "FootCollider"인지 확인
        if (other.CompareTag("FootCollider"))
        {
            // 애니메이션 중지 및 트랩 비활성화
            if (animator != null)
            {
                animator.SetBool("Active", false); // "Active" 애니메이션 파라미터를 false로 설정
                //DeactivateTrap();
            }
        }
    }

    public void On_Hit_Radius()
    {
        hit_radius.SetActive(true);
    }
    public void Off_Hit_Radius()
    {
        hit_radius.SetActive(false);
    }

    //// 트랩을 활성화하는 메서드
    //public void ActivateTrap()
    //{
    //    isActive = true; // 트랩을 활성화 상태로 설정

    //}

    //// 트랩을 비활성화하는 메서드
    //public void DeactivateTrap()
    //{
    //    isActive = false; // 트랩을 비활성화 상태로 설정

    //    //gameObject.GetComponent<BoxCollider2D>().enabled = false; // 트랩의 충돌체를 비활성화
    //}
}
