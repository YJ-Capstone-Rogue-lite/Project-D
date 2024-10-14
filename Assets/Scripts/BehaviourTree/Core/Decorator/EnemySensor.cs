using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace BehaviourTree
{
    public class EnemySensor : DecoratorNode
    {
        
        protected override void OnEnd()
        {

        }

        protected override void OnStart()
        {

        }

        protected override NodeState OnUpdate()
        {
            var hit = Physics2D.CircleCast(blackboard.thisUnit.transform.position, blackboard.thisUnit.attackRange+1, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Player"));
            if (hit)
            {
                return child.Update();
            }
            else return NodeState.Failure;

        }
    }
}
