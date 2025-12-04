# Procedural Animation:
Criação de um protótipo para testar a implementação de animações procedurais na Unity. O personagem deve enconstar a mão na parede ao se aproximar.

# Links:
- [Video demonstração](https://www.youtube.com/watch?v=bCcWzwIzfdQ)
- [Apresentação de Slides](https://github.com/flyinggdragon/AC20252/blob/main/Procedural%20Animation/Animação%20Procedural.pdf)

# Estrutura e Funcionamento:
Foi criado um cenário para teste com algumas paredes.

A animação foi dividida em 5 estágios (estados). Para gerenciar a troca de estados, foi criado uma máquina de estados, e cada estado único tornou-se uma classe concreta, que herda de uma classe pai.
```cs
public abstract class StateManager<EState> : MonoBehaviour where EState : Enum { [...] }
```

Os estados são: **_Search, Approach, Rise, Touch e Reset_**.
```cs
public class EnvironmentInteractionStateMachine : StateManager<EnvironmentInteractionStateMachine.EEnvironmentInteractionState> {
    public enum EEnvironmentInteractionState {
        Search,
        Approach,
        Rise,
        Touch,
        Reset
    }

  [...]
}
```

Cada classe cuida dos seus procedimentos individualmente, de como o estado interage com o rig, utilizando métodos que devem ser sobrescritos. Por exemplo:
```cs
public abstract class StateManager<EState> : MonoBehaviour where EState : Enum {
  private void OnTriggerEnter(Collider other) {
        currentState.OnTriggerEnter(other);
    }
    private void OnTriggerStay(Collider other) {
        currentState.OnTriggerStay(other);
    }
    private void OnTriggerExit(Collider other) {
        currentState.OnTriggerExit(other);
    }

    private void Start() {
        currentState.EnterState();
    }
    private void Update() {
        EState nextStateKey = currentState.GetNextState();

        if (!isTransitioningState && nextStateKey.Equals(currentState.StateKey)) {
            currentState.UpdateState();
        }
        else if (!isTransitioningState) {
            TransitionToState(nextStateKey);
        }
    }
}
```

```cs
public abstract class EnvironmentInteractionState : BaseState<EnvironmentInteractionStateMachine.EEnvironmentInteractionState> { [...] }
```

```cs
public class ResetState : EnvironmentInteractionState {
  public override void EnterState() {
        _elapsedTime = 0.0f;
        Context.ClosestPointOnColliderFromShoulder = Vector3.positiveInfinity;
        Context.CurrentIntersectingCollider = null;
    }
    public override void ExitState() { }
    public override void UpdateState() {
        // Redefine a posição do braço.
        _elapsedTime += Time.deltaTime;
        Context.InteractionPointYOffset = Mathf.Lerp(Context.InteractionPointYOffset, Context.ColliderCenterY, _elapsedTime / _lerpDuration);
        Context.CurrentIkConstraint.weight = Mathf.Lerp(Context.CurrentIkConstraint.weight, 0, _elapsedTime / _lerpDuration);
        Context.CurrentMultiRotationConstraint.weight = Mathf.Lerp(Context.CurrentMultiRotationConstraint.weight, 0, _elapsedTime / _lerpDuration);
        Context.CurrentIkTargetTransform.rotation = Quaternion.RotateTowards(Context.CurrentIkTargetTransform.rotation, Context.OriginalTargetRotation, _rotationSpeed * Time.deltaTime);
    }
}
```

Há uma classe intermediária, chamada de _EnvironmentInteractionContext_ (o contexto), que guarda informações importantes a serem compartilhadas entre todos os estados.
```cs
public class EnvironmentInteractionContext  {
    [...]

    public Collider CurrentIntersectingCollider { get; set; }
    public TwoBoneIKConstraint CurrentIkConstraint { get; private set; }
    public MultiRotationConstraint CurrentMultiRotationConstraint { get; private set; }
    public Transform CurrentIkTargetTransform { get; private set; }
    public Transform CurrentShoulderTransform { get; private set; }
    public EBodySide CurrentBodySide { get; private set; }
    public Vector3 ClosestPointOnColliderFromShoulder { get; set; } = Vector3.positiveInfinity;
    public float InteractionPointYOffset { get; set; } = 0.0f;
    public float ColliderCenterY { get; set; }
    public Vector3 CurrentOriginalTargetPosition { get; private set; }
    public Quaternion OriginalTargetRotation { get; private set; }

    [...]
}
```
