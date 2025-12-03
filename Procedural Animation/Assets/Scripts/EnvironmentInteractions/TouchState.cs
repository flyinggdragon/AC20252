using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchState : EnvironmentInteractionState {
    float _elapsedTime = 0.0f;
    float _resetThreshold = 0.5f;

    public TouchState(EnvironmentInteractionContext context, EnvironmentInteractionStateMachine.EEnvironmentInteractionState estate) : base(context, estate) {
        EnvironmentInteractionContext Context = context;
    }

    public override void EnterState() {
        _elapsedTime = 0.0f;
    }
    public override void ExitState() { }
    public override void UpdateState() {
        _elapsedTime += Time.deltaTime;
    }
    public override EnvironmentInteractionStateMachine.EEnvironmentInteractionState GetNextState() {
        if (_elapsedTime > _resetThreshold) {
            return EnvironmentInteractionStateMachine.EEnvironmentInteractionState.Reset;
        }
        
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other) {
        StartIkTargetPositionTracking(other);
    }
    public override void OnTriggerStay(Collider other) {
        UpdateIkTargetPosition(other);
    }
    public override void OnTriggerExit(Collider other) {
        ResetIkTargetPositionTracking(other);
    }
}