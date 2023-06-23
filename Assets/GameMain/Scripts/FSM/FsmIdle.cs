using GameFramework.Event;
using GameFramework.Fsm;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameMain
{
    public class FsmIdle : FsmState<EnemyController>
    {
        Animator animator;
        NavMeshAgent navMeshAgent;
        float time;
        protected override void OnEnter(IFsm<EnemyController> fsm)
        {
            base.OnEnter(fsm);
            animator = fsm.Owner.GetComponent<Animator>();
            navMeshAgent = fsm.Owner.GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = true;
            animator.SetBool("IsIdel", true);
            time = 3f;
        }
        protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            time -= Time.deltaTime;
            if (fsm.Owner.FindAttackTarget() && fsm.Owner.IsAttackRange())
            {
                navMeshAgent.isStopped = false;
                ChangeState<FsmAttack>(fsm);
            }
            else if (fsm.Owner.FindAttackTarget() && !fsm.Owner.IsAttackRange())
            {
                navMeshAgent.isStopped = false;
                ChangeState<FsmChase>(fsm);
            }
            else if (time <= 0)
            {
                navMeshAgent.isStopped = false;
                ChangeState<FsmPatral>(fsm);
            }
            else if (fsm.Owner.player != null && fsm.Owner.playMoveSpeed > 0)
            {
                navMeshAgent.isStopped = false;
                ChangeState<FsmFllowPlayer>(fsm);
            }
        }
        protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            animator.SetBool("IsIdel", false);
        }
    }
}
