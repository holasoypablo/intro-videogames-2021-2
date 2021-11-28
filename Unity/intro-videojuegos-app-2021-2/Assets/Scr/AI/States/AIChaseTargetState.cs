using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseTargetState : AIState
{
    private float _timerToRefresh = 0;
    
    public AIStateID GetID()
    {
        return AIStateID.ChaseTarget;
    }

    public void Enter(AIAgent agent)
    {
    }

    public void Update(AIAgent agent)
    {
        if (agent.Target == null)
        {
            return;
        }

        _timerToRefresh -= Time.deltaTime;

        Vector3 directionToTarget = agent.Target.position - agent.transform.position;
        directionToTarget.y = 0;

        float sqrDistanceToPlayer = directionToTarget.sqrMagnitude;
        
        if (_timerToRefresh < 0f)
        {

            //If is to close to the target, we don't update the path
            if (sqrDistanceToPlayer > agent.AIConfig.pathfindingMinDistanceToRefresh * agent.AIConfig.pathfindingMinDistanceToRefresh)
            {
                agent.MovableAgent.GoTo(agent.Target.position - directionToTarget.normalized * 0.75f,
                    () =>
                    {
                        //Do something...
                        OnArrive(agent);
                    }
                );
            }
            


            _timerToRefresh = agent.AIConfig.pathfindingRefreshTime;
        }
         //Si no esta viendo al player
        if (!agent.IsLookingTarget())
        {
            agent.StateMachine.ChangeState(AIStateID.Idle);
        }
        //If close enough -> Attack
        if (sqrDistanceToPlayer < agent.AIConfig.attackRange * agent.AIConfig.attackRange)
        {
            agent.StateMachine.ChangeState(AIStateID.Attack);
        }

    }

    public void Exit(AIAgent agent)
    {
        //Make sure to stop the NavMesh agent
        agent.MovableAgent.Stop();
    }

    private void OnArrive(AIAgent agent)
    {
        //Do something...
    }
}
