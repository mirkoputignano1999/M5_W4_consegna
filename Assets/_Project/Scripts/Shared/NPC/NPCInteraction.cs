using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private NPCDialogue _dialogue;

    private bool _playerInside;

    void Update()
    {
        if (_playerInside && Input.GetKeyDown(KeyCode.A))
        {
            // Non aprire un nuovo dialogo se uno è già aperto.
            if (DialogueUI.Instance != null)
            {
                if (DialogueUI.Instance.IsOpen) return;

                // Evita riaperture immediate nello stesso frame / nella finestra breve dopo la chiusura
                if (Time.time - DialogueUI.Instance.LastClosedTime < 0.2f) return;
            }

            _dialogue.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _playerInside = true;
        InteractionUI.Instance.Show();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _playerInside = false;
        InteractionUI.Instance.Hide();
    }
}