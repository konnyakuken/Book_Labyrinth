using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UniRx;
//ダイスに関わるスクリプト
public class DiceRollObserver : MonoBehaviour
{
    public DiceRollObserver instance;
    public int Dice_result;
    //public Subject<int> OnDiceRolledObservable = new Subject<int>();

    public void RollDice()
    {
        // 今回は１〜６の目が出るダイス
        //this.OnDiceRolledObservable.OnNext(Random.Range(1, 7));
        Dice_result = Random.Range(1, 7);
        //return Dice_result;
    }
}
