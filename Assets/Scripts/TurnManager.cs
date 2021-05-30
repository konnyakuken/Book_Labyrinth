using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public int turn = 0;//ターン管理　スタート停止も判定
                        // 現在のプレイヤー番号
    public int currentPlayer = 0;
    public bool turn_switching = false;//ターン切り替え処理中かどうか

    [SerializeField]
    public GameObject[] player;

    [SerializeField]
    public GameObject[] cam;

    [SerializeField]
    public GameObject[] sleep_player;

    public Text turn_countText;
    GameObject playerManager;

    Vector3 sleep_position;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void turn_switch()
    {

        cam[currentPlayer % 4].SetActive(false);
        sleep_player[currentPlayer % 4].transform.position= player[currentPlayer % 4].transform.position;
        sleep_player[currentPlayer % 4].SetActive(true);//待機中のオブジェクトをonに
        currentPlayer += 1;
        //Debug.Log(currentPlayer % 4);
        turn = (currentPlayer / 4) + 1;//ターン数の確認
        turn_countText.text = "ターン数:" + turn;

        sleep_player[currentPlayer % 4].SetActive(false);
        player[currentPlayer % 4].SetActive(true);//プレイヤー表示の切り替え
        cam[currentPlayer % 4].SetActive(true);
        player[currentPlayer % 4].GetComponent<PlayerScript>().my_turn = true;
    }
}
