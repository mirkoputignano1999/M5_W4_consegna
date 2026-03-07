using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : BaseEnemyState
{
    public EnemyChaseState(EnemyController enemy) : base(enemy) { }

    public override void Update()
    {
        _enemy.ChasePlayer();

        if (!_enemy.CanSeePlayer())
        {
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }
}