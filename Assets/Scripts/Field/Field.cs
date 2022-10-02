using System;
using UnityEngine;

public class Field : MonoBehaviour {
    public Rect Bounds;
    public int HorizontalCount;
    public int VerticalCount;
    [HideInInspector]
    public FieldGrid[] Grids;

    private void Start() {
    }

    private void Update() {
        // update all grids
        foreach (var grid in Grids) {
            grid.Update();
        }

        // add force to all foods
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        foreach (var food in foods) {
            var foodPosition = food.transform.position;
            food.GetComponent<Rigidbody2D>().AddForce(GetGrid(foodPosition)?.Force ?? new Vector2(0, 0));
        }
    }

    public FieldGrid GetGrid(Vector3 position) {
        var x = Mathf.Clamp(position.x, Bounds.xMin, Bounds.xMax);
        var y = Mathf.Clamp(position.y, Bounds.yMin, Bounds.yMax);
        var gridX = (int)((x - Bounds.xMin) / Bounds.width * HorizontalCount);
        var gridY = (int)((y - Bounds.yMin) / Bounds.height * VerticalCount);
        if (gridX + gridY * HorizontalCount >= Grids.Length) {
            return null;
        }
        return Grids[gridX + gridY * HorizontalCount];
    }
}
