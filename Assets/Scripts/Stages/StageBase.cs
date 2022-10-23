using UnityEngine;

public enum StageState {
    NotStarted,
    InProgress,
    Completed,
    Failed
}

public abstract class StageBase {
    public StageState State { get; protected set; } = StageState.NotStarted;
    public virtual void OnStageStart(StageManager stageManager, GameObject foodParent) {
        State = StageState.InProgress;
    }
    public abstract void OnStageUpdate(StageManager stageManager, GameObject foodParent);
    public abstract void OnStageEnd(StageManager stageManager, GameObject foodParent);
}
