using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ruleScript : MonoBehaviour
{
    public titleScript titlescript;
    public Text title;//説明
    public Text contents;
    public Text page_numberText;
    public int now_page = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (now_page == 0)
        {
            now_page = 7;
        }
        else if (now_page == 8)
        {
            now_page = 1;
        }


        switch (now_page)
        {
            case 1:
                page_numberText.text = now_page + "/7ページ";
                title.text = "概要" ;
                contents.text = "目が覚めたら本の迷宮世界にいた\r\n２つのサイコロを振り\r\n同じく迷い込んだ他者を出し抜きながら\r\n脱出に必要な「現し世のページ」を集めよう!\r\nいち早く「現世の本」を完成させたものだけが\r\n現実世界へと帰ることが出来る.";
                break;
            case 2:
                page_numberText.text = now_page + "/7ページ";
                title.text = "遊び方1" ;
                contents.text = "２つのサイコロを振り\r\n出た目の好きな方を１つ選択し、移動をする\r\n\r\n止まったマスの効果は誰かが\r\n踏まない限り分からない。";
                break;
            case 3:
                page_numberText.text = now_page + "/7ページ";
                title.text = "遊び方2";
                contents.text = "スタートを通り過ぎるような出目でも\r\nスタートには止まることが出来る。\r\n\r\n「現し世のページ」を100枚使って本を作成し\r\n最初にスタート地点へ戻るとクリア!";
                break;
            case 4:
                page_numberText.text = now_page + "/7ページ";
                title.text = "遊び方3";
                contents.text = "\r\n\r\nプレイヤーは「現し世のページ」を\r\n集めると有利なスキル各ターン1度発動できる！";
                break;
            case 5:
                page_numberText.text = now_page + "/7ページ";
                title.text = "マス効果1" ;
                contents.text = "^ もう一度ダイスを振る\r\n+ ダイス目×２ページ獲得\r\n- ダイス目×２ページ失う";
                break;
            case 6:
                page_numberText.text = now_page + "/7ページ";
                title.text = "マス効果2";
                contents.text = "w　　ランダムワープ\r\n 　　効果なし\r\n?　　いずれかの効果";
                break;
            case 7:
                page_numberText.text = now_page + "/7ページ";
                title.text = "その他のページの獲得について";
                contents.text = "\r\nターン開始時に1枚ページが配られる。\r\n3ターン毎に配られるページが\r\n1枚ずつ増える";
                break;
        }
    }
    public void Next_number()
    {
        now_page += 1;
        if (now_page == 8)
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
        titlescript.rule_object.SetActive(false);
        titlescript.title_object.SetActive(true);
    }
}
