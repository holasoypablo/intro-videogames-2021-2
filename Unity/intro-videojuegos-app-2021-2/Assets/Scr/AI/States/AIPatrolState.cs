using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{
    private float tActual = 0;
    private Vector3 p_Actual;
    
    public AIStateID GetID()
    {
        return AIStateID.Patrol;
    }

    public void Enter(AIAgent agent)
    {
        p_Actual = agent.transform.position;
    }

    public void Update(AIAgent agent)
    {
        //Caminar al siguiente punto
        if (agent.transform.position == p_Actual)
        {
            if (tActual < 0)
            {
                tActual = 5;
                p_Actual = agent.PathContainer.NextPoint();
                agent.MovableAgent.GoTo(p_Actual);
            }
            else
            {
                tActual -= Time.deltaTime;
            }
        }

        //Si esta viendo al player
        if (agent.IsLookingTarget())
        {
            //agent.StateMachine.ChangeState(AIStateID.ChaseTarget);
        }
    }

    public void Exit(AIAgent agent)
    {
        agent.MovableAgent.Stop();
    }
}
