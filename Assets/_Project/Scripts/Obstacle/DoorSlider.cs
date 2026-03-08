using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorSlider : MonoBehaviour
{
    [SerializeField] private Vector3 _openOffset = new Vector3(-2f, 0, 0);
    [SerializeField] private float _speed = 2f;

    private Vector3 _closedPos;
    private Vector3 _openPos;

    void Start()
    {
        _closedPos = transform.position;
        _openPos = _closedPos + _openOffset;
    }

    public void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(OpenRoutine());
    }

    IEnumerator OpenRoutine()
    {
        while (Vector3.Distance(transform.position, _openPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _openPos,
                _speed * Time.deltaTime
            );

            yield return null;
        }
    }
}