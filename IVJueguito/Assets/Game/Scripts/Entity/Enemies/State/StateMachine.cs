using UnityEngine;

public class StateMachine
{
 
    private EnemyState currentState;
    public EnemyState GetCurrentState()
    {
        return currentState;
    }
    public void Initialize(EnemyState startingState, Enemy owner)
    {
        ChangeState(startingState, owner);
    }

    public void Update(Enemy owner, float deltaTime)
    {
        if (currentState != null)
        {
            currentState.Execute(owner, deltaTime);
        }
    }

    public void ChangeState(EnemyState newState, Enemy owner)
    {
       
        if (currentState != null)
        {
            currentState.Exit(owner);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(owner);
        }
    }
}
