using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillScript : MonoBehaviour
{
    public GameObject Skil_managerCanvas;

    public Coroutine _someCoroutine;
    public int lack = 0;//不足時のテキスト分岐

    public Button next;
    public Button back;
    public Button skil_on;

    public TurnManager turnManager;
    public PopupScript popupScript;

    [SerializeField]
    public GameObject select_dice_number;//好きな出目を選択
    public int select_dice_num = 1;
    public Text select_number;
    public bool future_sight_flag = false;

    public Text Commentary;//説明
    public Text skil_name;
    [SerializeField]
    public GameObject warning;
    [SerializeField]
    public GameObject player_iconselect;

    [SerializeField]
    public GameObject skil_text_all;

    [SerializeField]
    public GameObject skil_Dice;

    public Text warning_text;

    public int[,] skil_count = new int[,]//多次元配列でスキル使用回数を管理
    {
        {1,3,3,3,3 },
        {1,3,3,3,3 },
        {1,3,3,3,3 },
        {1,3,3,3,3 }
    };
    public int skil_page = 1;
    public int now_skilcount = 0;
    public int select_player = 0;//指定プレイヤーの選択

    public int dice_result=0;
    public int random_player = 0;

    public bool overflow = false;
    // Start is called before the first frame update
    void Start()
    {
        _someCoroutine = StartCoroutine(Noruma_lack());
        warning.SetActive(false);
        skil_on.interactable = true;
        next.interactable = true;
        back.interactable = true;
        player_iconselect.SetActive(false);
        skil_Dice.SetActive(false);
        select_dice_number.SetActive(false);
        Skil_managerCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        select_number.text = select_dice_num.ToString();

        if (skil_page == 0)
        {
            skil_page = 5;
        }else if (skil_page == 6)
        {
            skil_page = 1;
        }
        now_skilcount = skil_count[turnManager.currentPlayer % 4,skil_page - 1];

        switch (skil_page)
        {
            case 1:
                skil_name.text = "強奪　残り回数:" + now_skilcount;
                Commentary.text = "指定したプレイヤーの出目×２\r\n「現し世のページ」を奪う" + " ノルマ:40　コスト:0";
                break;
            case 2:
                skil_name.text = "破壊　残り回数:" + now_skilcount;
                Commentary.text = "自分を含むランダムなプレイヤーの出目×２\r\n「現し世のページ」を失わせる" + " ノルマ:80　コスト:5";
                break;
            case 3:
                skil_name.text = "博打　残り回数:" + now_skilcount;
                Commentary.text = "自分を含むランダムなプレイヤーのターンを\r\n１ターン休みにできる" + " ノルマ:60　コスト:10";
                break;
            case 4:
                skil_name.text = "未来予知　残り回数:" + now_skilcount;
                Commentary.text = "1～6のダイス目を指定できる" + "\r\n ノルマ:50　コスト:8";
                break;
            case 5:
                skil_name.text = "限界突破　残り回数:" + now_skilcount;
                Commentary.text = "出た目+２移動できるようになる" + "\r\n ノルマ:30　コスト:5";
                break;
        }
    }

    public void Next_page()
    {
        skil_page += 1;
    }

    public void Back_page()
    {
        skil_page -= 1;
    }

    public void Skil_on()
    {
        switch (skil_page)
        {
            case 1:
                if(turnManager.page[turnManager.currentPlayer % 4]>=40&& now_skilcount > 0)
                {
                    skil_count[turnManager.currentPlayer % 4, skil_page - 1] -= 1;
                    now_skilcount -= 1;
                    skil_text_all.SetActive(false);
                    player_iconselect.SetActive(true);
                }
                else if(now_skilcount == 0)
                {
                    lack = 2;
                    StartCoroutine("Noruma_lack");
                }
                else
                {
                    lack = 1;
                    StartCoroutine("Noruma_lack");
                }

                break;
            case 2:
                if (turnManager.page[turnManager.currentPlayer % 4] >= 80 && now_skilcount > 0)
                {
                    skil_count[turnManager.currentPlayer % 4, skil_page - 1] -= 1;
                    now_skilcount -= 1;
                    turnManager.page[turnManager.currentPlayer % 4] -= 5;
                    skil_text_all.SetActive(false);
                    skil_Dice.SetActive(true);
                }
                else if (now_skilcount == 0)
                {
                    lack = 2;
                    StartCoroutine("Noruma_lack");
                }
                else
                {
                    lack = 1;
                    StartCoroutine("Noruma_lack");
                }
                break;
            case 3:
                if (turnManager.page[turnManager.currentPlayer % 4] >= 60 && now_skilcount > 0)
                {
                    skil_count[turnManager.currentPlayer % 4, skil_page - 1] -= 1;
                    now_skilcount -= 1;
                    turnManager.page[turnManager.currentPlayer % 4] -= 10;
                    skil_text_all.SetActive(false);
                    Gamble();
                }
                else if (now_skilcount == 0)
                {
                    lack = 2;
                    StartCoroutine("Noruma_lack");
                }
                else
                {
                    lack = 1;
                    StartCoroutine("Noruma_lack");
                }
                break;
            case 4:
                if (turnManager.page[turnManager.currentPlayer % 4] >= 50 && now_skilcount > 0)
                {
                    skil_count[turnManager.currentPlayer % 4, skil_page - 1] -= 1;
                    now_skilcount -= 1;

                    turnManager.page[turnManager.currentPlayer % 4] -= 8;
                    skil_text_all.SetActive(false);
                    select_dice_number.SetActive(true);
                }
                else if (now_skilcount == 0)
                {
                    lack = 2;
                    StartCoroutine("Noruma_lack");
                }
                else
                {
                    lack = 1;
                    StartCoroutine("Noruma_lack");
                }
                break;
            case 5:
                if (turnManager.page[turnManager.currentPlayer % 4] >= 30 && now_skilcount > 0)
                {
                    skil_count[turnManager.currentPlayer % 4, skil_page - 1] -= 1;
                    now_skilcount -= 1;
                    turnManager.page[turnManager.currentPlayer % 4] -= 5;
                    overflow = true;
                    skil_text_all.SetActive(false);

                    GetComponent<DiceButton>().skil_end = true;//終了処理
                    skil_text_all.SetActive(true);
                    GetComponent<DiceButton>().Skil_close();

                }
                else if (now_skilcount == 0)
                {
                    lack = 2;
                    StartCoroutine("Noruma_lack");
                }
                else
                {
                    lack = 1;
                    StartCoroutine("Noruma_lack");
                }
                Commentary.text = "出た目+２移動できるようになる" + "\r\n ノルマ:30　コスト:5";
                break;
        }
    }

    public IEnumerator Noruma_lack()
    {
        //ここに処理を書く
        warning.SetActive(true);
        if(lack == 1)
            warning_text.text = "必要ページ数が不足しています";
        else if(lack ==2)
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
    }

    public void Slect_P1()
    {
        select_player = 0;
        player_iconselect.SetActive(false);
        skil_Dice.SetActive(true);
    }

    public void Slect_P2()
    {
        select_player = 1;
        player_iconselect.SetActive(false);
        skil_Dice.SetActive(true);
    }

    public void Slect_P3()
    {
        select_player =2;
        player_iconselect.SetActive(false);
        skil_Dice.SetActive(true);
    }

    public void Slect_P4()
    {
        select_player = 3;
        player_iconselect.SetActive(false);
        skil_Dice.SetActive(true);
    }

    public void Dice_result()//強奪
    {
        dice_result = Random.Range(1, 7);
        dice_result *= 2;

        if (turnManager.page[select_player] <= dice_result * 2)
            dice_result = turnManager.page[select_player];//マイナスにならないように調整
        switch (skil_page)
        {
            case 1://強奪
                turnManager.page[select_player] -= dice_result;
                popupScript.telop_flag = 6;
                popupScript.skil_flag = true;
                turnManager.page[turnManager.currentPlayer % 4] += dice_result;//出目×２のページを獲得
                break;
            case 2://破壊
                random_player = Random.Range(0, 4);
                popupScript.telop_flag = 7;
                popupScript.skil_flag = true;
                turnManager.page[random_player] -= dice_result;
                break;
        }
        

        skil_Dice.SetActive(false);

        GetComponent<DiceButton>().skil_end = true;//終了処理
        skil_text_all.SetActive(true);
        GetComponent<DiceButton>().Skil_close();
        
    }

    public void Gamble()
    {
        random_player = Random.Range(0, 4);
        //random_player = 0;
        turnManager.player[random_player].GetComponent<PlayerScript>().rest_flag = true;
        //Debug.Log("P" + (random_player+1)+"の休み");
        popupScript.telop_flag = 8;
        popupScript.skil_flag = true;

        GetComponent<DiceButton>().skil_end = true;//終了処理
        skil_text_all.SetActive(true);
        GetComponent<DiceButton>().Skil_close();
    }

    public void Decision_number()
    {
        future_sight_flag = true;
        select_dice_number.SetActive(false);

        GetComponent<DiceButton>().skil_end = true;//終了処理
        skil_text_all.SetActive(true);
        GetComponent<DiceButton>().Skil_close();
    }
    public void Next_number()
    {
        select_dice_num += 1;
        if (select_dice_num == 7)
            select_dice_num = 1;
    }

    public void Back_number()
    {
        select_dice_num -= 1;
        if (select_dice_num == 0)
            select_dice_num = 6;
    }
}
