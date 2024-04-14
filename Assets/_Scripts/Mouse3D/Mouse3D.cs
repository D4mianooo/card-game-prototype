using UnityEngine;

public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance;

    [Tooltip("Layers to ignore when computing mouse position")]
    [SerializeField] private LayerMask _ignore;

    private void Start() {
        Instance = this;
    }
    private void FixedUpdate() {
        transform.position = GetMouseWorldPosition();
    }
    public Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, ~_ignore)) {
            return Vector3.zero;
        }

        return hit.point;
    }
}