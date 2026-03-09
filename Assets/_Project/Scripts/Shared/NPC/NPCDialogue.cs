using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string _dialogueText;

    public virtual void StartDialogue()
    {
        DialogueUI.Instance.ShowDialogue(_dialogueText);

        InteractionUI.Instance.Hide();
    }
}


