using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    [SerializeField] protected float _rotationSpeed = 10f;
    [SerializeField] protected float _stoppingDistance = 0.1f;

    protected NavMeshAgent _agent;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = _stoppingDistance;
        _agent.updateRotation = false;
    }
    protected virtual void Update()
    {
        RotateTowardsVelocity();
    }

    public virtual void MoveTo(Vector3 destination)
    {
        if (_agent.isOnNavMesh)
            _agent.SetDestination(destination);
    }

    protected void RotateTowardsVelocity()
    {
        if (_agent.velocity.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(_agent.velocity.normalized);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }
}

