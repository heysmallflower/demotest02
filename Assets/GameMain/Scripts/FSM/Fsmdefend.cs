using GameFramework.Fsm;
using GameMain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fsmdefend : FsmState<ItemController>
{
    NavMeshAgent agent;
    Animator animator;
    int attTime;
    int addSpeed;
    protected override void OnInit(IFsm<ItemController> fsm)
    {
        base.OnInit(fsm);
        attTime = 0;
        addSpeed = 0;
        agent = fsm.Owner.GetComponent<NavMeshAgent>();
        animator = fsm.Owner.GetComponent<Animator>();
    }
    protected override void OnEnter(IFsm<ItemController> fsm)
    {
        base.OnEnter(fsm);
        agent.enabled = false;
        animator.SetBool("IsDefend", true);
    }
    protected override void OnUpdate(IFsm<ItemController> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (fsm.Owner.attacker.GetComponent<ItemController>().isDead || Vector3.Distance(fsm.Owner.transform.position, fsm.Owner.attacker.transform.position) > agent.stoppingDistance + 2)
        {
            ChangeState<FsmDogIdel>(fsm);
        }
        else
        {
            if (fsm.Owner.attacker != null)
            {
                if (fsm.Owner.IsAttack)
                {
                    fsm.Owner.transform.LookAt(fsm.Owner.attacker.transform.position);
                    animator.SetTrigger("AttackDefend");
                    attTime++;
                    fsm.Owner.attacker.GetComponent<ItemController>().Hit(fsm.Owner.Date);
                    new WaitForSeconds(2.5f);
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
                if (fsm.Owner.attacker.GetComponent<ItemController>().isDead)
                {
                    fsm.Owner.FindAttackTarget();
                    agent.enabled = true;
                    ChangeState<FsmDogIdel>(fsm);
                }
            }
        }
    }
    protected override void OnLeave(IFsm<ItemController> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("IsDefend", false);
    }
}
