using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace BehaviourTree
{
    public class StateCheck : DecoratorNode
    {
        public Blackboard.State targetState;

        protected override void OnEnd() {}

        protected override void OnStart() {}

        protected override NodeState OnUpdate()
        {
            if(targetState == blackboard.state) return child.Update();
            else return NodeState.Failure;
        }
    }
}
