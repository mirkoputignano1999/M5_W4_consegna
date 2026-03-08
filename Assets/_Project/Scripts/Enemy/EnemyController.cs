using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : NavMeshMover
{
    private BaseEnemyState _currentState;

    [SerializeField] private Transform _player;

    [SerializeField] private float _viewDistance = 10f;
    [SerializeField] private float _viewAngle = 60f;
    //[SerializeField] private float _chaseRange = 10f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField] private Transform[] _waypoints;
    private int _currentWaypoint;

    private Vector3 _startPosition;

    protected override void Awake()
    {
        base.Awake();
        _startPosition = transform.position;

        if (_waypoints != null && _waypoints.Length > 0)
            ChangeState(new EnemyPatrolState(this));
        else
            ChangeState(new EnemyIdleState(this));
    }
    protected override void Update()
    {
        base.Update();
        _currentState?.Update();
    }

    public void ChangeState(BaseEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public bool CanSeePlayer()
    {
        Vector3 dir = (_player.position - transform.position);

        if (dir.magnitude > _viewDistance)
            return false;

        dir.Normalize();

        float dot = Vector3.Dot(transform.forward, dir);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (angle > _viewAngle * 0.5f)
            return false;

        if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hit, _viewDistance, _obstacleMask))
        {
            if (hit.transform != _player)
                return false;
        }

        return true;
    }

    public void MoveToNextWaypoint()
    {
        if (_waypoints.Length == 0)
            return;

        MoveTo(_waypoints[_currentWaypoint].position);
    }

    public bool ReachedDestination()
    {
        return !_agent.pathPending && _agent.remainingDistance < 0.3f;
    }

    public void NextWaypoint()
    {
        if (_waypoints == null || _waypoints.Length == 0)
            return;

        _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
    }

    public void ChasePlayer()
    {
        MoveTo(_player.position);
    }

    public Vector3 StartPosition => _startPosition;


//public bool PlayerInRange()
//{
//    return Vector3.Distance(transform.position, _player.position) <= _chaseRange;
//}

void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewDistance);

        Vector3 left = Quaternion.Euler(0, -_viewAngle / 2, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, _viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + left * _viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + right * _viewDistance);
    }

    public bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, _player.position) <= _attackRange;
    }
}
