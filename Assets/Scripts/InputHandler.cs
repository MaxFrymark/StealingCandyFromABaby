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
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += MovePlayer;
        inputActions.Player.Move.canceled += StopPlayer;
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
}
