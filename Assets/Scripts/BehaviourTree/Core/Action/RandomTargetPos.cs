using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class RandomTargetPos : ActionNode
    {
        public float range;
        private int count;
        private Transform transform;

        protected override void OnEnd() { }

        protected override void OnStart() => count = 0;

        protected override NodeState OnUpdate()
        {
            Vector3 thisPos;
            do{
                thisPos = blackboard.thisUnit.transform.position;
                transform.position = ChooseNewEndPoint();
                if(count++ > 10) return NodeState.Failure;
            }while(Physics2D.Raycast(thisPos, transform.position, (thisPos-transform.position).magnitude, 1<<LayerMask.NameToLayer("Wall")));
            blackboard.target = transform;
            return NodeState.Success;
        }

        public Vector3 ChooseNewEndPoint()
        {
            float currentAngle = Random.Range(0, 360);
            currentAngle = Mathf.Repeat(currentAngle, 360);
            return blackboard.thisUnit.transform.position + Vector3FromAngle(currentAngle) * range;
        }
        Vector3 Vector3FromAngle(float inputAngle)
        {
            float inputAngleRadians = inputAngle * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
        }
    }
}
