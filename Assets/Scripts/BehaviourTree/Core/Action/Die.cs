using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Die : ActionNode
    {
        protected override void OnEnd() {}

        protected override void OnStart() {}

        protected override NodeState OnUpdate()
        {
            blackboard.thisUnit.Enemy_die();
            return NodeState.Success;
        }
    }
}
