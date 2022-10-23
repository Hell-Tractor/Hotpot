using System;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public string StageToLoad = "Stage1";
    public GameObject FoodParent;
    public GameObject Chopsticks;
    private StageBase _currentStage = null;

    private void Start() {
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
                        if (food != null)
                            food.SetActive(false);
                    },
                    food => {
                        if (food != null) {
                            Chopsticks.GetComponent<BothChopstickBehaviour>().AddPart(food.GetComponent<FoodBehaviour>().PartPrefab);
                            Destroy(food);
                        }
                    }
                );
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
