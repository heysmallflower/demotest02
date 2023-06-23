using GameFramework.Fsm;
using GameMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FsmDogIdel : FsmState<ItemController>
{
    NavMeshAgent meshAgent;
    protected override void OnInit(IFsm<ItemController> fsm)
    {
        meshAgent = fsm.Owner.gameObject.GetComponent<NavMeshAgent>();
        base.OnInit(fsm);
    }
    protected override void OnEnter(IFsm<ItemController> fsm)
    {
        base.OnEnter(fsm);
    }
    protected override void OnUpdate(IFsm<ItemController> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (fsm.Owner.attackTarget != null || fsm.Owner.UpdatePos)
        {
            ChangeState<FsmDogRun>(fsm);
        }
        IsByAttack(fsm);
    }
    protected override void OnLeave(IFsm<ItemController> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
    void IsByAttack(IFsm<ItemController> fsm)
    {
        if (fsm.Owner.attacker != null)
        {
            if (Vector3.Distance(fsm.Owner.transform.position, fsm.Owner.attacker.transform.position) <= meshAgent.stoppingDistance + 1)
            {
                ChangeState<Fsmdefend>(fsm);
            }
        }
    }
}
