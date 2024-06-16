using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider;
    public Vector2[] colliderSizes; // 각 프레임에 대한 콜라이더 크기 배열

    public Animator animator;
    private float animationLength;


    void Start()
    {

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        animationLength = clips[0].length; // 첫 번째 애니메이션 클립의 길이를 가져옵니다.
    }

    void Update()
    {
        // 애니메이션 진행에 따라 적절한 시기에 콜라이더 크기 변경
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; // 현재 애니메이션 진행 상태를 가져옵니다.
        float time = normalizedTime * animationLength;

        for (int i = 0; i < colliderSizes.Length; i++)
        {
            if (time <= (i + 1) * (animationLength / colliderSizes.Length))
            {
                SetColliderSize(colliderSizes[i]);
                break;
            }
        }
    }

    // 캡슐 콜라이더의 크기를 설정하는 함수
    void SetColliderSize(Vector2 size)
    {
        capsuleCollider.size = size;
    }

}
