using UnityEngine;

public enum StageState {
    NotStarted,
    InProgress,
    Completed,
    Failed
}

public abstract class StageBase {
    public StageState State { get; protected set; } = StageState.NotStarted;
    protected float _bottomBound;
    public virtual void OnStageStart(StageManager stageManager, GameObject foodParent) {
        State = StageState.InProgress;
        _bottomBound = Camera.main.ScreenToWorldPoint(Vector2.zero).y;
        stageManager.Chopsticks.SetActive(true);
    }
    public virtual void CheckStageState(StageManager stageManager, GameObject foodParent) {
        if (State == StageState.InProgress && stageManager.Chopsticks.GetComponent<BothChopstickBehaviour>().GetChopstickBottom() < _bottomBound) {
            State = StageState.Completed;
        }
        if (State == StageState.InProgress && foodParent.transform.childCount == 0) {
            State = StageState.Failed;
        }
    }
    public virtual void OnStageUpdate(StageManager stageManager, GameObject foodParent) {}
    public virtual void OnStageEnd(StageManager stageManager, GameObject foodParent) {
        stageManager.Chopsticks.SetActive(false);
    }
}
