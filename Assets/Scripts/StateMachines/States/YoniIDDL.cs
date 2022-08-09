using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoniIDDL : AiState

{
    public AiStateId GetId()
    {
        return AiStateId.idle;
    }
    public void Enter(AiAgent agent)
    {
        GameManager.instance.ShowText("La plata o plomo", 25, Color.green, agent.transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, 1.0f);

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

