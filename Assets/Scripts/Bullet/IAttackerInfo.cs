using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AttackerInfo Interface
public interface IAttackerInfo {

}

/// <summary>
/// Attack from Player
/// </summary>
public struct SPlayerAttacker:IAttackerInfo
{
    //playerID param
    public PlayerID PlayerId{get;private set;}

    //Constructer: set PlayerID
    public SPlayerAttacker(PlayerID in_playerid) :this()//?????????????
    {
        PlayerId = in_playerid;
    }
}


/// <summary>
/// Attack from Others
/// </summary>
public struct SNonPlayerAttacker : IAttackerInfo
{
    //????????????
    public static SNonPlayerAttacker Default = new SNonPlayerAttacker();
}