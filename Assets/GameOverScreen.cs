using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] MenuInputHandler menuInput;

    private void OnEnable()
    {
        inputHandler.gameObject.SetActive(false);
        menuInput.gameObject.SetActive(true);
    }
}
