using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainruleScript : MonoBehaviour
{
    public DiceButton diceButton;

    public Text title;//説明
    public Text contents;
    public Text page_numberText;
    public int now_page = 1;

    public GameObject Mainrule_object;
    public GameObject Icon;
    public GameObject Skil_all;
    public GameObject[] mass_icon;
    // Start is called before the first frame update
    void Start()
    {
        mass_icon[0].SetActive(false);
        mass_icon[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (now_page == 0)
        {
            now_page = 8;
        }
        else if (now_page == 9)
        {
            now_page = 1;
        }


        switch (now_page)
        {
            case 8:
                page_numberText.text = now_page + "/8ページ";
                title.text = "これまでの経緯";
                contents.text = "目が覚めたら本の迷宮世界にいた\r\n２つのサイコロを振り\r\n同じく迷い込んだ他者を出し抜きながら\r\n脱出に必要な「現し世のページ」を集めよう!\r\nいち早く「現世の本」を完成させたものだけが\r\n現実世界へと帰ることが出来る.";
                break;
            case 1:
                page_numberText.text = now_page + "/8ページ";
                title.text = "遊び方1";
                contents.text = "２つのサイコロを振り\r\n出た目の好きな方を１つ選択し、移動をする\r\n\r\n止まったマスの効果は誰かが\r\n踏まない限り分からない。";
                break;
            case 2:
                page_numberText.text = now_page + "/8ページ";
                title.text = "遊び方2";
                contents.text = "スタートを通り過ぎるような出目でも\r\nスタートには止まることが出来る。\r\n\r\n「現し世のページ」を100枚使って本を作成し\r\n最初にスタート地点へ戻るとクリア!";
                break;
            case 3:
                page_numberText.text = now_page + "/8ページ";
                mass_icon[0].SetActive(false);
                title.text = "遊び方3";
                contents.text = "\r\nプレイヤーは「現し世のページ」を\r\n\r\n集めると有利なスキル各ターン1度発動できる！";
                break;
            case 4:
                page_numberText.text = now_page + "/8ページ";
                mass_icon[0].SetActive(true);
                mass_icon[1].SetActive(false);
                title.text = "マス効果1";
                contents.text = "         もう一度ダイスを振る\r\n\r\n         ダイス目×２ページ獲得\r\n\r\n         ダイス目×２ページ失う";
                break;
            case 5:
                page_numberText.text = now_page + "/8ページ";
                mass_icon[0].SetActive(false);
                mass_icon[1].SetActive(true);
                title.text = "マス効果2";
                contents.text = "         ランダムワープ\r\n\r\n         効果なし\r\n\r\n         いずれかの効果";
                break;
            case 6:
                page_numberText.text = now_page + "/8ページ";
                mass_icon[1].SetActive(false);
                title.text = "その他のページの獲得について";
                contents.text = "ターン開始時に1枚ページが配られる。\r\n\r\n3ターン毎に配られるページが\r\n\r\n1枚ずつ増える。";
                break;
            case 7:
                page_numberText.text = now_page + "/8ページ";
                title.text = "スキル発動の補足";
                contents.text = "\r\nノルマ:スキルを使用するのに必要なページ数\r\n\r\nコスト:スキルを発動すると失うページ数";
                break;
        }
    }
    public void Next_number()
    {
        now_page += 1;
        if (now_page == 9)
            now_page = 1;
    }

    public void Back_number()
    {
        now_page -= 1;

        //now_page = 1;
    }
    public void Close_Rule()
    {
        now_page = 1;
        mass_icon[0].SetActive(false);
        mass_icon[1].SetActive(false);
        Mainrule_object.SetActive(false);
        Icon.SetActive(true);
        diceButton.rule_flag = false;
        Skil_all.SetActive(true);
    }

    public void Open_Rule()
    {
        
        Mainrule_object.SetActive(true);
        Icon.SetActive(false);
        diceButton.rule_flag = true;
        Skil_all.SetActive(false);
    }
}
