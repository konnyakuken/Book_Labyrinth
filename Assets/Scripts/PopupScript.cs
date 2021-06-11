using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる

public class PopupScript : MonoBehaviour
{
    public TurnManager turnManager;
    public SkillScript skillScript;
    public DiceButton diceButton;

    //0=none、1=bounus、2=minus、3=Move、4=Warp、5=Random、6= Gamble（強奪）、7=破壊、8=gamble
    public int telop_flag = 0;
    public bool end_flag = false;//DOtween内にターン終了処理を入れるとおかしくなった為

    public GameObject telop;
    public Text telopText;
    // Start is called before the first frame update
    void Start()
    {
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 800, sequencesCapacity: 200);//キャパシティの増加
        telop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ( turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().effect_flag == true)//turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false&&
        {
            switch (telop_flag)
            {//0=none、1=bounus、2=minus、3=Move、4=Warp、5=Random、6= Gamble（強奪）、7=破壊、8=gamble
                case 0:
                    
                    break;
                case 1:
                    telop.SetActive(true);
                    telopText.text = diceButton.Dice_Effect + "ページ獲得！";
                    telop_flag = 0;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().effect_flag = false;
                        Debug.Log("end!!");
                        telop.SetActive(false);
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
                        //end_flag = true;

                    });

                    break;
                case 2:
                    telop.SetActive(true);
                    telopText.text = diceButton.Dice_Effect + "ページ消失！";
                    telop_flag = 0;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
                    });
                    break;
                case 3:
                    telop.SetActive(true);
                    telopText.text ="もう一度ダイスを振る!";
                    telop_flag = 0;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);
                        
                    });
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
            }
        }

        if (end_flag == true)
        {
            end_flag = false;
            turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
        }
    }

    /*
    public IEnumerator DiceEffect_stop()
    {
        //ここに処理を書く
        telop.SetActive(true);
        if (telop_flag == 1)
            warning_text.text = "必要ページ数が不足しています";
        else if (telop_flag == 2)
            warning_text.text = "残り回数が不足しています";

        skil_on.interactable = false;
        next.interactable = false;
        back.interactable = false;
        //1フレーム停止
        yield return new WaitForSeconds(3f);

        //ここに再開後の処理を書く
        warning.SetActive(false);
        skil_on.interactable = true;
        next.interactable = true;
        back.interactable = true;
    }*/
}
