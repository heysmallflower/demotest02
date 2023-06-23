using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
    public class FsmWalk : FsmState<EnemyController>
    {
        protected override void OnEnter(IFsm<EnemyController> fsm)
        {
            base.OnEnter(fsm);
            Debug.Log("进入行走状态");
        }
        protected override void OnUpdate(IFsm<EnemyController> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }
        protected override void OnLeave(IFsm<EnemyController> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }
    }
}
