using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Controls : MonoBehaviour
{
    private BasePlayerInputs _basePlayerInputs;
    protected BasePlayerInputs basePlayerInputs => _basePlayerInputs;

    protected Dictionary<string, Variables> _actionMaps;
    
    private void Awake()
    {
        if (_basePlayerInputs != null) return;
        _basePlayerInputs = new BasePlayerInputs();
    }

    protected void EnablePlayer()
    {
        _basePlayerInputs.Buildings.Disable();
        _basePlayerInputs.Player.Enable();
    }
    
    protected void EnableBuild()
    {
        _basePlayerInputs.Player.Disable();
        _basePlayerInputs.Buildings.Enable();
    } 
    
    protected void EnableUI()
    {
        _basePlayerInputs.Disable();
        _basePlayerInputs.UI.Enable();
    }
}

