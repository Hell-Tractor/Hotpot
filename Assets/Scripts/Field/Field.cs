using System;
using UnityEngine;

public class Field : MonoBehaviour {
    public Rect Bounds;
    public int HorizontalCount;
    public int VerticalCount;
    private FieldGrid[] Grids;

    [SerializeField]
    private _FieldGridEdit[] GridsEdit;

    [Serializable]
    private class _FieldGridEdit {
        [SerializeField]
        public FieldGrid Grid;
        public int x;
        public int y;
    }

    private void Start() {
        this._applyGridsEdit();
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

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Bounds.center, Bounds.size);
        if (Grids != null) {
            for (int gridX = 0; gridX < HorizontalCount; gridX++) {
                for (int gridY = 0; gridY < VerticalCount; gridY++) {
                    // calculate grid bounds
                    var gridBounds = new Rect(
                        Bounds.xMin + Bounds.width * gridX / HorizontalCount,
                        Bounds.yMin + Bounds.height * gridY / VerticalCount,
                        Bounds.width / HorizontalCount,
                        Bounds.height / VerticalCount
                    );
                    // draw grid bounds
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(gridBounds.center, gridBounds.size);
                }
            }
            // draw grid forces
            foreach (var gridEdit in GridsEdit) {
                var gridBounds = new Rect(
                    Bounds.xMin + Bounds.width * gridEdit.x / HorizontalCount,
                    Bounds.yMin + Bounds.height * gridEdit.y / VerticalCount,
                    Bounds.width / HorizontalCount,
                    Bounds.height / VerticalCount
                );
                Gizmos.color = new Color(0.5f, 0.5f, 1.0f);
                Gizmos.DrawLine(gridBounds.center, gridBounds.center + gridEdit.Grid.Force / 3f);
            }
        }
    }

    private void _applyGridsEdit() {
        Grids = new FieldGrid[HorizontalCount * VerticalCount];
        for (int i = 0; i < Grids.Length; i++) {
            Grids[i] = new FieldGrid();
        }
        foreach (var gridEdit in GridsEdit) {
            Grids[gridEdit.x + gridEdit.y * HorizontalCount] = gridEdit.Grid;
        }
    }
}
