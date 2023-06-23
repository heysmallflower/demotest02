using GameFramework.Fsm;
using StarterAssets;
using Suntail;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;
using static UnityEditor.VersionControl.Asset;

namespace GameMain
{
    public class EnemyController : MonoBehaviour
    {
        FsmState<EnemyController>[] states = { new FsmIdle(), new FsmWalk(), new FsmRun(), new FsmAttack(), new FsmPatral(), new FsmChase(), new FsmFllowPlayer() };
        List<string> stateName = new List<string>() { "IsWalk", "IsRun", "IsChase", "AttacState" };
        IFsm<EnemyController> fsmManager;
        Animator animator;
        string name;
        public EnemyDate temp;
        NavMeshAgent agent;
        public bool IsDead
        {
            get;
            private set;
        }
        public EnemyDate date
        {
            get;
            private set;
        }
        public GameObject player
        {
            get;
            private set;
        }
        public float playMoveSpeed
        {
            get;
            private set;
        }
        public bool AttackZhilING
        {
            get;
            private set;
        }
        public string tag
        {
            get;
            private set;
        }
        [SerializeField]
        float attackRange;
        public bool IsSuoDing
        {
            get;
            private set;
        }
        public float attackTime
        {
            get;
            private set;
        }
        public float time
        {
            get;
            private set;
        }
        public GameObject attackTarget
        {
            get;
            private set;
        }
        public TeamIndex teamIndex;
        //{
        //    get;
        //    private set;
        //}
        public float sightRadius;

        private void Awake()
        {
            sightRadius = 20;
            attackTime = 3f;
            time = attackTime;
            IsSuoDing = false;
            IsDead = false;
            tag = null;
            AttackZhilING = false;
            agent = GetComponent<NavMeshAgent>();
            if (temp != null)
            {
                date = Instantiate(temp);
            }
            InitTeamIndex();
            InitEnemyFsm();
        }

        private void InitEnemyFsm()
        {
            name = this.GetHashCode().ToString();
            animator = GetComponent<Animator>();
            fsmManager = GameEntry.Fsm.CreateFsm<EnemyController>(name, this, states);
        }
        private void InitTeamIndex()
        {
            if (GameEntry.TeamManager.teamA.Count < 2)
            {
                GameEntry.TeamManager.teamA.Add(this);
                teamIndex = TeamIndex.A;
            }
            else if (GameEntry.TeamManager.teamB.Count < 3)
            {
                GameEntry.TeamManager.teamB.Add(this);
                teamIndex = TeamIndex.B;
            }
            else if (GameEntry.TeamManager.teamC.Count < 3)
            {
                GameEntry.TeamManager.teamC.Add(this);
                teamIndex = TeamIndex.C;
            }
            else if (GameEntry.TeamManager.teamD.Count < 3)
            {
                GameEntry.TeamManager.teamD.Add(this);
                teamIndex = TeamIndex.D;
            }
            else if (GameEntry.TeamManager.teamE.Count < 3)
            {
                GameEntry.TeamManager.teamE.Add(this);
                teamIndex = TeamIndex.E;
            }
        }
        private void OnEnable()
        {
            fsmManager.Start<FsmIdle>();
        }
        private void Update()
        {
            if (player != null)
            {
                playMoveSpeed = player.GetComponent<Animator>().GetFloat("Speed");
            }
            DuQuanHit();
        }
        private void FixedUpdate()
        {
            attackTime -= Time.deltaTime;
        }
        public bool FindAttackTarget()
        {
            //查找当前物体中心点位置的半径所有碰撞体
            var coliders = Physics.OverlapSphere(transform.position, sightRadius);
            foreach (var target in coliders)
            {
                if (target.CompareTag("Player") && teamIndex == TeamIndex.A)
                {
                    player = target.gameObject;
                }
                //如果找到标签为Player，返回true方法结束
                if (target.CompareTag("Player") && teamIndex != TeamIndex.A)
                {
                    //将玩家信息存储到攻击目标里
                    attackTarget = target.gameObject;
                    tag = "Player";
                    //返回true
                    return true;
                }
                else if (target.CompareTag("Enemy") && target.GetComponent<EnemyController>().teamIndex != teamIndex && !target.GetComponent<EnemyController>().IsDead)
                {
                    //将玩家信息存储到攻击目标里
                    attackTarget = target.gameObject;
                    tag = "Enemy";
                    //返回true
                    return true;
                }
            }

            //没有找到玩家
            attackTarget = null;
            tag = null;
            //返回false
            return false;
        }
        public bool IsAttackRange()
        {
            if (attackTarget != null)
            {
                if (Vector3.Distance(attackTarget.transform.position, transform.position) <= attackRange)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public void InitAttackTime()
        {
            attackTime = time;
        }
        PlayController playerIsAttacker;
        EnemyController attacker;
        public void Hit(PlayController player = null, EnemyController attacker = null, bool isBeanHit = false)
        {
            if (isBeanHit)
            {
                animator.SetTrigger("BeanHit");
                date.currentHealth -= 5;
                playerIsAttacker = player;
                this.attacker = attacker;
                Dead();
                Debug.Log("敌人当前血量" + date.currentHealth);
            }
            else
            {
                animator.SetTrigger("FeatHit");
                date.currentHealth -= 5;
                playerIsAttacker = player;
                this.attacker = attacker;
                Dead();
                Debug.Log("敌人当前血量" + date.currentHealth);
            }
        }
        public void Dead()
        {
            if (date.currentHealth <= 0)
            {
                if (attacker != null)
                {
                    GameEntry.TeamManager.AddF(attacker.teamIndex, 5);
                }
                if (playerIsAttacker != null)
                {
                    GameEntry.TeamManager.AddF(playerIsAttacker.teamIndex, 5);
                }
                IsDead = true;
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<CharacterController>().enabled = false;
                agent.radius = 0;
                GameEntry.Fsm.DestroyFsm<EnemyController>(name);
                animator.SetTrigger("IsDead");
            }
        }
        void DuQuanHit()
        {
            float distance = Vector3.Distance(transform.position, GameEntry.DuQuanManager.gameObject.transform.position);
            if (distance > GameEntry.DuQuanManager.Length && !IsDead)
            {
                date.currentHealth -= 2;
                Dead();
            }
        }
    }
}