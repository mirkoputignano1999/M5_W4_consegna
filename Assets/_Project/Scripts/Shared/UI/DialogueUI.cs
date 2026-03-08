using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _text;

    private bool _dialogueActive;
    public bool IsOpen => _dialogueActive;

    public float LastClosedTime { get; private set; } = -10f;

    void Awake()
    {
        Instance = this;
        _panel.SetActive(false);
    }

    void Update()
    {
        if (_dialogueActive && Input.GetKeyDown(KeyCode.A))
        {
            CloseDialogue();
        }
    }

    public void ShowDialogue(string text)
    {
        _text.text = text;
        _panel.SetActive(true);
        _dialogueActive = true;
    }

    public void CloseDialogue()
    {
        _panel.SetActive(false);
        _dialogueActive = false;
        LastClosedTime = Time.time;
    }
}