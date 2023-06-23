using GameFramework.Fsm;
using GameMain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;
using static UnityEditor.PlayerSettings;
using GameEntry = GameMain.GameEntry;

public class ItemController : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;
    public event Action<float, float> UpdateCdBarOnAttack;
    public event Action<float> AddHealthWidth;
    public ItemDate temp;
    public ItemDate Date
    {
        get;
        private set;
    }
    public int attackNum;
    public Team teamName;
    public int index;
    public IFsm<ItemController> fsmManager
    {
        get;
        private set;
    }
    Animator animator;
    NavMeshAgent meshAgent;
    string name;
    public float attackTime
    {
        get;
        private set;
    }
    public float attackCD
    {
        get;
        private set;
    }
    public float curtime
    {
        get;
        private set;
    }
    public bool IsReturnPos
    {
        get;
        set;
    }
    public bool AttackEnd
    {
        get;
        set;
    }
    public Vector3 pos
    {
        get;
        private set;
    }
    public bool UpdatePos;
    public GameObject attackTarget
    {
        get;
        private set;
    }
    public GameObject attacker
    {
        get;
        private set;
    }
    public bool isDead
    {
        get;
        private set;
    }
    public bool IsAttack
    {
        get;
        set;
    }
    public bool AddAttack
    {
        get;
        set;
    }
    public bool AddSpeed
    {
        get;
        set;
    }
    public float atk1Sp = 1f;
    public float atk2Sp = 1f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        if (temp != null)
        {
            Date = Instantiate(temp);
        }
    }
    void Start()
    {
        attackTime = 30f;
        attackCD = 6f;
        curtime = 0;
        IsReturnPos = false;
        UpdatePos = false;
        AttackEnd = false;
        isDead = false;
        AddAttack = false;
        AddSpeed = false;
        pos = transform.position;
        name = this.GetHashCode().ToString();
        fsmManager = GameEntry.Fsm.CreateFsm<ItemController>(name, this, new FsmDogIdel(), new FsmDogAttack(), new FsmDogRun(),new Fsmdefend());
        fsmManager.Start<FsmDogIdel>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack02")
            {
                atk2Sp = clip.length;
            }
            if (clip.name == "Attack01")
            {
                atk1Sp = clip.length;
                break;
            }
        }

    }

    public void SetAnimationSpeed(Animation ani, string name, float speed)
    {
        if (null == ani) return;
        AnimationState state = ani[name];
        if (!state) state.speed = speed;
    }

    void Update()
    {
        if (attackTarget != null)
        {
            attackCD = 6f / (Date.speed + 2);

            //if (atk2Sp > Date.speed) Date.speed = (int)(atk2Sp / Date.speed);
            //animator.speed = Date.speed;
            //IsAttack = true;
        }
        else if(attacker != null)
        {
            attackCD = 6f / Date.speed;
            //if (atk1Sp > Date.speed) Date.speed = (int)(atk1Sp / Date.speed);
            //animator.speed = Date.speed;
            //IsAttack = true;
        }
        if (curtime <= 0 && !IsAttack)
        {
            CdUI(curtime, attackCD);
            curtime = attackCD;
            CdUI(curtime, attackCD);
            IsAttack = true;
        }
        else
        {
            curtime -= Time.deltaTime;
            CdUI(curtime, attackCD);
        }
        if (AddAttack)
        {
            AddHeaAtt();
            attackNum = Date.attackNum;
        }
        //if (AddSpeed)
        //{
        //    AddSpeeds();
        //}

    }
    public int speed;
    private void AddSpeeds()
    {
        Date.speed += 5;
        speed = Date.speed;
    }

    private void FixedUpdate()
    {
    }
    public void FindAttackTarget(GameObject target = null , GameObject attacker = null)
    {
        attackTarget = target;
        this.attacker = attacker;
    }
    public void SwapInfo(ItemController target,Vector3 targetpos)
    {
        //attackTarget = target.attackTarget;
        pos = targetpos;
        UpdatePos = true;
    }
    public void AddHealth(int value)
    {
        if (Date.currentHealth == Date.maxHealth)
        {
            return;
        }
        else
        {
            Date.currentHealth = Mathf.Min(Date.currentHealth + value, Date.maxHealth);
            UpdateHealthBarOnAttack.Invoke(Date.currentHealth, Date.maxHealth);
        }
    }
    public void InitAddHealth(int value)
    {
        Date.currentHealth += value;
        Date.maxHealth += value;
        if (AddHealthWidth != null)
        {
            AddHealthWidth.Invoke(0.05F);
        }
    }
    public void Hit(ItemDate itemDate)
    {
        int damage = Mathf.Max(itemDate.attackNum - Date.currentDefence, 0);
        Date.currentHealth = Mathf.Max(Date.currentHealth - damage, 0);
        if (UpdateHealthBarOnAttack != null)
        {
            UpdateHealthBarOnAttack.Invoke(Date.currentHealth, Date.maxHealth);
        }
        Date.currentHealth = Mathf.Min(Date.currentHealth + (int)(Date.currentHealth * 0.05), Date.maxHealth);
        UpdateHealthBarOnAttack.Invoke(Date.currentHealth, Date.maxHealth);
        Dead();
    }
    public void CdUI(float time, float cd)
    {
        if (UpdateCdBarOnAttack != null)
        {
            UpdateCdBarOnAttack.Invoke(time, cd);
        }
    }
    public void Dead()
    {
        if (Date.currentHealth <= 0)
        {
            isDead = true;
            GameEntry.Fsm.DestroyFsm<ItemController>(name);
            animator.SetTrigger("Dead");
            gameObject.GetComponent<Collider>().enabled = false;
            CdUI(0, 0);
            Invoke("Die", 2f);
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
    public void SetSpeed(int speed)
    {
        Date.speed = speed;
    }
    void AddHeaAtt()
    {
        Date.attackNum += (int)(Date.attackNum * 0.5f);
        AddHealth((int)(Date.currentHealth * 0.3));
        AddAttack = false;
    }
    public void InitSpeedAttack()
    {
        Date.attackNum = 2;
        Date.speed = 2;
    }
}
