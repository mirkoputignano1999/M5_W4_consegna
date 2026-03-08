using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : BaseEnemyState
{
    public EnemyReturnState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        _enemy.MoveTo(_enemy.StartPosition);
    }

    public override void Update()
    {
        if (_enemy.CanSeePlayer())
        {
            _enemy.ChangeState(new EnemyChaseState(_enemy));
            return;
        }

        if (_enemy.ReachedDestination())
        {
            _enemy.ChangeState(new EnemyPatrolState(_enemy));
        }
    }
}