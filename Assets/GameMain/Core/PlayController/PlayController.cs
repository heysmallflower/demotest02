using GameMain;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using static UnityEditor.Progress;
using static UnityEngine.ParticleSystem;

namespace GameMain
{
    public class PlayController : GameFrameworkComponent
    {
        //要攻击的目标
        private List<Transform> targets;
        //扇形距离 攻击距离 扇形的半径 
        private float SkillDistance = 5;
        //扇形的角度 也就是攻击的角度
        private float SkillJiaodu = 60;
        private SphereCollider collider;
        Animator playAnimCon;
        PlayerInput playInput;
        float footAttackTime;
        DuQuanManager duQuanManager;
        public float CurrentHealth
        {
            get;
            private set;
        }
        public float MaxHealth
        {
            get;
            private set;
        }
        public TeamIndex teamIndex
        {
            get;
            private set;
        }
        public bool IsDead
        {
            get;
            private set;
        }
        protected override void Awake()
        {
            base.Awake();
            playAnimCon = GetComponent<Animator>();
            playInput = GetComponent<PlayerInput>();
            collider = GetComponent<SphereCollider>();
            targets = new List<Transform>();
            footAttackTime = 0;
            teamIndex = TeamIndex.A;
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
        }
        private void Start()
        {

        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("攻击");
                playInput.enabled = false;
                playAnimCon.SetTrigger("Attack");
                Attack();
            }
            else if (Input.GetMouseButtonUp(1) && footAttackTime <= 0)
            {
                footAttackTime = 10f;
                playInput.enabled = false;
                playAnimCon.SetTrigger("AttactFoot");
                Attack(true);
            }
            DuQuanHit();
        }
        private void FixedUpdate()
        {
            footAttackTime -= Time.deltaTime;
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Enemy") && !targets.Contains(other.gameObject.transform))
            {
                targets.Add(other.gameObject.transform);
            }
        }
        void EndAttack()
        {
            playInput.enabled = true;
        }
        void Attack(bool isBean = false)
        {
            if (targets.Count > 0)
            {
                foreach (var item in targets)
                {
                    //与敌人的距离
                    float distance = Vector3.Distance(transform.position, item.position);
                    //玩家正前方的向量
                    Vector3 norVec = transform.rotation * Vector3.forward;
                    //玩家与敌人的方向向量
                    Vector3 temVec = item.position - transform.position;
                    //求两个向量的夹角
                    float jiajiao = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
                    if (distance < SkillDistance)
                    {
                        if (jiajiao <= SkillJiaodu * 0.5f)
                        {
                            if (item.gameObject.GetComponent<EnemyController>().teamIndex != TeamIndex.A && !item.gameObject.GetComponent<EnemyController>().IsDead)
                                item.gameObject.GetComponent<EnemyController>().Hit(this);
                        }
                    }
                }
            }
            targets.Clear();
        }
        EnemyController attacker;
        public void Hit(EnemyController attacker, bool isBean = false)
        {
            if (isBean && !IsDead)
            {
                playAnimCon.SetTrigger("Fali");
                CurrentHealth -= 2;
                this.attacker = attacker;
                Dead();
            }
            else if (!isBean && !IsDead)
            {
                playAnimCon.SetTrigger("FeatHit");
                CurrentHealth -= 5;
                this.attacker = attacker;
                Dead();
            }
        }
        void DuQuanHit()
        {
            float distance = Vector3.Distance(transform.position, GameEntry.DuQuanManager.gameObject.transform.position);
            if (distance > GameEntry.DuQuanManager.Length && !IsDead)
            {
                CurrentHealth -= 2;
                Dead();
            }
        }
        public void Add(int value)
        {
            CurrentHealth += value;
        }
        void Dead()
        {
            if (CurrentHealth <= 0)
            {
                IsDead = true;
                if (attacker != null)
                {
                    TeamIndex teamIndex = attacker.teamIndex;
                    GameEntry.TeamManager.AddF(teamIndex, 10);
                    GameEntry.TeamManager.ShowGameEnd();
                }
                playAnimCon.SetTrigger("IsDead");
            }
        }
    }
}
