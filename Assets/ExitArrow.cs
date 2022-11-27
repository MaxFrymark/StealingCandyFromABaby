using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArrow : MonoBehaviour
{
    [SerializeField] SceneLoader loader;
    [SerializeField] GameObject goodJob;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        goodJob.SetActive(true);
        collision.GetComponent<Player>().SetHasAlreadyWon();
        loader.LoadNextLevel();
    }
}
