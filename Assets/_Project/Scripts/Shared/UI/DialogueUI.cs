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
    private float LastOpenedTime { get; set; } = -10f;

    // Tempo in secondi durante il quale ignoriamo il tasto di chiusura subito dopo l'apertura
    [SerializeField] private float _ignoreCloseBuffer = 0.05f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"[DialogueUI] Duplicate instance trovata su '{gameObject.name}'. Distruggo l'istanza duplicata.");
            Destroy(gameObject);
            return;
        }

        Debug.Log($"[DialogueUI] Awake su '{gameObject.name}'. GameObject activeInHierarchy={gameObject.activeInHierarchy}, component enabled={enabled}");

        if (_panel == null)
            Debug.LogError("[DialogueUI] _panel non assegnato nell'Inspector.");

        if (_text == null)
            Debug.LogError("[DialogueUI] _text non assegnato nell'Inspector.");

        if (_panel != null)
            _panel.SetActive(false);
    }

    void Update()
    {
        // Ignora il tasto di chiusura se siamo troppo vicini all'apertura
        if (_dialogueActive && Time.time - LastOpenedTime >= _ignoreCloseBuffer && Input.GetKeyDown(KeyCode.A))
        {
            CloseDialogue();
        }
    }

    public void ShowDialogue(string text)
    {
        if (_text == null || _panel == null)
        {
            Debug.LogError("[DialogueUI] ShowDialogue chiamato ma _text/_panel non assegnati.");
            return;
        }

        _text.text = text ?? "";
        _panel.SetActive(true);
        _dialogueActive = true;
        LastOpenedTime = Time.time;

        Debug.Log($"[DialogueUI] ShowDialogue chiamato su '{gameObject.name}'. testo len={_text.text?.Length ?? 0}; panel.activeSelf={_panel.activeSelf}");
    }

    public void CloseDialogue()
    {
        if (_panel != null)
            _panel.SetActive(false);

        _dialogueActive = false;
        LastClosedTime = Time.time;
        Debug.Log("[DialogueUI] CloseDialogue chiamato. LastClosedTime = " + LastClosedTime);
    }
}