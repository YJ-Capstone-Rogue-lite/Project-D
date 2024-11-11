using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChatContainer
{
    [CreateAssetMenu(fileName = "Chat", menuName = "Scriptable Object/Chat/Chat", order = 1)]
    public class Chat : ScriptableObject
    {
        public string[] chat;
    }
}
