using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestDialogue : NPCDialogue
{
    [SerializeField] private DoorSlider _door;

    public override void StartDialogue()
    {
        base.StartDialogue();
        _door.OpenDoor();
    }
}