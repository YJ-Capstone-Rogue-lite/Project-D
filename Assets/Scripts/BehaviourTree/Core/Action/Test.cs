using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : ActionNode
{
    protected override void OnEnd() {}

    protected override void OnStart() {}

    protected override NodeState OnUpdate()
    {
        return NodeState.Success;
    }
}
