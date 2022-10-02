using UnityEngine;
using AI.FSM;

public class Sonar : FSMBase {
    public float DurationSeconds = 10f;
    public float ColdDownSeconds = 10f;
    public float SpreadSpeed = 1f;
    public float Degree = 45f;
    [HideInInspector]
    public Vector2 sonarTarget;

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
