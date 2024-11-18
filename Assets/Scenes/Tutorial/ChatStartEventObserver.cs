using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatStartEventObserver : EventObserver<NPC_ChatManager>
{
    public override void AddEvent() => target.OnStartChat.AddListener(Action);
    public override void RemoveEvent() => target.OnStartChat.RemoveListener(Action);
}
