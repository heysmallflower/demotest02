using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

namespace GameMain
{
    public class FsmAttack : FsmState<EnemyController>
    {
        //���ξ��� �������� ���εİ뾶 
        private float SkillDistance = 3;
        //���εĽǶ� Ҳ���ǹ����ĽǶ�
        private float SkillJiaodu = 60;
        Animator animator;
        NavMeshAgent navMeshAgent;
        AnimatorStateInfo animatorStateInfo;
        protected override void OnInit(IFsm<EnemyController> fsm)
        {
            animator = fsm.Owner.GetComponent<Animator>();
            navMeshAgent = fsm.Owner.GetComponent<NavMeshAgent>();
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(1);
        }
        protected override void OnEnter(IFsm<EnemyController> fsm)
        {
            base.OnEnter(fsm);
            navMeshAgent.enabled = false;
            Debug.Log("���빥��״̬");
        }
        protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (fsm.Owner.IsAttackRange() && fsm.Owner.attackTime <= 0 && fsm.Owner.FindAttackTarget())
            {
                animator.SetBool("AttacState", false);
                fsm.Owner.transform.LookAt(fsm.Owner.attackTarget.transform.position);
                animator.SetTrigger("Attack");
                //����˵ľ���
                float distance = Vector3.Distance(fsm.Owner.transform.position, fsm.Owner.attackTarget.transform.position);
                //�����ǰ��������
                Vector3 norVec = fsm.Owner.transform.rotation * Vector3.forward;
                //�������˵ķ�������
                Vector3 temVec = fsm.Owner.attackTarget.transform.position - fsm.Owner.transform.position;
                //�����������ļн�
                float jiajiao = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
                if (distance < SkillDistance)
                {
                    if (jiajiao <= SkillJiaodu * 0.5f)
                    {
                        if (fsm.Owner.tag != "Player" && !fsm.Owner.attackTarget.GetComponent<EnemyController>().IsDead)
                        {
                            fsm.Owner.attackTarget.GetComponent<EnemyController>().Hit(null,fsm.Owner);
                        }
                        else
                        {
                            fsm.Owner.attackTarget.GetComponent<PlayController>().Hit(fsm.Owner);
                        }
                    }
                }
                fsm.Owner.InitAttackTime();
                //new WaitForSeconds(2f);
            }
            else
            {
                animator.SetBool("AttacState", true);
            }

            if (!fsm.Owner.IsAttackRange())
            {
                animator.SetBool("AttacState", false);
                navMeshAgent.enabled = true;
                ChangeState<FsmChase>(fsm);
            }
        }
        protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);

        }
    }
}
