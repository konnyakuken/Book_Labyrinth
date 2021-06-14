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
    public bool skil_flag = false;

    public GameObject telop;
    public Button Move_Bottun;

    public Text telopText;

    public int animation_flag = 0;//0=none、1=bounus、2=minus
    public bool move_telopFlag = false;//move後NPCがすぐにボーナスマスなどに踏むと消えるため
    // Start is called before the first frame update
    void Start()
    {
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 800, sequencesCapacity: 200);//キャパシティの増加
        telop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ( turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().effect_flag == true|| skil_flag == true)//turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false&&
        {
            switch (telop_flag)
            {//0=none、1=bounus、2=minus、3=Move、4=Warp、5=Random、6= Gamble（強奪）、7=破壊、8=gamble
                case 0:
                    
                    break;
                case 1:
                    telop.SetActive(true);
                    telopText.text = diceButton.Dice_Effect + "ページ獲得！";
                    StartCoroutine("Delay_move");
                    animation_flag = 1;
                    telop_flag = 0;
                    move_telopFlag = true;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().effect_flag = false;
                        Debug.Log("end!!");
                        telop.SetActive(false);
                        move_telopFlag = false;
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
                        //end_flag = true;

                    });

                    break;
                case 2:
                    telop.SetActive(true);
                    telopText.text = diceButton.Dice_Effect + "ページ消失！";
                    StartCoroutine("Delay_move");
                    animation_flag = 2;
                    telop_flag = 0;
                    move_telopFlag = true;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);
                        move_telopFlag = false;
                        turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
                    });
                    break;
                case 3:
                    telop.SetActive(true);
                    telopText.text ="Moveマス!";
                    StartCoroutine("Delay_move");
                    telop_flag = 0;
                    DOVirtual.DelayedCall(1.5f, () => {
                        if(move_telopFlag == false)
                            telop.SetActive(false);
                        
                    });
                    break;
                case 4:
                    telop.SetActive(true);
                    telopText.text = "ワープ!";
                    StartCoroutine("Delay_move");
                    telop_flag = 0;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);

                    });
                    break;
                case 5:
                    telop.SetActive(true);
                    telopText.text = "ランダム効果!";
                    telop_flag = 0;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);

                    });
                    break;
                case 6:
                    telop.SetActive(true);
                    telopText.text = (skillScript.select_player + 1).ToString() + "Pは" + skillScript.dice_result.ToString() + "枚奪われた!";
                    StartCoroutine("Delay_move");
                    telop_flag = 0;
                    skil_flag = false;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);

                    });
                    break;
                case 7:
                    telop.SetActive(true);
                    telopText.text = (skillScript.random_player + 1).ToString() + "Pは"+ skillScript.dice_result.ToString()+"枚失った!";
                    StartCoroutine("Delay_move");
                    telop_flag = 0;
                    skil_flag = false;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);

                    });
                    break;
                case 8:
                    telop.SetActive(true);
                    telopText.text = (skillScript.random_player+1).ToString()+"P 1ターン休み!";
                    StartCoroutine("Delay_move");
                    telop_flag = 0;
                    skil_flag = false;
                    DOVirtual.DelayedCall(1.5f, () => {
                        telop.SetActive(false);

                    });
                    break;
            }
        }

        if (end_flag == true)
        {
            end_flag = false;
            turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
        }
    }

    public IEnumerator Delay_move()
    {

        Move_Bottun.interactable = false;
        //1フレーム停止
        yield return new WaitForSeconds(1.5f);

        //ここに再開後の処理を書く

        Move_Bottun.interactable = true;
    }
}
