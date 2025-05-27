using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public List<string> lineasDelDialogo;

    private bool yaActivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (yaActivado) return;
        if (other.CompareTag("Player"))
        {
            yaActivado = true;
            DialogueManager.Instance.StartDialogue(lineasDelDialogo, true);
        }
    }
}
