using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public DiceButton diceButton;

    public int turn = 0;//ターン管理　スタート停止も判定
    public int last_turn = 0;
    public int count = 1;
                        // 現在のプレイヤー番号
    public int currentPlayer = 0;

    public Text player_trunText;

    [SerializeField]
    public GameObject[] player;

    [SerializeField]
    public GameObject[] cam;

    [SerializeField]
    public GameObject[] sleep_player;

    [SerializeField]
    public GameObject create_book;

    [SerializeField]
    public GameObject[] book_image;

    public Text turn_countText;
    GameObject playerManager;


    Vector3 sleep_pos;

    public int[] page=new int[4] {50,50,50,50};
    public Text[] pageText;
    //public bool[] create_book=new bool[4] { false,false,false,false};



    Vector3 sleep_position;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 3; i++)//1p以外は一回comに
            player[i].GetComponent<PlayerScript>().computer = true;

        if (titleScript.player_num == 1)//soroプレイ
        {
            player[0].GetComponent<PlayerScript>().computer = false;
            titleScript.player_num = 0;
        }
        else if (titleScript.player_num == 2)//2人プレイ
        {
            player[0].GetComponent<PlayerScript>().computer = false;
            int p2 = Random.Range(1, 4);
            player[p2].GetComponent<PlayerScript>().computer = false;
            titleScript.player_num = 0;
        }
        else if (titleScript.player_num == 3)//3人プレイ
        {
            player[0].GetComponent<PlayerScript>().computer = false;//仮実装
            int p2 = 1;
            int p3 = 2;
            player[p2].GetComponent<PlayerScript>().computer = false;
            player[p3].GetComponent<PlayerScript>().computer = false;
            titleScript.player_num = 0;
        }else if (titleScript.player_num == 4)//4人プレイ
        {
            for (int i = 1; i <= 3; i++)//1p以外は一回comに
                player[i].GetComponent<PlayerScript>().computer = false;
        }
            player[1].SetActive(false);
        player[2].SetActive(false);
        player[3].SetActive(false);
        turn = 1;
        currentPlayer = 0;
        turn_countText.text = "ターン数:" + turn;
        player[currentPlayer % 4].SetActive(true);
        player[currentPlayer % 4].GetComponent<PlayerScript>().my_turn = true;

        cam[currentPlayer % 4].SetActive(true);
        cam[1].SetActive(false);
        cam[2].SetActive(false);
        cam[3].SetActive(false);

        create_book.SetActive(false);

        if (last_turn != turn)
        {
            if (turn % 3 == 0)//3ターン毎に+１する
                count++;
            for (int i = 0; i < 4; i++)
                page[i] += count;
            last_turn = turn;
        }

        for (int i = 0; i < 4; i++)
            pageText[i].text = page[i].ToString();

        for (int i = 0; i < 4; i++)
            book_image[i].SetActive(false);
     }

    // Update is called once per frame
    void Update()
    {
        //ページ数の更新
        for (int i = 0; i < 4; i++)
            pageText[i].text = page[i].ToString();


        if (player[currentPlayer % 4].GetComponent<PlayerScript>().computer == false&& GetComponent<DiceButton>().skil_flag == false&&diceButton.move_Buttonflag == 0)//100を超えたら本を作成するボタンをonにする
        {
            //Debug.Log(page[currentPlayer % 4]);
            if (page[currentPlayer % 4] >= 100)
                create_book.SetActive(true);
            else
                create_book.SetActive(false);

        }
        else if (page[currentPlayer % 4] >= 100 && player[currentPlayer % 4].GetComponent<PlayerScript>().my_turn == true && player[currentPlayer % 4].GetComponent<PlayerScript>().turn_end == false && player[currentPlayer % 4].GetComponent<PlayerScript>().computer == true&& diceButton.move_Buttonflag == 0)
        {
            //100を超えたら本を作成するボタンをonにする)
            GetComponent<DiceButton>().create_bookButton();
        }
        else
            create_book.SetActive(false);

    }

    public void turn_switch()
    {

        cam[currentPlayer % 4].SetActive(false);
        sleep_pos = player[currentPlayer % 4].transform.position;
        sleep_pos.y +=0.6f;
        sleep_player[currentPlayer % 4].transform.position = sleep_pos;
        sleep_player[currentPlayer % 4].SetActive(true);//待機中のオブジェクトをonに
        currentPlayer += 1;
        //Debug.Log(currentPlayer % 4);

        turn = (currentPlayer / 4) + 1;//ターン数の確認
        turn_countText.text = "ターン数:" + turn;
        player_trunText.text =  (currentPlayer % 4 + 1) + "Pのターン";
        sleep_player[currentPlayer % 4].SetActive(false);
        player[currentPlayer % 4].SetActive(true);//プレイヤー表示の切り替え
        cam[currentPlayer % 4].SetActive(true);
        Invoke("delay_player", 2.0f);
        player[currentPlayer % 4].GetComponent<PlayerScript>().select_com = true;

        if (last_turn != turn)
        {
            if (turn % 3 == 0)//3ターン毎に+１する
                count++;
            for (int i = 0; i < 4; i++)
                page[i] += count;
            last_turn = turn;
        }

        
    }


    public void delay_player()
    {
        player[currentPlayer % 4].GetComponent<PlayerScript>().my_turn = true;
        if(player[currentPlayer % 4].GetComponent<PlayerScript>().computer==true)
            player[currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
    }
}
