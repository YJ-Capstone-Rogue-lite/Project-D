using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEventObserver : EventObserver<PlayerChar>
{
    public override void AddEvent() => target.onRollingAction.AddListener(Action);

    public override void RemoveEvent() => target.onRollingAction.RemoveListener(Action);
}
