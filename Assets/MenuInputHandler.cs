using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputHandler : MonoBehaviour
{
    [SerializeField] SceneLoader loader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loader.LoadNewScene();
        }
    }
}
