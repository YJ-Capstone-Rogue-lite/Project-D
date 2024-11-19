using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekupObserver : EventObserver<Item_interaction>
{
    public override void AddEvent() => target.onPeekup.AddListener(Action);

    public override void RemoveEvent() => target.onPeekup.RemoveListener(Action);
}
