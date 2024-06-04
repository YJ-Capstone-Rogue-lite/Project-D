using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    public Queue<string> sentences;
    public string currentSentences;
    public TextMeshPro text;
    public GameObject textbox;
    private bool isDialogueActive = false; // 대화 상태를 추적하는 변수

    public void Ondialogue(string[] lines, Transform chatPoint)
    {
        transform.position = chatPoint.position;
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }
        isDialogueActive = true; // 대화 시작 시 활성화
        StartCoroutine(DialogueFlow(chatPoint));
    }

    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;
        while (sentences.Count > 0)
        {
            currentSentences = sentences.Dequeue();
            text.text = currentSentences;
            float x = text.preferredWidth;
            x = (x > 3) ? 3 : x + 0.3f;
            textbox.transform.localScale = new Vector2(x, text.preferredHeight + 0.3f);
            transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y + text.preferredHeight / 2);
            yield return new WaitForSeconds(3f);
        }
        isDialogueActive = false; // 대화가 끝나면 비활성화
        Destroy(gameObject);
    }

    // 대화 상태를 반환하는 메서드
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
