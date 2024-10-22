using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC_ChatManager : MonoBehaviour
{
    public GameObject textBox;
    public TextMeshProUGUI currenttext;
    public TextMeshProUGUI npcName;
    public Image npcFace;

    private bool time;

    private Queue<string> chatQueue;
    private bool isChatting = false;
    private NPC_Char currentNPC;

    public void StartChat(NPC_Char npc)
    {
        if (isChatting) return;

        currentNPC = npc;
        chatQueue = new Queue<string>(npc.line);  // NPC의 대화 줄을 받아서 큐에 넣음
        textBox.SetActive(true);
        isChatting = true;
        ShowNextChat();
    }

    private void Update()
    {

        if (isChatting && Input.GetKeyDown(KeyCode.G))
        {
            if (chatQueue.Count > 0)
            {
                ShowNextChat();
            }
            else
            {
                EndChat();
                if (currentNPC.npcNameText == "인형")
                {
                    IngameUI.single.Open_BuffBG();  // BuffBG 활성화
                }
            }
        }
    }

    private void ShowNextChat()
    {
        if (chatQueue.Count > 0)
        {
            npcName.text = currentNPC.npcNameText; // NPC 이름을 표시
            npcFace.sprite = currentNPC.npcFace;   // NPC 얼굴을 표시
            currenttext.text = chatQueue.Dequeue(); // 다음 대화를 보여줌
        }
    }

    public void EndChat()
    {
        textBox.SetActive(false);
        isChatting = false;
        currentNPC.OnChatEnd();
    }

}
