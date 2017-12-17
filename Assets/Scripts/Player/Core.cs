using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class Core : MonoBehaviour {

    /// <summary>
    /// 初期化を実行を開始するイベント通知
    /// </summary>
    public IObservable<int> OnInitializeAsync { get { return _onInitializeAsyncSubject; } }
    protected readonly AsyncSubject<int> _onInitializeAsyncSubject = new AsyncSubject<int>();

    //AsyncSubjectとはなんぞや
}
