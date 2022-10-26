using System;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public string StageToLoad = "Stage1";
    public GameObject FoodParent;
    public GameObject Chopsticks;
    public MaskController Mask;
    private StageBase _currentStage = null;
    private float? _lastLength = null;

    private void Start() {
        if (StageToLoad.Length != 0)
            this.LoadStage(StageToLoad);
    }

    private void Update() {
        if (_currentStage != null) {
            //_currentStage.CheckStageState(this, FoodParent);
            if (_currentStage.State == StageState.InProgress)
                _currentStage.OnStageUpdate(this, FoodParent);
            else if (_currentStage.State == StageState.Completed || _currentStage.State == StageState.Failed) {
                _currentStage.OnStageEnd(this, FoodParent);
                _currentStage = null;
            }

            if (Input.GetMouseButtonDown(0)) {
                Chopsticks.GetComponent<BothChopstickBehaviour>().Fetch(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    food => {
                        if (food != null) {
                            food.SetActive(false);
                        }
                    },
                    food => {
                        if (food != null) {
                            Chopsticks.GetComponent<BothChopstickBehaviour>().AddPart(food.GetComponent<FoodBehaviour>().PartPrefab);
                            Destroy(food);
                            if (_lastLength == null) {
                                _lastLength = Chopsticks.GetComponent<BothChopstickBehaviour>().CurrentLength;
                                Mask.TargetY -= (float)_lastLength;
                            } else {
                                float _currentLength = Chopsticks.GetComponent<BothChopstickBehaviour>().CurrentLength;
                                Mask.TargetY -= _currentLength - _lastLength.Value;
                                _lastLength = _currentLength;
                            }
                        }
                    }
                );
            }
        }
    }

    public void LoadStage(string stage, params object[] args) {
        Type stageType = Type.GetType(stage);
        if (stageType == null) {
            Debug.LogError($"Stage {stage} not found");
            return;
        }
        _currentStage = Activator.CreateInstance(stageType, args) as StageBase;
        _currentStage.OnStageStart(this, FoodParent);
    }
}
