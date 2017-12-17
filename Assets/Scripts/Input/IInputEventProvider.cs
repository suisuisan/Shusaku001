using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface IInputEventProvider{
    //発射ボタン押したときにイベントが走る
    IReadOnlyReactiveProperty<bool> ShootButtonActivate { get; }
    //向き入力したときにイベントが走る
    IReadOnlyReactiveProperty<Vector3> MoveVector { get; }
}
