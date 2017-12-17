using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public struct DamageInfo {

    public IAttackerInfo Attacker;
    public int Value;
    public float Direction;
    public AttackType Type;
}

public enum AttackType
{
    Ground,
    EnemyAttack,
    Bullet,
    Others
}