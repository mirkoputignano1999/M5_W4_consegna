using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : BaseEnemyState
{
    public EnemyIdleState(EnemyController enemy) : base(enemy) {}

    public override void Update()
    {
        if (_enemy.CanSeePlayer())
        {
            _enemy.ChangeState(new EnemyChaseState(_enemy));
        }
    }
}
