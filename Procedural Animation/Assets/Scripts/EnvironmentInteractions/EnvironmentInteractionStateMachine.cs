using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Assertions;

public class EnvironmentInteractionStateMachine : StateManager<EnvironmentInteractionStateMachine.EEnvironmentInteractionState> {
    public enum EEnvironmentInteractionState {
        Search,
        Approach,
        Rise,
        Touch,
        Reset
    }

    private EnvironmentInteractionContext _context;

    [SerializeField] private TwoBoneIKConstraint _leftIkConstraint;
    [SerializeField] private TwoBoneIKConstraint _rightIkConstraint;
    [SerializeField] private MultiRotationConstraint _leftMultiRotationConstraint;
    [SerializeField] private MultiRotationConstraint _rightMultiRotationConstraint;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _rootCollider;

    private void Awake() {
        ValidateConstraints();

        _context = new EnvironmentInteractionContext(_leftIkConstraint, _rightIkConstraint, _leftMultiRotationConstraint, _rightMultiRotationConstraint, _rigidbody, _rootCollider, transform.root);

        ConstructEnvironmentDetectionCollider();
        InitializeStates();
    } 

    private void ValidateConstraints() {
        Assert.IsNotNull(_leftIkConstraint, "Left IK constraint is not assigned.");
        Assert.IsNotNull(_rightIkConstraint, "Right IK constraint is not assigned.");
        Assert.IsNotNull(_leftMultiRotationConstraint, "Left multi-rotation constraint is not assigned.");
        Assert.IsNotNull(_rightMultiRotationConstraint, "Right multi-rotation constraint is not assigned.");
        Assert.IsNotNull(_rigidbody, "Rigidbody to control character is not assigned.");
        Assert.IsNotNull(_rootCollider, "RootCollider attached to character is not assigned.");
    }

    private void InitializeStates() {
        states.Add(EEnvironmentInteractionState.Reset, new ResetState(_context, EEnvironmentInteractionState.Reset));
        states.Add(EEnvironmentInteractionState.Search, new SearchState(_context, EEnvironmentInteractionState.Search));
        states.Add(EEnvironmentInteractionState.Approach, new ApproachState(_context, EEnvironmentInteractionState.Approach));
        states.Add(EEnvironmentInteractionState.Rise, new RiseState(_context, EEnvironmentInteractionState.Rise));
        states.Add(EEnvironmentInteractionState.Touch, new TouchState(_context, EEnvironmentInteractionState.Touch));

        currentState = states[EEnvironmentInteractionState.Reset];
    }

    private void ConstructEnvironmentDetectionCollider() {
        float wingspan = _rootCollider.height;

        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new(wingspan, wingspan, wingspan);
        boxCollider.center = new(_rootCollider.center.x, _rootCollider.center.y + (wingspan * .25f), _rootCollider.center.z + (wingspan * .5f));
        boxCollider.isTrigger = true;
    }
}