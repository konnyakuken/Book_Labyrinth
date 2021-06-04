using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resultScript : MonoBehaviour
{
    public Text win; 
    // Start is called before the first frame update
    void Start()
    {
        win.text = "P"+DiceButton.winner.ToString()+" 脱出！";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        DiceButton.winner = 0;
        SceneManager.LoadScene("Main");//シーン切り替え
    }
}
