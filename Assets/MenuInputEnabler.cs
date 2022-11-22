using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputEnabler : MonoBehaviour
{
    [SerializeField] MenuInputHandler menuInput;

    void Start()
    {
        menuInput = FindObjectOfType<MenuInputHandler>(true);
        menuInput.gameObject.SetActive(true);
    }

}
