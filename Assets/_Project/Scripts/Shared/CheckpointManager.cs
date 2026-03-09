using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _checkpoint;

    private NavMeshAgent _agent;

    void Awake()
    {
        Instance = this;
        _agent = _player.GetComponent<NavMeshAgent>();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        yield return ScreenFader.Instance.FadeOut();

        _agent.ResetPath();
        _agent.Warp(_checkpoint.position);

        yield return new WaitForSeconds(0.3f);

        yield return ScreenFader.Instance.FadeIn();
    }
}