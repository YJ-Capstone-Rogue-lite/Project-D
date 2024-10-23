using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Char : MonoBehaviour
{
    public string[] line; // NPC의 대사
    public Sprite npcFace; // NPC의 얼굴 이미지
    public string npcNameText; // NPC 이름
    public bool isChatting = false;
    public bool isPlayerInRange = false; // 플레이어가 범위에 있는지 여부

    [SerializeField] private NPC_ChatManager chatManager;

    private void Update()
    {
        if (isPlayerInRange && !isChatting && Input.GetKeyDown(KeyCode.E) && chatManager != null)
        {
            chatManager.StartChat(this); // 대화 시작
            isChatting = true;
        }
        if(chatManager == null)
        {
            chatManager = GameObject.FindObjectOfType<NPC_ChatManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            chatManager.EndChat();
        }
    }

    public void OnChatEnd()
    {
        isChatting = false;
    }
}
