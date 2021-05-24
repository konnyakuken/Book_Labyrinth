using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public DiceButton diceButton;
    public Map map;
    public GameObject player;//プレイヤーの現在地を配列で表現,onoff
    public int player_now =0;//現在地
    public int mass_max = 21; 
    int move_mass = 0;
    public bool my_turn = false;
    public bool dice_select = false;
    public float speed = 1;

    Vector3 my_position;
    Vector3 end_positon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dice_select== true)
        {
            my_position = map.massGameObjects[player_now].transform.position;//開始位置の地面の座標
            my_position.y += 0.65f;
            move_mass = diceButton.Move_result1;//diceのダイスの出目

            end_positon = map.massGameObjects[player_now + 1].transform.position;
            end_positon.y += 0.65f;

            dice_select = false;
            Time.timeScale = 0.1f;
        }

        if (move_mass >= 1)
        {
            transform.position=Vector3.Lerp(my_position, end_positon, 1);
        }else if (move_mass == 0)
        {
            Time.timeScale = 1;
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground"|| collision.gameObject.tag == "Start")
        {
            //マスとの設置判定からどれだけ移動したか把握する
            move_mass -= 1;
            player_now += 1;//スタート地点の処理も後で入れる
           
            switch (player_now)//分岐の処理
            {
                case 21://mass_max
                    player_now = 0;
                    my_position = map.massGameObjects[mass_max].transform.position;
                    my_position.y += 0.65f;//スタート地点
                    end_positon = map.massGameObjects[0].transform.position;
                    end_positon.y += 0.65f;//ゴール地点
                    break;   
                case 27: //スタート分岐
                    

                break;  
                default: //その他
                    my_position = map.massGameObjects[player_now].transform.position;
                    my_position.y += 0.65f;//スタート地点
                    end_positon = map.massGameObjects[player_now + 1].transform.position;
                    end_positon.y += 0.65f;//ゴール地点

                    break;
            }

        }
        
    }

    private void OnCollisionStay (Collision collision2)
    {
        //my_position = collision2.gameObject.position;
    }

    public void Moveplayer()
    {

    }
}
