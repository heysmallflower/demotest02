using GameFramework.Fsm;
using GameMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FsmFllowPlayer : FsmState<EnemyController>
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    float speed;
    Transform playerPos;
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
        Debug.Log("½øÈë¸úËæ×´Ì¬");
    }
    protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        FllowPlay(fsm);
    }
    protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("IsChase", false);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsWalk", false);
    }
    void FllowPlay(IFsm<EnemyController> fsm)
    {
        if (fsm.Owner.playMoveSpeed >= 2 && fsm.Owner.playMoveSpeed < 6)
        {
            animator.SetBool("IsWalk", true);
            playerPos = fsm.Owner.player.transform;
            fsm.Owner.gameObject.transform.LookAt(fsm.Owner.player.transform.position);
            navMeshAgent.speed = speed;
            navMeshAgent.destination = fsm.Owner.player.transform.position;
        }
        else if (fsm.Owner.playMoveSpeed >= 6)
        {
            animator.SetBool("IsChase", true);
            playerPos = fsm.Owner.player.transform;
            fsm.Owner.gameObject.transform.LookAt(fsm.Owner.player.transform.position);
            navMeshAgent.speed = speed + 1;
            navMeshAgent.destination = fsm.Owner.player.transform.position;
        }
        else if (fsm.Owner.playMoveSpeed <= 0)
        {
            navMeshAgent.speed = speed;
            navMeshAgent.enabled = false;
            ChangeState<FsmIdle>(fsm);
        }
        else if(fsm.Owner.FindAttackTarget() && fsm.Owner.IsAttackRange())
        {
            navMeshAgent.speed = speed;
            ChangeState<FsmAttack>(fsm);
        }
    }
}
