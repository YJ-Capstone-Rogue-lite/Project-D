using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class EndAttack : ActionNode
    {
        protected override void OnEnd() {}

        protected override void OnStart() {}

        protected override NodeState OnUpdate()
        {
            blackboard.thisUnit.End_Attack();
            return NodeState.Success;
        }
    }
}
