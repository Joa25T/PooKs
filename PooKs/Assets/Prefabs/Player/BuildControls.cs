using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseControls))]
public class BuildControls : Controls
{
    private BaseControls _baseControls;
    
    private Vector2 _lastMousePos;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _building;
    [SerializeField] private GameObject _buildingCamera;
    [SerializeField] private LayerMask _ignoreOnMouseRaycast;

    private void OnEnable()
    {
        basePlayerInputs.Buildings.Point.performed += Pointing;
        basePlayerInputs.Buildings.Navigate.performed += Navigating;
        basePlayerInputs.Buildings.Confrim.performed += Confirm;
        _baseControls = GetComponent<BaseControls>();

    }

    public void OnCall()
    {
        _building.SetActive(true);
        _buildingCamera.SetActive(true);
    }

    private void Navigating(InputAction.CallbackContext context)
    {
        //_basePlayerInputs.Buildings.Point.Disable();
        Vector2 inputDir = context.ReadValue<Vector2>(); 
        
        _building.transform.position = 
            new Vector3(_building.transform.position.x + inputDir.x,
                _building.transform.position.y, 
                _building.transform.position.z + inputDir.y);
    }

    private void Pointing(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        if (Vector2.Distance(value, _lastMousePos)< 0.1f)return;
        _lastMousePos = value;
        Ray pointHit = _mainCamera.ScreenPointToRay(context.ReadValue<Vector2>());
        if(Physics.Raycast(pointHit, out RaycastHit hitInfo,200f,_ignoreOnMouseRaycast ))
        {
            if (Vector3.Distance(hitInfo.point,_building.transform.position) < 3) return;
            
            _building.transform.position =
                new Vector3((int)hitInfo.point.x, 
                    (hitInfo.point.y + 1.5f), 
                    (int)hitInfo.point.z);
        }
    }

    private void Confirm(InputAction.CallbackContext context)
    {
/*        basePlayerInputs.Buildings.Point.performed -= Pointing;
        basePlayerInputs.Buildings.Navigate.performed -= Navigating;
        basePlayerInputs.Buildings.Confrim.performed -= Confirm;
        _buildingCamera.SetActive(false); */
        _baseControls.enabled = true;
        EnablePlayer();
        this.enabled = false;
    }
}
