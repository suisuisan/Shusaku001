using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyProvider : MonoBehaviour {
    [SerializeField]
    //player prefab
    private EnemyCore EnemyPrefab;

    private float posy = 17.4f;

    [SerializeField]
    //敵の数これで敵が0になったときにイベントが発生するはず！不要
    private IntReactiveProperty _enemyCount = new IntReactiveProperty(30);
    public IReadOnlyReactiveProperty<int> enemyCount { get { return _enemyCount; } }

    //player Dictionary 変更があるとイベントが発生する。
    private ReactiveDictionary<int, EnemyCore> _enemies = new ReactiveDictionary<int, EnemyCore>();

    /// <summary>
    /// get Current Player
    /// </summary>
    public IReadOnlyReactiveDictionary<int, EnemyCore> Players
    {
        get { return _enemies; }
    }

    //敵出現コルーチンのスタート関数
    public void StartEnemyStartEnemySequence()
    {
        StartCoroutine(StartEnemyCoroutine()) ;
    }

    //1sおきに出現関数
    IEnumerator StartEnemyCoroutine()
    {
        /////////////////////////////////////ゲームがおわっても出現しっぱなしになってしまいました
        int i = enemyCount.Value;
        while (i > 0)
        {
            var x = Random.Range(-10f,10f);
            var pos = new Vector3(x, posy,0 );
            Create(i, pos, pos);
            i--;
            _enemyCount.Value = i;
            yield return new WaitForSeconds(1.0f);
        }
        

    }

    /// <summary>
    /// Create player
    /// </summary>
    /// <param name="id">プレイヤーＩＤ</param>
    /// <param name="pos">初期ポジション</param>
    /// <param name="Respawnpos">リスポーンポジション</param>
    /// <returns></returns>
    EnemyCore Create(int id, Vector3 pos, Vector3 Respawnpos)
    {
        //Instantiate Player prefab
        EnemyCore prefab = Instantiate(EnemyPrefab, pos, Quaternion.LookRotation(Vector3.back));

        var core = prefab.GetComponent<EnemyCore>();
        //initialization player
        core.Initialize(id, pos, this);
        _enemies.Add(id, core);
        return core;
    }
    EnemyCore Create(int id)
    {
        var core = Create(id, Vector3.zero, Vector3.zero);

        return core;
    }
}
