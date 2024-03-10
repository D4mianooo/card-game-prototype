using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitContoller : MonoBehaviour
{
    private void OnMouseDrag() {
        FollowMouse3D();
    }
        
    
    private void OnMouseUp() {
        Vector3 parentPosition = transform.parent.position;
        transform.parent.position = new Vector3(Mathf.Round(parentPosition.x), parentPosition.y, Mathf.Round(parentPosition.z));
    }
    private void FollowMouse3D() {
        transform.parent.position = Mouse3D.GetMouseWorldPosition(~LayerMask.GetMask("Unit"));
    }
}

