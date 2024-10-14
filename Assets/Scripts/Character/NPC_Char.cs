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
    public Image npcFace;
    private bool npc = false;

    private void Start()
    {
        NPC_Chat();
    }

    private void Update()
    {
        if (npc && Input.GetKeyDown(KeyCode.E) && !textBox.activeSelf)
        {
            textBox.SetActive(true);
        }

        if (textBox.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.G) && chat.Count > 0)
            {
                currentChat = chat.Dequeue();
                currenttext.text = currentChat;
            }
            else if (Input.GetKeyDown(KeyCode.G) && chat.Count <= 0)
            {
                textBox.SetActive(false);
                currenttext.text = "반갑다"; // text 초기화
                NPC_Chat();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npc = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npc = false;
        }
    }

    public void NPC_Chat()
    {
        chat = new Queue<string>();
        chat.Clear();
        foreach (string npctext in line)
        {
            chat.Enqueue(npctext);
        }
    }
}
