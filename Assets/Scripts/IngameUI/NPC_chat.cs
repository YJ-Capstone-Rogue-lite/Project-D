using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_chat : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;
    private bool isTalking = false;

    public void TalkNPC()
    {
        if (isTalking)
            return;

        isTalking = true;
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
        StartCoroutine(WaitForDialogueEnd(go));
    }

    private IEnumerator WaitForDialogueEnd(GameObject chatBox)
    {
        ChatSystem chatSystem = chatBox.GetComponent<ChatSystem>();
        while (chatSystem.IsDialogueActive())
        {
            yield return null; // 대화가 끝날 때까지 대기
        }
        isTalking = false; // 대화가 끝나면 isTalking을 false로 설정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TalkNPC();
        }
    }
}
