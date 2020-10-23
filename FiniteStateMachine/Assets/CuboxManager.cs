using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CuboxManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask ClickColliderLayerMask;

    private List<CuboxController> _boxes = new List<CuboxController>();
    public ReadOnlyCollection<CuboxController> Boxes => _boxes.AsReadOnly();

    public CuboxController Selected { get; private set; }

    private bool _leftButtonDown;
    private bool _rightButtonDown;

    private void Start()
    {
        _boxes.AddRange(FindObjectsOfType<CuboxController>());
    }

    private void Update()
    {
        UpdateInput();

        if (CuboxWasLeftClicked(out CuboxController cubox))
            UpdateSelection(cubox);

        if (PointWasRightClicked(out Vector3 point))
            UpdateLookAtPointIfSelectedIsLookAtState(point);
    }

    private void UpdateInput()
    {
        _leftButtonDown = Input.GetMouseButtonDown(0);
        _rightButtonDown = Input.GetMouseButtonDown(1);
    }

    private bool CuboxWasLeftClicked(out CuboxController cubox)
    {
        cubox = _leftButtonDown
            && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100, ClickColliderLayerMask)
            ? hitInfo.transform.parent.GetComponent<CuboxController>() : null;

        return cubox != null;
    }

    private bool PointWasRightClicked(out Vector3 point)
    {
        point = _rightButtonDown
            && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100)
            ? hitInfo.point : Vector3.zero;
        
        return point != Vector3.zero;
    }

    private void UpdateSelection(CuboxController cubox)
    {
        if (cubox == Selected)
            return;

        Selected?.Deselect();
        Selected = cubox;
        Selected?.Select();
    }

    private void UpdateLookAtPointIfSelectedIsLookAtState(Vector3 point)
    {
        if (Selected == null
            || !(Selected.StateMachine.CurrentState is CuboxLookAtState))
            return;

        (Selected.StateMachine.CurrentState as CuboxLookAtState).LookAtPoint = point;
    }
}
