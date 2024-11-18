using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieObserver : EventObserver<Enemy>
{
    public override void AddEvent() => target.onEnenyDie.AddListener(Action);
    public override void RemoveEvent() => target.onEnenyDie.RemoveListener(Action);
}
