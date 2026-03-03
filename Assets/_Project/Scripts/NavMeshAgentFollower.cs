using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentFollower : MonoBehaviour
{

    [SerializeField] private Transform _target;

    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(RefreshPath());

    }

    IEnumerator RefreshPath()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return wfs;
            _agent.SetDestination(_target.position);
        }
    }

   
}
