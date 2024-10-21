using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviourTree
{
    [Serializable]
    public class Blackboard
    {
        public Enemy thisUnit;
        public Transform target;
        [HideInInspector] public PlayableDirector playableDirector;
        
        public enum State { Idle, Aggro, Death }

        public State state;
    }
}
