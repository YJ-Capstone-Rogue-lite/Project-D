using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Char : MonoBehaviour
{
    public GameObject textBox;
    public Queue<string> chat;
    public string currentChat;
    public string[] line;
    public TextMeshProUGUI currenttext;
    public TextMeshProUGUI npcName;
    public Image npcFace;
    public bool npc = false;
    private bool isChatting = false; // 대화 진행 상태를 추적하는 변수

    public Sprite mephistoFace;
    public Sprite DoolFace;

    private void Start()
    {
        NPC_Chat(); // 대화 초기화
    }

    private void Update()
    {
        if (npc && Input.GetKeyDown(KeyCode.E) && !isChatting)
        {
            textBox.SetActive(true);
            isChatting = true;
            ShowNextChat(); 
        }

        // 대화 상자가 활성화된 상태에서 G키를 누르면 다음 대화를 보여줌
        if (textBox.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.G) && chat.Count > 0)
            {
                ShowNextChat(); // 다음 대화를 보여줌
                Debug.Log(chat.Count);
            }
            else if (Input.GetKeyDown(KeyCode.G) && chat.Count <= 0)
            {
                // 대화가 끝나면 상자를 닫고 초기화
                textBox.SetActive(false);
                NPC_Chat(); // 대화 큐 재설정
                isChatting = false; // 대화 상태를 false로 설정
                //if(gameObject.name == "Doll")
                //{
                //    //버프 찍는 창 true
                //}
            }
        }
    }

    private void ShowNextChat()
    {
        if (chat.Count > 0)
        {
            if(gameObject.name == "Mephisto")
            {
                npcName.text = "메피스토";
                npcFace.sprite = mephistoFace;
            }
            else if (gameObject.name == "Doll")
            {
                npcName.text = "인형";
                npcFace.sprite = DoolFace;
            }
            currentChat = chat.Dequeue(); // 큐에서 대화를 꺼내옴
            currenttext.text = currentChat; // 텍스트 업데이트
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npc = true; // 플레이어가 NPC 범위에 들어오면 대화 가능
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textBox.SetActive(false);
            NPC_Chat(); // 대화 큐 재설정
            isChatting = false; // 대화 상태를 false로 설정
            npc = false;
            textBox.SetActive(false);
            //버프찍는창 False
        }
    }

    public void NPC_Chat()
    {
        chat = new Queue<string>();
        chat.Clear();
        foreach (string npctext in line)
        {
            chat.Enqueue(npctext); // 큐에 대화 줄 추가
        }
    }
}
