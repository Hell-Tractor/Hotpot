using System;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public string StageToLoad = "Stage1";
    public GameObject FoodParent;
    private StageBase _currentStage = null;

    private void Start() {
        this.LoadStage(StageToLoad);
    }

    private void Update() {
        if (_currentStage != null) {
            if (_currentStage.State == StageState.InProgress)
                _currentStage.OnStageUpdate(this, FoodParent);
            else if (_currentStage.State == StageState.Completed || _currentStage.State == StageState.Failed) {
                _currentStage.OnStageEnd(this, FoodParent);
                _currentStage = null;
            }
        }
    }

    public void LoadStage(string stage) {
        Type stageType = Type.GetType(stage);
        if (stageType == null) {
            Debug.LogError($"Stage {stage} not found");
            return;
        }
        _currentStage = Activator.CreateInstance(stageType) as StageBase;
        _currentStage.OnStageStart(this, FoodParent);
    }
}
