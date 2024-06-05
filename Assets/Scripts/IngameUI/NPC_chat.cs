using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_chat : MonoBehaviour
{
    // 대화 문장들
    public string[] sentences;
    // 대화 위치를 나타내는 트랜스폼
    public Transform chatTr;
    // 대화 상자 프리팹
    public GameObject chatBoxPrefab;
    // 대화 중인지 여부를 나타내는 불리언 변수
    private bool isTalking = false;

    // NPC와 대화를 시작하는 메서드
    public void TalkNPC()
    {
        // 이미 대화 중이라면 메서드 종료
        if (isTalking)
            return;

        // 대화 시작을 표시
        isTalking = true;
        // 대화 상자 프리팹을 인스턴스화
        GameObject go = Instantiate(chatBoxPrefab);
        // 대화 시스템을 통해 대화를 시작
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
        // 대화가 끝날 때까지 대기하는 코루틴 시작
        StartCoroutine(WaitForDialogueEnd(go));
    }

    // 대화가 끝날 때까지 대기하는 코루틴
    private IEnumerator WaitForDialogueEnd(GameObject chatBox)
    {
        // 대화 시스템 컴포넌트를 가져옴
        ChatSystem chatSystem = chatBox.GetComponent<ChatSystem>();
        // 대화가 활성화된 동안 대기
        while (chatSystem.IsDialogueActive())
        {
            yield return null; // 다음 프레임까지 대기
        }
        // 대화가 끝나면 isTalking을 false로 설정
        isTalking = false;
    }

    // 플레이어가 트리거 영역에 들어왔을 때 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 "Player" 태그를 가지고 있는지 확인
        if (collision.CompareTag("Player"))
        {
            // NPC와 대화 시작
            TalkNPC();
        }
    }
}
