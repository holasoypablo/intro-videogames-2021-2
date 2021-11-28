using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private AIConfig _aiConfig;
    [SerializeField] 
    private float angulo_vision = 140;   // Es el angulo total de vision

    private MovableAgent _movableAgent;
    private AIStateMachine _stateMachine;
    private PathContainer _pathContainer;

    public Transform Target => _player;
    public AIConfig AIConfig => _aiConfig;
    public MovableAgent MovableAgent => _movableAgent;
    public AIStateMachine StateMachine => _stateMachine;
    public PathContainer PathContainer => _pathContainer;

    void Start()
    {
        _movableAgent = GetComponent<MovableAgent>();
        _pathContainer = GetComponent<PathContainer>();
        
        _stateMachine = new AIStateMachine(this);
        
        _stateMachine.AddState(new AIIdleState());
        _stateMachine.AddState(new AIChaseTargetState());
        _stateMachine.AddState(new AIAttackState());
        _stateMachine.AddState(new AIPatrolState());
        
        _stateMachine.ChangeState(_aiConfig.initialState);
    }


    void Update()
    {
        _stateMachine.Update();
    }

    public bool IsLookingTarget()
    {
        Vector3 objetivo = (Target.position - transform.position).normalized;
        float angulo = Vector3.Angle(objetivo, transform.forward);
        if (angulo <= angulo_vision/2)
        {
            return true;
        }
        else {
            return false;
        }
        
    }
}
