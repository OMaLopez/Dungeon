using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public int bossHp, bossMaxHp, bossTriger;

    void Start()
    {
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChase());
        stateMachine.RegisterState(new AiIdle());
        stateMachine.ChangeState(initialState);
    }


    void Update()
    {
        stateMachine.Update();
    }
}
