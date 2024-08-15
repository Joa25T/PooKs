using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCharacterManager : MonoBehaviour
{
    [SerializeField]private List<SO_PlayerCharacter> _playerCharacters;
    //private PlayerInputManager _playerInputManager;
    private PlayerInput _playerInput;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _playerCharacters = new List<SO_PlayerCharacter>();
    }

    public void AddPC(SO_PlayerCharacter playerCharacter)
    {
        if (_playerCharacters.Count > 1)
        {
            _playerCharacters.Add(playerCharacter);
            return;
        }
        foreach (SO_PlayerCharacter player in _playerCharacters)
        {
            if (playerCharacter.PlayerID == player.PlayerID)return;
        }
        _playerCharacters.Add(playerCharacter);
    }

    public void RemovePC(PlayerInput playerInput)
    {
        _playerCharacters.RemoveAt(playerInput.playerIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "RunPrototype")
        {
            for (int i = 0; i < _playerCharacters.Count; i++)
            {
                Debug.Log($"ID : {_playerCharacters[i].PlayerID}");
                Debug.Log($"ShipName : {_playerCharacters[i].ShipName}");
                Debug.Log($"BodyName : {_playerCharacters[i].ShipBody.Name}");
                Debug.Log($"WeaponName : {_playerCharacters[i].ShipWeapon.Name}");
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
