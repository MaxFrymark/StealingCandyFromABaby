using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] MenuInputHandler menuInput;
    [SerializeField] GameObject messages;

    private void OnEnable()
    {
        messages.SetActive(false);
        inputHandler.gameObject.SetActive(false);
        menuInput.gameObject.SetActive(true);
    }
}
