using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// provide Players
/// </summary>
public class PlayerProvider : MonoBehaviour {
    [SerializeField]
    //player prefab
    private PlayerCore PlayerPrefab;

    //player Dictionary 変更があるとイベントが発生する。
    private ReactiveDictionary<PlayerID, PlayerCore> _players = new ReactiveDictionary<PlayerID, PlayerCore>();

    /// <summary>
    /// get Current Player
    /// </summary>
    public IReadOnlyReactiveDictionary<PlayerID, PlayerCore> Players
    {
        get { return _players; }
    }

    /// <summary>
    /// Create player
    /// </summary>
    /// <param name="id">プレイヤーＩＤ</param>
    /// <param name="pos">初期ポジション</param>
    /// <param name="Respawnpos">リスポーンポジション</param>
    /// <returns></returns>
    public PlayerCore Create(PlayerID id, Vector3 pos, Vector3 Respawnpos)
    {
        //Instantiate Player prefab
        PlayerCore prefab = Instantiate(PlayerPrefab, pos , Quaternion.LookRotation(Vector3.back));

        var core = prefab.GetComponent<PlayerCore>();
        
        //initialization player
        core.Initialize(id, pos,this);
        _players.Add(id, core);
        return core;
    }

    public PlayerCore Create(PlayerID id)
    {

        var core = Create(id, Vector3.zero, Vector3.zero);

        return core;
    }

}
