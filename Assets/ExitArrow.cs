using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArrow : MonoBehaviour
{
    [SerializeField] SceneLoader loader;
    [SerializeField] GameObject goodJob;
    [SerializeField] GameObject messages;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        messages.SetActive(false);
        goodJob.SetActive(true);
        collision.GetComponent<Player>().SetHasAlreadyWon();
        loader.LoadNextLevel();
    }
}
