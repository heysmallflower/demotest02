using GameFramework.Fsm;
using GameMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FsmDogAttack : FsmState<ItemController>
{
    NavMeshAgent agent;
    Animator animator;
    float attackTime = 30f;
    float time;
    int attTime;
    int addSpeed;
    protected override void OnInit(IFsm<ItemController> fsm)
    {
        base.OnInit(fsm);
        addSpeed = 0;
        agent = fsm.Owner.GetComponent<NavMeshAgent>();
        animator = fsm.Owner.GetComponent<Animator>();
    }
    protected override void OnEnter(IFsm<ItemController> fsm)
    {
        attTime = 0;

        attackTime = 30;
        agent.isStopped = true;
        agent.enabled = false;
        base.OnEnter(fsm);
    }
    protected override void OnUpdate(IFsm<ItemController> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (fsm.Owner.IsAttack)
        {
            fsm.Owner.transform.LookAt(fsm.Owner.attackTarget.transform.position);
            animator.SetTrigger("Attack");
            attTime++;
            addSpeed++;
            fsm.Owner.attackTarget.GetComponent<ItemController>().Hit(fsm.Owner.Date);
            fsm.Owner.IsAttack = false;
            if (attTime >= 5 && !fsm.Owner.AddAttack)
            {
                fsm.Owner.AddAttack = true;
                attTime = 0;
            }
            if (addSpeed < 5)
            {
                fsm.Owner.AddSpeed = true;
            }
            else
            {
                fsm.Owner.AddSpeed = false;
            }
        }
        attackTime -= Time.deltaTime;
        if (attackTime <= 0 || fsm.Owner.attackTarget.GetComponent<ItemController>().isDead)
        {
            fsm.Owner.IsReturnPos = true;
            fsm.Owner.InitSpeedAttack();
            fsm.Owner.FindAttackTarget();
            ChangeState<FsmDogRun>(fsm);
        }
    }
    protected override void OnLeave(IFsm<ItemController> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
