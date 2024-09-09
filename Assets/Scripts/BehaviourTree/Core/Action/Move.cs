using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Move : ActionNode
    {
        protected override void OnEnd() { }

        protected override void OnStart() { }

        protected override NodeState OnUpdate()
        {
            var tempPos = blackboard.thisUnit.transform.position - blackboard.target.position;
            if(tempPos.magnitude < 1) return NodeState.Failure;
            var norm = tempPos.normalized;
            // blackboard.thisUnit.enemy_animator.SetFloat("MoveX", norm.x);
            // blackboard.thisUnit.enemy_animator.SetFloat("MoveY", norm.y);
            // if(Mathf.Sign(norm.x) > 0) blackboard.thisUnit.spriteRenderer.flipX = true;
            // else blackboard.thisUnit.spriteRenderer.flipX = false;
            blackboard.thisUnit.transform.position = Vector3.MoveTowards(blackboard.thisUnit.transform.position, blackboard.target.position, blackboard.thisUnit.enemy_speed * Time.deltaTime);
            return NodeState.Success;
        }
    }
}
