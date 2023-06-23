using GameFramework.Fsm;
using GameMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class FsmPatral : FsmState<EnemyController>
{
    IFsm<EnemyController> Patralfsm;
    Animator animator;
    NavMeshAgent navMeshAgent;
    Vector3 pos;
    //巡逻范围
    private float patrolRange = 50;
    Vector3 guardPos;
    //随机移动的位置
    Vector3 wayPonit;

    protected override void OnEnter(IFsm<EnemyController> fsm)
    {
        base.OnEnter(fsm);
        Patralfsm = fsm;
        animator = fsm.Owner.GetComponent<Animator>();
        navMeshAgent = fsm.Owner.GetComponent<NavMeshAgent>();
        pos = fsm.Owner.gameObject.transform.position;
        guardPos = pos;
        animator.SetBool("IsWalk", true);
        Debug.Log("进入巡逻状态");
    }
    protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
    {
        navMeshAgent.destination = wayPonit;
        GetNewWayPoint();
        if (fsm.Owner.FindAttackTarget() && Vector3.Distance(fsm.Owner.attackTarget.transform.position, fsm.Owner.gameObject.transform.position) > 5)
        {
            ChangeState<FsmChase>(fsm);
        }
        else if (fsm.Owner.FindAttackTarget() && Vector3.Distance(fsm.Owner.attackTarget.transform.position, fsm.Owner.gameObject.transform.position) < 5)
        {
            ChangeState<FsmAttack>(fsm);
        }
        else if (fsm.Owner.player != null && fsm.Owner.playMoveSpeed > 0)
        {
            ChangeState<FsmFllowPlayer>(fsm);
        }

    }
    protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("IsWalk", false);
    }
    void GetNewWayPoint()
    {
        //随机生成巡逻范围内的x点位
        float randomx = Random.Range(-patrolRange, patrolRange);
        //随机生成巡逻范围内的z点位
        float randomz = Random.Range(-patrolRange, patrolRange);
        //生成巡逻范围内新的随机坐标
        Vector3 randomPonit = new Vector3(guardPos.x + randomx, Patralfsm.Owner.gameObject.transform.position.y, guardPos.z + randomz);
        NavMeshHit hit;
        //返回视野范围半径内的定义的随机点并且返回给hit，且返回true，如果不是这个网格导航的图层，那么返回false
        if (NavMesh.SamplePosition(randomPonit, out hit, patrolRange, 1))
        {
            //将随机坐标赋值给随机点位变量
            wayPonit = hit.position;
        }
        //如果是不能行走的区域赋予当前位置坐标
        else
        {
            wayPonit = Patralfsm.Owner.gameObject.transform.position;
        }
    }
}
