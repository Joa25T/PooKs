using UnityEngine;

public class ShipReader_T : MonoBehaviour
{
    [SerializeField] private SO_PartSelection selectedPart;

    private void OnEnable()
    {
        //if (selectedPart== null) return;
        Debug.LogError($"Ship Name:{selectedPart.ShipName}, the selected body is {selectedPart.Body.Name}, the selected weapon is {selectedPart.Weapon.Name}");
    }
}
