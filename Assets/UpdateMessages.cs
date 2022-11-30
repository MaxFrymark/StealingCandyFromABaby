using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMessages : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void ChangeMessage(string newMessage)
    {
        text.text = newMessage;
    }
}
