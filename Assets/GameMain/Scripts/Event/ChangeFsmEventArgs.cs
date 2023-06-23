using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameMain
{
    public sealed class ChangeFsmEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ChangeFsmEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public string fsmName
        {
            get;
            private set;
        }
        public Animator animator
        {
            get;
            private set;
        }
        public NavMeshAgent agent
        {
            get;
            private set;
        }
        public static ChangeFsmEventArgs Create(string fsmName, Animator animation, NavMeshAgent agent)
        {
            ChangeFsmEventArgs e = ReferencePool.Acquire<ChangeFsmEventArgs>();
            e.fsmName = fsmName;
            e.animator = animation;
            e.agent = agent;
            return e;
        }
        public override void Clear()
        {

        }
        public sealed class ChangeFsmWalkEventArgs : GameEventArgs
        {
            public static readonly int EventId = typeof(ChangeFsmWalkEventArgs).GetHashCode();

            public override int Id
            {
                get
                {
                    return EventId;
                }
            }
            public string fsmName
            {
                get;
                private set;
            }
            public Animator animator
            {
                get;
                private set;
            }
            public NavMeshAgent agent
            {
                get;
                private set;
            }
            public static ChangeFsmWalkEventArgs Create(string fsmName, Animator animation, NavMeshAgent agent)
            {
                ChangeFsmWalkEventArgs e = ReferencePool.Acquire<ChangeFsmWalkEventArgs>();
                e.fsmName = fsmName;
                e.animator = animation;
                e.agent = agent;
                return e;
            }
            public override void Clear()
            {

            }
        }
        public sealed class ChangeFsmRunEventArgs : GameEventArgs
        {
            public static readonly int EventId = typeof(ChangeFsmRunEventArgs).GetHashCode();

            public override int Id
            {
                get
                {
                    return EventId;
                }
            }
            public string fsmName
            {
                get;
                private set;
            }
            public Animator animator
            {
                get;
                private set;
            }
            public NavMeshAgent agent
            {
                get;
                private set;
            }
            public static ChangeFsmRunEventArgs Create(string fsmName, Animator animation, NavMeshAgent agent)
            {
                ChangeFsmRunEventArgs e = ReferencePool.Acquire<ChangeFsmRunEventArgs>();
                e.fsmName = fsmName;
                e.animator = animation;
                e.agent = agent;
                return e;
            }
            public override void Clear()
            {

            }
        }
        public sealed class ChangeFsmAttackEventArgs : GameEventArgs
        {
            public static readonly int EventId = typeof(ChangeFsmAttackEventArgs).GetHashCode();

            public override int Id
            {
                get
                {
                    return EventId;
                }
            }
            public string fsmName
            {
                get;
                private set;
            }
            public Animator animator
            {
                get;
                private set;
            }
            public NavMeshAgent agent
            {
                get;
                private set;
            }
            public static ChangeFsmAttackEventArgs Create(string fsmName, Animator animation, NavMeshAgent agent)
            {
                ChangeFsmAttackEventArgs e = ReferencePool.Acquire<ChangeFsmAttackEventArgs>();
                e.fsmName = fsmName;
                e.animator = animation;
                e.agent = agent;
                return e;
            }
            public override void Clear()
            {

            }
        }
        public sealed class ChangeFsmPatralEventArgs : GameEventArgs
        {
            public static readonly int EventId = typeof(ChangeFsmPatralEventArgs).GetHashCode();

            public override int Id
            {
                get
                {
                    return EventId;
                }
            }
            public string fsmName
            {
                get;
                private set;
            }
            public Animator animator
            {
                get;
                private set;
            }
            public NavMeshAgent agent
            {
                get;
                private set;
            }
            public static ChangeFsmPatralEventArgs Create(string fsmName, Animator animation, NavMeshAgent agent)
            {
                ChangeFsmPatralEventArgs e = ReferencePool.Acquire<ChangeFsmPatralEventArgs>();
                e.fsmName = fsmName;
                e.animator = animation;
                e.agent = agent;
                return e;
            }
            public override void Clear()
            {

            }
        }
    }
}
