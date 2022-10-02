using UnityEditor;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Field))]
public class FieldEditor : MonoBehaviour {
    public GameObject GridPrefab;
    public GameObject[] Grids { get; private set; } = null;
    private int _currentWidth = -1, _currentHeight = -1;
    private void Update() {
        if (Application.isPlaying)
            return;
        Field field = this.GetComponent<Field>();
        if (_currentHeight != field.VerticalCount || _currentWidth != field.HorizontalCount) {
            // Debug.Log("Field size changed, updating...");
            // Debug.Log("VerticalCount: " + field.VerticalCount + ", HorizontalCount: " + field.HorizontalCount);
            // Debug.Log("currentWidth " + _currentWidth + " currentHeight " + _currentHeight);
            _destroyGrids();
            _generateGrids();
        }
    }
    private void Start() {
        EditorApplication.playModeStateChanged += (PlayModeStateChange obj) => {
            if (obj == PlayModeStateChange.ExitingEditMode) {
                // Debug.Log("Exiting edit mode, destroying grids...");
                _applyGrids();
                _destroyGrids();
            }
        };
    }

    private void _destroyGrids() {
        if (Grids != null) {
            foreach (var grid in Grids) {
                if (grid == null)
                    continue;
                // Debug.Log("Destroying grid " + grid.name);
                DestroyImmediate(grid);
            }
        }
    }

    private void _generateGrids() {
        Field field = this.GetComponent<Field>();
        Vector3 gridSize = new Vector3(field.Bounds.width / field.HorizontalCount, field.Bounds.height / field.VerticalCount, 1);
        Grids = new GameObject[field.HorizontalCount * field.VerticalCount];
        _currentHeight = field.VerticalCount;
        _currentWidth = field.HorizontalCount;
        // Generate Grids
        for (int i = 0; i < field.HorizontalCount; i++) {
            for (int j = 0; j < field.VerticalCount; j++) {
                int index = i * field.VerticalCount + j;
                Grids[index] = Instantiate(GridPrefab, this.transform);
                Grids[index].transform.position = new Vector3(field.Bounds.xMin + field.Bounds.width * (i + 0.5f) / field.HorizontalCount, field.Bounds.yMin + field.Bounds.height * (j + 0.5f) / field.VerticalCount, 0);
                Grids[index].transform.localScale = gridSize;
                Grids[index].name = $"Grid {i} {j}";
                if (field.Grids != null && field.Grids.Length > index)
                    Grids[index].GetComponent<FieldGridEditor>().data = field.Grids[index];
            }
        }
    }

    private void _applyGrids() {
        if (Grids == null)
            return;
        Field field = this.GetComponent<Field>();
        field.Grids = new FieldGrid[Grids.Length];
        for (int i = 0; i < Grids.Length; i++) {
            field.Grids[i] = Grids[i].GetComponent<FieldGridEditor>().data;
        }
    }
}