using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class MagicianIdle : ActionNode
    {
        
        protected override void OnEnd() { }

        protected override void OnStart() { }

        protected override NodeState OnUpdate()
        {
            return NodeState.Success;
        }
    }
}
