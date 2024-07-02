using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseControls))]
public class BuildControls : Controls
{
    private BaseControls _baseControls;
    
    private Vector2 _lastMousePos;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _building;
    [SerializeField] private LayerMask _ignoreOnMouseRaycast;
    [SerializeField] private LayerMask _ignoreAllButFloor;

    private void OnEnable()
    {
        basePlayerInputs.Buildings.Point.performed += Pointing;
        basePlayerInputs.Buildings.Navigate.performed += Navigating;
        basePlayerInputs.Buildings.Confirm.performed += Confirm;
        _baseControls = GetComponent<BaseControls>();
    }

    public void OnCall()
    {
        _building.SetActive(true);
        EnableBuild();
    }

    private void Navigating(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>(); 
        
        _building.transform.position = 
            new Vector3(_building.transform.position.x + inputDir.x,
                _building.transform.position.y, 
                _building.transform.position.z + inputDir.y);
    }

    private void Pointing(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        if (Vector3.Distance(value , _lastMousePos) < 0.3f)return;
        
        Ray mouseRay = _mainCamera.ScreenPointToRay(value);
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 200f, _ignoreOnMouseRaycast))
        {
            _building.transform.position = FloorRefence(hitInfo.point);
        }
        
    }

    private void Confirm(InputAction.CallbackContext context)
    {
        _baseControls.enabled = true;
        EnablePlayer();
        this.enabled = false;
    }

    private Vector3 FloorRefence(Vector3 point)
    {
        Ray centerRay = new Ray(point, Vector3.down);
        if (Physics.Raycast(centerRay, out RaycastHit hitInfo, 200f, _ignoreAllButFloor))
        {
            Vector3 centerPos;
            centerPos.x = (int)hitInfo.point.x;
            centerPos.y = (int)hitInfo.point.y;
            centerPos.z = 0;
            return centerPos;
        }
        return Vector3.zero;
    }
    
}
