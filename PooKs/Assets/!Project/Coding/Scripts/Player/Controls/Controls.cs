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
        _basePlayerInputs.Hangar.Disable();
        _basePlayerInputs.Run.Enable();
    }
    
    protected void EnableHangar()
    {
        _basePlayerInputs.Run.Disable();
        _basePlayerInputs.Hangar.Enable();
    }
    
}

