using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class RandomTargetPos : ActionNode
    {
        public float range;
        private int count;

        private Transform tempTransform;

        void OnEnable() => tempTransform = new GameObject("temp").transform;

        protected override void OnEnd() { }

        protected override void OnStart() => count = 0;

        protected override NodeState OnUpdate()
        {
            Vector3 thisPos;
            thisPos = ChooseNewEndPoint();
            // do{
            //     thisPos = ChooseNewEndPoint();
            //     if(count++ > 10) return NodeState.Failure;
            // }while(Physics2D.Raycast(thisPos, blackboard.thisUnit.transform.position, (thisPos-blackboard.thisUnit.transform.position).magnitude, 1<<LayerMask.NameToLayer("Wall")));
            tempTransform.position = thisPos;
            blackboard.target = tempTransform;
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
