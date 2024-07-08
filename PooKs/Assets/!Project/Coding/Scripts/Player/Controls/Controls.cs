using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private BasePlayerInputs _basePlayerInputs;
    protected BasePlayerInputs basePlayerInputs => _basePlayerInputs;
    
    
    private void Awake()
    {
        if (_basePlayerInputs != null) return;
        _basePlayerInputs = new BasePlayerInputs();
    }

    protected void EnableRun()
    {
        _basePlayerInputs.Building.Disable();
        _basePlayerInputs.Hangar.Disable();
        _basePlayerInputs.Run.Enable();
    }
    
    protected void EnableHangar()
    {
        _basePlayerInputs.Run.Disable();
        _basePlayerInputs.Building.Disable();
        _basePlayerInputs.Hangar.Enable();
    }
    
    protected void EnableBuild()
    {
        _basePlayerInputs.Run.Disable();
        _basePlayerInputs.Hangar.Disable();
        _basePlayerInputs.Building.Enable();
    }
    
}

