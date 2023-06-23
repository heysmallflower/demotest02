using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameMain
{
    public class FsmChase : FsmState<EnemyController>
    {
        Animator animator;
        NavMeshAgent navMeshAgent;
        float speed;
        protected override void OnInit(IFsm<EnemyController> fsm)
        {
            base.OnInit(fsm);
            animator = fsm.Owner.GetComponent<Animator>();
            navMeshAgent = fsm.Owner.GetComponent<NavMeshAgent>();
        }
        protected override void OnEnter(IFsm<EnemyController> fsm)
        {
            base.OnEnter(fsm);
            speed = navMeshAgent.speed;
            animator.SetBool("IsChase", true);
            Debug.Log("½øÈë×·»÷×´Ì¬");
        }
        protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            ChasePlay(fsm);
        }
        protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            animator.SetBool("IsChase", false);
        }
        void ChasePlay(IFsm<EnemyController> fsm)
        {
            if (fsm.Owner.FindAttackTarget() && !fsm.Owner.IsAttackRange())
            {
                fsm.Owner.gameObject.transform.LookAt(fsm.Owner.attackTarget.transform.position);
                navMeshAgent.speed = speed + 1;
                navMeshAgent.destination = fsm.Owner.attackTarget.transform.position;
            }
            else if (fsm.Owner.FindAttackTarget() && fsm.Owner.IsAttackRange())
            {
                navMeshAgent.speed = speed;
                navMeshAgent.isStopped = true;
                ChangeState<FsmAttack>(fsm);
            }
            else
            {
                navMeshAgent.speed = speed;
                navMeshAgent.isStopped = true;
                ChangeState<FsmIdle>(fsm);
            }
        }
    }
}
