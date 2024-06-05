using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    // 대화의 문장들을 저장할 큐
    public Queue<string> sentences;
    // 현재 표시되고 있는 문장
    public string currentSentences;
    // 텍스트를 표시할 TextMeshPro 컴포넌트
    public TextMeshProUGUI text;
    // 텍스트 박스를 나타내는 게임 오브젝트
    public GameObject textbox;
    // 대화가 활성화되었는지 추적하는 불리언 변수
    private bool isDialogueActive = false;

    // 주어진 대화 라인들과 채팅 위치로 대화를 시작하는 메서드
    public void Ondialogue(string[] lines, Transform chatPoint)
    {
        // 문장 큐 초기화
        sentences = new Queue<string>();
        sentences.Clear();
        // 주어진 라인들을 큐에 추가
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }
        // 대화가 시작되면 활성화
        isDialogueActive = true;
        // 대화 흐름을 시작하는 코루틴 호출
        StartCoroutine(DialogueFlow());
    }

    // 대화 흐름을 처리하는 코루틴
    IEnumerator DialogueFlow()
    {
        yield return null;
        // 큐에 문장이 남아있는 동안
        while (sentences.Count > 0)
        {
            // 큐에서 문장을 꺼내 현재 문장으로 설정
            currentSentences = sentences.Dequeue();
            // 텍스트 컴포넌트에 현재 문장을 설정
            text.text = currentSentences;
            // 3초 대기
            yield return new WaitForSeconds(3f);
        }
        // 대화가 끝나면 비활성화
        isDialogueActive = false;
        // 대화 상자 제거
        Destroy(gameObject);
    }

    // 대화 상태를 반환하는 메서드
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
