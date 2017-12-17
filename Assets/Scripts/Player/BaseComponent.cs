using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class BaseComponent : MonoBehaviour {

    protected Core Core;

    //インプットインターフェース
    private IInputEventProvider _inputEventProvider;
    protected IInputEventProvider InputEventProvider { get { return _inputEventProvider; } }

    protected void Start()
    {
        Core = GetComponent<Core>();

        //インプットインターフェースを格納
        _inputEventProvider = GetComponent<IInputEventProvider>();

        //Coreで定義したOnInitializeAsyncが完了したイベントが発生したらこっちのInitializeを走らせる
        Core.OnInitializeAsync
            .Subscribe(_ => OnInitialize());

        OnStart();
    }

    //仮想関数　機能を持つことができる。
    protected virtual void OnStart() { }

    //抽象関数は、機能を持つことができない
    protected abstract void OnInitialize();//virtualとabstract????
}
