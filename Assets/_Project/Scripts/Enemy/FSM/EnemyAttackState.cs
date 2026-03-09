using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : BaseEnemyState
{
    public EnemyAttackState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("Enemy Attacking");

        CheckpointManager.Instance.RespawnPlayer();
    }

    public override void Update()
    {
        if (!_enemy.PlayerInAttackRange())
        {
            _enemy.ChangeState(new EnemyChaseState(_enemy));
        }
    }
}
