using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NavMeshAgentFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _refreshInterval = 0.5f;
    [SerializeField] private float _updateThreshold = 0.25f;

    // Usa il tipo completo per evitare shadowing con il nome della classe
    private UnityEngine.AI.NavMeshAgent _agent;
    private Coroutine _refreshCoroutine;
    private Vector3 _lastTargetPosition;

    private void Awake()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void OnEnable()
    {
        if (_target != null && _agent != null && _agent.isOnNavMesh)
        {
            _lastTargetPosition = _target.position;
            _agent.SetDestination(_lastTargetPosition);
            _refreshCoroutine = StartCoroutine(RefreshPathRoutine());
        }
    }

    private void OnDisable()
    {
        if (_refreshCoroutine != null)
            StopCoroutine(_refreshCoroutine);
    }

    private IEnumerator RefreshPathRoutine()
    {
        var wait = new WaitForSeconds(_refreshInterval);
        while (true)
        {
            yield return wait;

            if (_target == null || _agent == null)
                continue;

            if (!_agent.isOnNavMesh)
                continue;

            Vector3 currentTargetPos = _target.position;
            // aggiornamento solo se il target si è spostato significativamente
            if ((currentTargetPos - _lastTargetPosition).sqrMagnitude > (_updateThreshold * _updateThreshold))
            {
                _agent.SetDestination(currentTargetPos);
                _lastTargetPosition = currentTargetPos;
            }
        }
    }

    // Metodo pubblico comodo per cambiare target a runtime
    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;

        if (_target != null && _agent != null && _agent.isOnNavMesh)
        {
            _lastTargetPosition = _target.position;
            _agent.SetDestination(_lastTargetPosition);
            if (_refreshCoroutine == null)
                _refreshCoroutine = StartCoroutine(RefreshPathRoutine());
        }
    }
}
