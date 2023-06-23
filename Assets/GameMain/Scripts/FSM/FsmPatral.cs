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
    //Ѳ�߷�Χ
    private float patrolRange = 50;
    Vector3 guardPos;
    //����ƶ���λ��
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
        Debug.Log("����Ѳ��״̬");
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
        //�������Ѳ�߷�Χ�ڵ�x��λ
        float randomx = Random.Range(-patrolRange, patrolRange);
        //�������Ѳ�߷�Χ�ڵ�z��λ
        float randomz = Random.Range(-patrolRange, patrolRange);
        //����Ѳ�߷�Χ���µ��������
        Vector3 randomPonit = new Vector3(guardPos.x + randomx, Patralfsm.Owner.gameObject.transform.position.y, guardPos.z + randomz);
        NavMeshHit hit;
        //������Ұ��Χ�뾶�ڵĶ��������㲢�ҷ��ظ�hit���ҷ���true���������������񵼺���ͼ�㣬��ô����false
        if (NavMesh.SamplePosition(randomPonit, out hit, patrolRange, 1))
        {
            //��������긳ֵ�������λ����
            wayPonit = hit.position;
        }
        //����ǲ������ߵ������赱ǰλ������
        else
        {
            wayPonit = Patralfsm.Owner.gameObject.transform.position;
        }
    }
}
