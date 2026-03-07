using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyState
{
    protected EnemyController _enemy;

    public BaseEnemyState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public abstract void Update();
}
