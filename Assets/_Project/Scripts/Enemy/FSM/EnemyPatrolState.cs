using System.Collections;
using System.Collections.Generic;
public class EnemyPatrolState : BaseEnemyState
{
    public EnemyPatrolState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        _enemy.MoveToNextWaypoint();
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
            _enemy.NextWaypoint();
            _enemy.MoveToNextWaypoint();
        }
    }
}