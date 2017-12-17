using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    Player1 = 1
}

[SerializeField]
public struct PlayerParameters
{
    public float MoveSpeed;
    public float BounceRate;
}

/// <summary>
/// 死亡理由
/// </summary>
public struct DeadReason
{
    private PlayerID _deadPlayerId;
    private IAttackerInfo _attacker;

    /// <summary>
    /// 死亡したPlayerId
    /// </summary>
    public PlayerID DeadPlayerId
    {
        get { return _deadPlayerId; }
    }

    /// <summary>
    /// 死亡原因となった攻撃者情報
    /// </summary>
    public IAttackerInfo Attacker
    {
        get { return _attacker; }
    }

    public DeadReason(PlayerID deadPlayerId, IAttackerInfo attacker)
    {
        _deadPlayerId = deadPlayerId;
        _attacker = attacker;
    }
}