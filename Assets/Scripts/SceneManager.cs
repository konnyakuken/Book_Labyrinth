using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public int turn=0;//ターン管理　スタート停止も判定
    public int player_turn = 0;//プレイヤーのターン管理
    [SerializeField]
    public GameObject[] player;
     GameObject playerManager;
    // Start is called before the first frame update
    void Start()
    {
        player[1].SetActive(false);
        player[2].SetActive(false);
        player[3].SetActive(false);
        turn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
