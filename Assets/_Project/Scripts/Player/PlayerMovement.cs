using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NavMeshMover
{
    [SerializeField] private LayerMask _navMeshLayer;
    [SerializeField] private Camera _mainCamera;

    protected override void Awake()
    {
        base.Awake();

        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        HandleMouseInput();

    }

    private void HandleMouseInput()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _navMeshLayer))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1f, NavMesh.AllAreas))
            {
                MoveTo(navHit.position);
            }
        }


    }
}

