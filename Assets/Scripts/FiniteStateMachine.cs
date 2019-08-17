using UnityEngine.Events;

public class FiniteStateMachine<T>
{
    Table<FsmState<T>, UnityEvent, FsmState<T>> fsmTable;
    FsmState<T> currentState;

    public FiniteStateMachine(FsmState<T>[] states, UnityEvent[] transitionEvents, FsmState<T> entryState)
    {
        fsmTable = new Table<FsmState<T>, UnityEvent, FsmState<T>>(states, transitionEvents);
        currentState = entryState;
        currentState.EnterState();
    }

    void OnTransitionEventTrigger(UnityEvent transitionEvent)
    {
        FsmState<T> targetState = fsmTable[currentState, transitionEvent];

        if (targetState != null)
        {
            currentState.ExitState();
            currentState = targetState;
            targetState.EnterState();
        }
    }

    public void SetTransitionRelation(FsmState<T> sourceState, FsmState<T> targetState, UnityEvent transitionEvent)
    {
        fsmTable[sourceState, transitionEvent] = targetState;
        transitionEvent.AddListener(() => OnTransitionEventTrigger(transitionEvent));
    }

    public void UpdateCurrentState()
    {
        currentState.UpdateState();
    }
}
