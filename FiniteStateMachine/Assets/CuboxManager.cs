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

        if(_leftButtonDown)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100, ClickColliderLayerMask))
                UpdateSelection(hitInfo.transform);
            else
                UpdateSelection(null);
    }

    private void UpdateInput()
    {
        _leftButtonDown = Input.GetMouseButtonDown(0);
        _rightButtonDown = Input.GetMouseButtonDown(1);
    }

    private void UpdateSelection(Transform selected)
    {
        var box = selected?.GetComponentInParent<CuboxController>();

        if (box == Selected)
            return;

        Selected?.Deselect();
        Selected = box;
        Selected?.Select();
    }
}
