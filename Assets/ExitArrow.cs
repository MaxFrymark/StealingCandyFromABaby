using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArrow : MonoBehaviour
{
    [SerializeField] SceneLoader loader;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        loader.LoadNextLevel();
    }
}
