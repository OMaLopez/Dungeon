using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdle : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.idle;
    }
    public void Enter(AiAgent agent)
    {
        agent.bossTriger = Boss.instance.hitpoints / 4;
        agent.bossMaxHp = Boss.instance.hitpoints;

        Boss.instance.chaseLenght = 0.0f;

    }


    public void Update(AiAgent agent)
    {
        agent.bossHp = Boss.instance.hitpoints;
        if (agent.bossMaxHp - agent.bossTriger > agent.bossHp)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
        if (agent)
            GameManager.instance.ShowText("Aghh", 25, Color.white, agent.transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, 1.0f);

        if (Boss.instance.adds < 2)
        {
            Boss.instance.adds++;
        }


    }
}
