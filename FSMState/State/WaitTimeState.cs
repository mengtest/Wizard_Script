﻿//---//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Friday, May 20, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

public class WaitTimeState : FSMState {

    public float Duration {
        get; set;
    }

    private float endTime;
    public WaitTimeState (FSMStateMachine _machine) :
        base(_machine) {

    }

    public override void Enter () {
        base.Enter();
        if (Duration < 0) {
            Status = FSMSateStatus.Failure;
        }
        else {
            Status = FSMSateStatus.Running;
            endTime = Time.time + Duration;
        }

    }

    public override void Excute () {
        base.Excute();
        if (Time.time < endTime) {
            Debug.Log(string.Format("剩余：{0}秒", endTime - Time.time));
        }
        else {
            Status = FSMSateStatus.Success;
        }
    }

    public override void Exit () {
        base.Exit();
    }

}







