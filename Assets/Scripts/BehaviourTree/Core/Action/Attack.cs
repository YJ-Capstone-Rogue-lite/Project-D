using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Attack : ActionNode
    {
        
        protected override void OnEnd() { }

        protected override void OnStart() { }

        protected override NodeState OnUpdate()
        {
            if(blackboard.thisUnit.Attack_the_Player) return NodeState.Running;
            blackboard.thisUnit.Attack_of_Enemy();
            return NodeState.Success;
        }
    }
}
