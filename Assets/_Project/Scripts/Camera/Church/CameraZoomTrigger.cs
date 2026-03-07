using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraZoomTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private Vector3 _outsideOffset = new Vector3(0, 18, -15);
    [SerializeField] private Vector3 _insideOffset = new Vector3(0, 10, -8);

    [SerializeField] private float _transitionDuration = 0.4f;

    private CinemachineTransposer _transposer;
    private Coroutine _currentTransition;

    void Awake()
    {
        if (_virtualCamera == null)
        {
            Debug.LogError($"{nameof(CameraZoomTrigger)}: _virtualCamera non assegnata sull'Inspector.", this);
            return;
        }

        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        if (_transposer == null)
        {
            Debug.LogWarning($"{nameof(CameraZoomTrigger)}: CinemachineVirtualCamera non usa un Transposer come Body. Controlla il componente della virtual camera.", this);
        }
    }

    private void Reset()
    {
        // Aiuta in editor: assicura che il collider sia un trigger quando si aggiunge lo script
        var col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (_transposer == null)
        {
            Debug.LogWarning($"{nameof(CameraZoomTrigger)}: transposer assente, impossibile aggiornare offset.", this);
            return;
        }

        StartOffsetTransition(_insideOffset);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (_transposer == null)
        {
            Debug.LogWarning($"{nameof(CameraZoomTrigger)}: transposer assente, impossibile aggiornare offset.", this);
            return;
        }

        StartOffsetTransition(_outsideOffset);
    }

    private void StartOffsetTransition(Vector3 targetOffset)
    {
        if (_currentTransition != null)
            StopCoroutine(_currentTransition);

        _currentTransition = StartCoroutine(TransitionOffsetCoroutine(targetOffset));
    }

    private IEnumerator TransitionOffsetCoroutine(Vector3 target)
    {
        Vector3 start = _transposer.m_FollowOffset;
        if (_transitionDuration <= 0f)
        {
            _transposer.m_FollowOffset = target;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < _transitionDuration)
        {
            elapsed += Time.deltaTime;
            _transposer.m_FollowOffset = Vector3.Lerp(start, target, Mathf.SmoothStep(0f, 1f, elapsed / _transitionDuration));
            yield return null;
        }

        _transposer.m_FollowOffset = target;
        _currentTransition = null;
    }
}