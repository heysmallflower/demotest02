using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameMain
{
    public class FsmRun : FsmState<EnemyController>
    {
        Animator animator;
        NavMeshAgent navMeshAgent;
        float speed;
        protected override void OnEnter(IFsm<EnemyController> fsm)
        {
            base.OnEnter(fsm);
            animator = fsm.Owner.GetComponent<Animator>();
            navMeshAgent = fsm.Owner.GetComponent<NavMeshAgent>();
            speed = navMeshAgent.speed;
            
            Debug.Log("½øÈëÅÜ¶¯×´Ì¬");
        }
        protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (!animator.GetBool("IsRun"))
            {
                animator.SetBool("IsRun", true);
            }
        }
        protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsIdel", true);
        }

    }
}
