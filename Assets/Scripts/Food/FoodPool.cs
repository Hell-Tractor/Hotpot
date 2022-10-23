using System.Collections.Generic;
using UnityEngine;

public class FoodPool : MonoBehaviour {
    public static FoodPool _instance = null;
    public static FoodPool Instance {
        get {
            if (_instance == null) {
                return _instance = GameManager.Instance.gameObject.AddComponent<FoodPool>();
            }
            return _instance;
        }
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        }
    }

    private Dictionary<string, GameObject> _foodPool = new Dictionary<string, GameObject>();

    public GameObject GetFood(string foodName) {
        if (!_foodPool.ContainsKey(foodName)) {
            _foodPool.Add(foodName, this._loadFood(foodName));
        }
        return _foodPool[foodName];
    }

    private GameObject _loadFood(string foodName) {
        GameObject food = Resources.Load<GameObject>($"Food/{foodName}");
        if (food == null) {
            Debug.LogError($"Food {foodName} not found");
            return null;
        }
        return food;
    }
}
