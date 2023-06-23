using GameFramework.Fsm;
using GameMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FsmDead : FsmState<EnemyController>
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    protected override void OnInit(IFsm<EnemyController> fsm)
    {
        animator = fsm.Owner.GetComponent<Animator>();
        navMeshAgent = fsm.Owner.GetComponent<NavMeshAgent>();
    }
    protected override void OnEnter(IFsm<EnemyController> fsm)
    {
    }
    protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
    {
    }
    protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
