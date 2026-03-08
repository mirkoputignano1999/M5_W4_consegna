using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [SerializeField] private GameObject _prompt;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show()
    {
        _prompt.SetActive(true);
    }

    public void Hide()
    {
        _prompt.SetActive(false);
    }
}