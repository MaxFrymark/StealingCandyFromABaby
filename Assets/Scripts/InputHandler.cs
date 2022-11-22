using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Player player;

    
    PlayerInputActions inputActions;

    private void Start()
    {
        inputActions = new PlayerInputActions();
        
        inputActions.Player.Move.performed += MovePlayer;
        inputActions.Player.Move.canceled += StopPlayer;
        inputActions.Player.Interact.performed += PlayerInteract;
        inputActions.Player.SelectItem.performed += SelectItem;
        inputActions.Player.UseItem.performed += UseItem; inputActions.Player.Enable();
        inputActions.SceneControls.Disable();
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        switch (context.action.GetBindingIndexForControl(context.control))
        {
            case 0:
                player.Move(-1);
                break;
            case 1:
                player.Move(1);
                break;
        }
    }

    private void StopPlayer(InputAction.CallbackContext context)
    {
        player.Stop();
    }

    private void PlayerInteract(InputAction.CallbackContext context)
    {
        player.Interact();
    }

    private void SelectItem(InputAction.CallbackContext context)
    {
        player.SelectItem(context.action.GetBindingIndexForControl(context.control));
    }

    private void UseItem(InputAction.CallbackContext context)
    {
        player.UseActiveItem();
    }

    

    public void SwitchToMenuControls()
    {
        inputActions.Player.Disable();
        inputActions.SceneControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
