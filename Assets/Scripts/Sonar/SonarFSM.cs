using UnityEngine;
using AI.FSM;

[RequireComponent(typeof(ParticleSystem))]
public class SonarFSM : FSMBase {
    public float DurationSeconds = 10f;
    public float ColdDownSeconds = 10f;
    public float SpreadSpeed = 1f;
    public float Degree = 45f;
    public float LoopDuration = 0.5f;
    [HideInInspector]
    public Vector2 sonarTarget;
    public ParticleSystem Wave { get; private set; }

    protected override void init() {
        Wave = GetComponent<ParticleSystem>();
    }

    protected override void setUpFSM() {
        base.setUpFSM();

        SonarIdleState idleState = new SonarIdleState();
        idleState.AddMap(FSMTriggerID.LeftMouseDown, FSMStateID.SonarSpread);
        _states.Add(idleState);

        SonarSpreadState spreadState = new SonarSpreadState();
        spreadState.AddMap(FSMTriggerID.StateEnd, FSMStateID.ColdDown);
        _states.Add(spreadState);

        ColdDownState coldDownState = new ColdDownState();
        coldDownState.AddMap(FSMTriggerID.StateEnd, FSMStateID.SonarIdle);
        _states.Add(coldDownState);
    }
}
