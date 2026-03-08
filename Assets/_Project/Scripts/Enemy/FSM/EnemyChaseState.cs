using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : BaseEnemyState
{
    public EnemyChaseState(EnemyController enemy) : base(enemy) { }

    public override void Update()
    {
        if (_enemy.CanSeePlayer())
        {
            _enemy.ChasePlayer();
        }
        else
        {
            _enemy.ChangeState(new EnemyReturnState(_enemy));
        }
    }
}