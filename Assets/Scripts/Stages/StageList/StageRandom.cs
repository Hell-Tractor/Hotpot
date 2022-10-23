using UnityEngine;

public class StageRandom : StageBase {
    public const int InitialFoodCount = 10;

    public override void OnStageStart(StageManager stageManager, GameObject foodParent) {
        base.OnStageStart(stageManager, foodParent);

        for (int i = 0; i < InitialFoodCount; i++) {
            _generateRandomFood(foodParent);
        }
    }

    private void _generateRandomFood(GameObject foodParent) {
        string foodName = FoodPool.Instance.GetRandomFoodName();
        GameObject food = FoodPool.Instance.GetFood(foodName);
        GameObject foodInstance = GameObject.Instantiate(food, foodParent.transform);
        Vector2 randomScreenPoint = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        Vector2 worldpoint = Camera.main.ScreenToWorldPoint(randomScreenPoint);
        foodInstance.transform.position = new Vector3(worldpoint.x, worldpoint.y, 0);
    }

    public override void OnStageEnd(StageManager stageManager, GameObject foodParent) {
        base.OnStageEnd(stageManager, foodParent);

        if (State == StageState.Completed) {
            Debug.Log("Stage completed");
            return;
        }

        if (State == StageState.Failed) {
            Debug.Log("Stage failed");
            return;
        }
    }
}
