using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : NavMeshMover
{
    public void Chase(Transform target)
    {
        MoveTo(target.position);
    }
}

