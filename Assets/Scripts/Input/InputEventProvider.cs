using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class InputEventProvider : BaseComponent, IInputEventProvider{

    private ReactiveProperty<bool> _shoot = new ReactiveProperty<bool>();
    private ReactiveProperty<Vector3> _movedir = new ReactiveProperty<Vector3>();

    //発射ボタン押したときにイベントが走る
    public IReadOnlyReactiveProperty<bool> ShootButtonActivate { get { return _shoot; } }
    //向き入力したときにイベントが走る
    public IReadOnlyReactiveProperty<Vector3> MoveVector { get { return _movedir; } }

    protected override void OnInitialize()
    {
        //いまいちしっくりこないけどうごいた//////////////////////////////////////////////
        //updateのたびに呼ばれる
        this.UpdateAsObservable()
                        .Select(_ => Input.GetKey(KeyCode.B))   //_をキーboolに変換
                        .DistinctUntilChanged()                 //値が変化したときのみよびだす
                        .Subscribe(x => _shoot.Value = x);      //キー情報を_shootvalueにいれる

        //updateのたびに呼ばれる
        this.UpdateAsObservable()
                        .Select(_ => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")))   //
                        .Subscribe(x => _movedir.SetValueAndForceNotify(x));      //キー情報を_shootvalueにいれる
    }
}
