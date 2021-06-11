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


    public GameObject telop;
    public Text telopText;
    // Start is called before the first frame update
    void Start()
    {
        telop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false)
        {
            switch (telop_flag)
            {//0=none、1=bounus、2=minus、3=Move、4=Warp、5=Random、6= Gamble（強奪）、7=破壊、8=gamble
                case 0:
                    telop.SetActive(false);
                    break;
                case 1:
                    telopText.text = diceButton.Dice_Effect + "ページ獲得！";
                    DOVirtual.DelayedCall(3f, () => {
                        //telop.SetActive(false);
                        telop_flag = 0;
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
                    });
                    break;
                case 2:
                    telopText.text = diceButton.Dice_Effect + "ページ消失！";
                    DOVirtual.DelayedCall(3f, () => {
                        //telop.SetActive(false);
                        telop_flag = 0;
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
                    });
                    break;
                case 3:
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
