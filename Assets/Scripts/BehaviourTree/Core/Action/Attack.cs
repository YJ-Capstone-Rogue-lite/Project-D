using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Attack : ActionNode
    {
        private bool isAttacked;
        protected override void OnEnd() => isAttacked = false;

        protected override void OnStart()
        {
            blackboard.thisUnit.enemy_rb.velocity = Vector2.zero;
        }

        protected override NodeState OnUpdate()
        {
            if (!isAttacked)
            {
                blackboard.thisUnit.Attack_of_Enemy();
                isAttacked = true;
            }
            if (blackboard.thisUnit.Attack_the_Player) return NodeState.Running;
            else return NodeState.Success;
        }
    }
}
