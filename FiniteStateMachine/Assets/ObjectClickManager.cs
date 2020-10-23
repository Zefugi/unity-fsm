using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ObjectClickManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask CustomLayerMask;
    [SerializeField]
    private Camera SpecificCamera;

    void Update()
    {
        var leftButton = Input.GetMouseButtonDown(0);
        var rightButton = Input.GetMouseButtonDown(1);

        if (!leftButton && !rightButton)
            return;

        InvokeClickEventIfRaycastHitsClickController(leftButton, rightButton);
    }

    private void InvokeClickEventIfRaycastHitsClickController(bool leftButton, bool rightButton)
    {
        var clickCtrl = GetClickController(GetRaycastObject());

        if (clickCtrl == null)
            return;

        if (leftButton)
            clickCtrl.LeftMouseClick.Invoke();
        if (rightButton)
            clickCtrl.RightMouseClick.Invoke();
    }

    private Transform GetRaycastObject()
    {
        if (Physics.Raycast(SpecificCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100, CustomLayerMask))
            return hitInfo.transform;
        return null;
    }

    private ObjectClickController GetClickController(Transform transform)
    {
        return transform?.GetComponent<ObjectClickController>();
    }
}
