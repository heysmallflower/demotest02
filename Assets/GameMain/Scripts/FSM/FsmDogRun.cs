using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FsmDogRun : FsmState<ItemController>
{
    NavMeshAgent agent;
    Animator animator;
    Vector3 targrtPos;
    protected override void OnInit(IFsm<ItemController> fsm)
    {
        base.OnInit(fsm);
        agent = fsm.Owner.GetComponent<NavMeshAgent>();
        animator = fsm.Owner.GetComponent<Animator>();
    }
    protected override void OnEnter(IFsm<ItemController> fsm)
    {
        agent.enabled = true;
        base.OnEnter(fsm);
        if (fsm.Owner.UpdatePos)
        {
            targrtPos = fsm.Owner.pos;
            agent.destination = targrtPos;
            animator.SetBool("IsWalk", true);
            return;
        }
        if (fsm.Owner.IsReturnPos)
        {
            fsm.Owner.transform.LookAt(fsm.Owner.pos);
            if (fsm.Owner.teamName == GameMain.Team.A)
            {
                agent.destination = fsm.Owner.pos + new Vector3(0, 0, 2);
            }
            else
            {
                agent.destination = fsm.Owner.pos + new Vector3(0, 0, -2);
            }
            animator.SetBool("IsWalk", true);
            return;
        }
        RunAttackPosAt(fsm);
    }
    protected override void OnUpdate(IFsm<ItemController> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (fsm.Owner.UpdatePos && Mathf.Abs(agent.remainingDistance) <= 1)
        {
            animator.SetBool("IsWalk", false);
            fsm.Owner.UpdatePos = false;
            ChangeState<FsmDogIdel>(fsm);
            return;
        }
        if (fsm.Owner.IsReturnPos && Mathf.Abs(agent.remainingDistance) <= 1)
        {
            animator.SetBool("IsWalk", false);
            fsm.Owner.IsReturnPos = false;
            fsm.Owner.AttackEnd = true;
            new WaitForSeconds(3f);
            //TODO:回到初始位置旋转
            //fsm.Owner.transform.localRotation =  Quaternion.AngleAxis(100, Vector3.right);
            ChangeState<FsmDogIdel>(fsm);
            return;
        }
        if (!fsm.Owner.UpdatePos  && !fsm.Owner.IsReturnPos && Vector3.Distance(fsm.Owner.attackTarget.transform.position, fsm.Owner.transform.position) <= agent.stoppingDistance)
        {
            ChangeState<FsmDogAttack>(fsm);
        }
    }
    protected override void OnLeave(IFsm<ItemController> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("IsWalk", false);
    }
    void RunAttackPosAt(IFsm<ItemController> fsm)
    {
        if (fsm.Owner.attackTarget != null)
        {
            animator.SetBool("IsWalk",true);
            agent.destination = fsm.Owner.attackTarget.transform.position;
        }
    }
}
