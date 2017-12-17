using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ScoreManager : MonoBehaviour {

    private ReactiveCollection<DeadReason> deadReasons
        = new ReactiveCollection<DeadReason>();

    public void SetResist( EnemyCore in_core )
    {
        in_core.OnDied
            .Subscribe(d => deadReasons.Add(d))
            .AddTo(gameObject);
    }

    /////////////とちゅう
}
