using JetBrains.Annotations;
using UnityEngine;

public interface IInteractable
{
    public void OnInteract(float dir, SO_PlayerCharacter playerCharacter, Transform playerPos);
}