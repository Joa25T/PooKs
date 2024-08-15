using TMPro;
using UnityEngine;

namespace PooKs.UI
{
    public class ShipCrafter : Panel
    {
        [SerializeField] private TMP_InputField _name;
        [SerializeField] private PartAsigner _bodyAsigner;
        [SerializeField] private PartAsigner _weaponAsigner;
        
        public override void OnOpen(SO_PlayerCharacter interactingPC)
        {
            base.OnOpen(interactingPC);
        }

        public override void OnClose()
        {
            base.OnClose();
        }
        public void SaveShip()
        {
            if (_interactingPC == null) return;
            
            _interactingPC.SaveShip(_name.text, 
            (Part_Body)_bodyAsigner.AsignedPart(),
            (Part_Weapon)_weaponAsigner.AsignedPart());
        }
    }
}