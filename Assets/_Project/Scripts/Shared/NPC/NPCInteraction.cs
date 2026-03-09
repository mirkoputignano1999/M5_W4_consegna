using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public static NPCInteraction CurrentNPC;

    [SerializeField] private NPCDialogue _dialogue;

    private bool _playerInside;

    void Update()
    {
        if (CurrentNPC != this) return;

        if (_playerInside && Input.GetKeyDown(KeyCode.A))
        {
            if (DialogueUI.Instance != null)
            {
                if (DialogueUI.Instance.IsOpen) return;

                if (Time.time - DialogueUI.Instance.LastClosedTime < 0.2f) return;
            }

            _dialogue.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _playerInside = true;
        CurrentNPC = this;

        InteractionUI.Instance.Show();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _playerInside = false;

        if (CurrentNPC == this)
            CurrentNPC = null;

        InteractionUI.Instance.Hide();
    }
}