using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatEndEventObserver : EventObserver<NPC_ChatManager>
{
    public override void AddEvent() => target.OnEndChat.AddListener(Action);
    public override void RemoveEvent() => target.OnEndChat.RemoveListener(Action);
}
