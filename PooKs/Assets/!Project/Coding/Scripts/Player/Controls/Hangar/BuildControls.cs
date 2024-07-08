using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildControls : Controls
{
    //private HangarControls _hangarControls;
    private Vector2 _lastMousePos;
    
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _building;
    [SerializeField] private LayerMask _baseColliderLayer;
    [SerializeField] private LayerMask _groundLayer;

    private void OnEnable()
    {
        OnCall();
        basePlayerInputs.Building.FindPos.performed += PositionWithMouse;
        basePlayerInputs.Building.MoveBuilding.performed += MoveBuilding;
        //basePlayerInputs.Buildings.Confirm.performed += Confirm;
        //_hangarControls = GetComponent<HangarControls>();
    }

    private void OnDisable()
    {
        basePlayerInputs.Building.FindPos.performed -= PositionWithMouse;
        basePlayerInputs.Building.MoveBuilding.performed -= MoveBuilding;
    }

    public void OnCall()
    {
        _building = Instantiate(_building);
        EnableBuild();
    }

    private void MoveBuilding(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();
        _building.transform.position = GroundInfo.NavigatingPosition(inputDir, _building.transform.position, _groundLayer);
    }

    private void PositionWithMouse(InputAction.CallbackContext context)
    {
        //if(MouseReferences.CheckMouseDisplacement(context, _lastMousePos, .1f))return;
        Vector2 value = context.ReadValue<Vector2>();
        Ray mouseRay = _mainCamera.ScreenPointToRay(value);
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 200f, _baseColliderLayer))
        {
            _building.transform.position = hitInfo.point.LayerHit(_groundLayer, Vector3.down);
        }
    }
    
    private void Confirm(InputAction.CallbackContext context)
    {
        //_hangarControls.enabled = true;
        EnableRun();
        this.enabled = false;
    }
}