using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_chat : MonoBehaviour
{
    public Queue<string> chat;
    public string currentText;

    public void NPC_Chat()
    {

    }

    //public void SaveChat(string[] lines)
    //{
    //    chat = new Queue<string>();
    //    chat.Clear();

    //    foreach (string line in lines)
    //    {
    //        chat.Enqueue(line);
    //    }
    //}

}
