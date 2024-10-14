using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Move : ActionNode
    {
        protected override void OnEnd()
        {
            blackboard.thisUnit.enemy_animator.SetFloat("MoveX", 0);
            blackboard.thisUnit.enemy_animator.SetFloat("MoveY", 0);
            blackboard.thisUnit.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        protected override void OnStart()
        {
            blackboard.thisUnit.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        protected override NodeState OnUpdate()
        {
            var tempPos = blackboard.thisUnit.transform.position - blackboard.target.position;
            if(tempPos.magnitude < blackboard.thisUnit.attackRange) return NodeState.Success;
            var norm = tempPos.normalized;
            blackboard.thisUnit.enemy_animator.SetFloat("MoveX", -norm.x);
            blackboard.thisUnit.enemy_animator.SetFloat("MoveY", -norm.y);
            if(Mathf.Sign(norm.x) < 0) blackboard.thisUnit.spriteRenderer.flipX = true;
            else blackboard.thisUnit.spriteRenderer.flipX = false;
            // blackboard.thisUnit.GetComponent<Rigidbody2D>().velocity = -norm * blackboard.thisUnit.enemy_speed * Time.deltaTime;
            blackboard.thisUnit.transform.position = Vector3.MoveTowards(blackboard.thisUnit.transform.position, blackboard.target.position, blackboard.thisUnit.enemy_speed * Time.deltaTime);
            return NodeState.Running;
        }
    }
}
