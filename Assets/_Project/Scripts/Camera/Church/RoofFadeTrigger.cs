using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofFadeTrigger : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private float _fadeAlpha = 0.2f;

    private Material[] _materials;
    private Color[] _originalColors;

    void Start()
    {
        _materials = new Material[_renderers.Length];
        _originalColors = new Color[_renderers.Length];

        for (int i = 0; i < _renderers.Length; i++)
        {
            _materials[i] = _renderers[i].material;
            _originalColors[i] = _materials[i].color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        SetAlpha(_fadeAlpha);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        SetAlpha(1f);
    }

    private void SetAlpha(float alpha)
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            Color c = _materials[i].color;
            c.a = alpha;
            _materials[i].color = c;
        }
    }
}