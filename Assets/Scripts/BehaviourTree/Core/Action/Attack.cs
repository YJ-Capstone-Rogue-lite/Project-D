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
            blackboard.thisUnit.enemy_animator.SetTrigger("Attack");
            blackboard.thisUnit.Attack_of_Enemy();
            return NodeState.Success;
        }
    }
}
