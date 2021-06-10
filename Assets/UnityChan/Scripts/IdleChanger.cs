using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace UnityChan
{
//
// ↑↓キーでループアニメーションを切り替えるスクリプト（ランダム切り替え付き）Ver.3
// 2014/04/03 N.Kobayashi
// 2015/03/11 Revised for Unity5 (only)
//

// Require these components when using this script
	[RequireComponent(typeof(Animator))]



	public class IdleChanger : MonoBehaviour
	{
		public TurnManager turnManager;
		public DiceButton diceButton;

		private Animator anim;						// Animatorへの参照
		private AnimatorStateInfo currentState;		// 現在のステート状態を保存する参照
		private AnimatorStateInfo previousState;	// ひとつ前のステート状態を保存する参照
		public bool _random = false;				// ランダム判定スタートスイッチ
		public float _threshold = 0.5f;				// ランダム判定の閾値
		public float _interval = 10f;               // ランダム判定のインターバル
      //private float _seed = 0.0f;					// ランダム判定用シード
        public bool isGUI = true;



		public int next_flag = 0;
		public bool anime_switching = false;

		

		void Start ()
		{
			// 各参照の初期化
			anim = GetComponent<Animator> ();
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			previousState = currentState;
			// ランダム判定用関数をスタートする
			StartCoroutine ("RandomChange");
		}
	
		// Update is called once per frame
		void  Update ()
		{
			if (turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().anime_flagNum == 1&& turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag == false) {
				anim.SetInteger("Now",1);
                // ブーリアンNextをtrueにする
                //anim.SetBool("Next", true);
            }
            else
            {
				anim.SetInteger("Now", 0);
			}

			//1=down、2=up、3=left、4=right
			if (diceButton.player_rotation == 1)
            {
				transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
				diceButton.player_rotation = 5;//何度も実行されるのを防ぐ為
				transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
				{
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
					diceButton.player_rotation = 0;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().direction = 1;
				});
			}
			else if (diceButton.player_rotation == 2)
			{
				transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
				diceButton.player_rotation = 5;//何度も実行されるのを防ぐ為
				transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
				{
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
					diceButton.player_rotation = 0;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().direction = 2;
				});
			}
			else if (diceButton.player_rotation == 3)
			{
				transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
				diceButton.player_rotation = 5;
				transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
				{
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
					diceButton.player_rotation = 0;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().direction = 3;
				});
			}
			else if (diceButton.player_rotation == 4)
			{
				transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
				diceButton.player_rotation = 5;
				transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
				{
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
					diceButton.player_rotation = 0;
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().direction = 4;
				});
			}

			if (turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().warp_flag == true)//ワープ時のアニメーション処理
			{
				transform.DORotate(new Vector3(0, 180, 0), 0, RotateMode.Fast);
				transform.DORotate(new Vector3(0, 180, 0), 0, RotateMode.Fast).OnComplete(() =>//移動終了後実行
				{
					turnManager.player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().warp_flag = false;
				});
			}

			/*.
			// ↓キーが押されたら、ステートを前に戻す処理
			if (Input.GetKeyDown ("down")) {
				// ブーリアンBackをtrueにする
				anim.SetBool ("Back", true);
			}*/
			/*
			// "Next"フラグがtrueの時の処理
			if (anim.GetBool ("Next")) {
				// 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
				currentState = anim.GetCurrentAnimatorStateInfo (0);
				if (previousState.fullPathHash != currentState.fullPathHash) {
					anim.SetBool ("Next", false);
					previousState = currentState;				
				}
			}
		
			// "Back"フラグがtrueの時の処理
			if (anim.GetBool ("Back")) {
				// 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
				currentState = anim.GetCurrentAnimatorStateInfo (0);
				if (previousState.fullPathHash != currentState.fullPathHash) {
					anim.SetBool ("Back", false);
					previousState = currentState;
				}
			}
			*/
		}

		void OnGUI ()
		{
            if (isGUI)
            {
                GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Change Motion");
                if (GUI.Button(new Rect(Screen.width - 100, 40, 80, 20), "Next"))
                    anim.SetBool("Next", true);
                if (GUI.Button(new Rect(Screen.width - 100, 70, 80, 20), "Back"))
                    anim.SetBool("Back", true);
            }
		}


		// ランダム判定用関数
		IEnumerator RandomChange ()
		{
			// 無限ループ開始
			while (true) {
				//ランダム判定スイッチオンの場合
				if (_random) {
					// ランダムシードを取り出し、その大きさによってフラグ設定をする
					float _seed = Random.Range (0.0f, 1.0f);
					if (_seed < _threshold) {
						anim.SetBool ("Back", true);
					} else if (_seed >= _threshold) {
						anim.SetBool ("Next", true);
					}
				}
				// 次の判定までインターバルを置く
				yield return new WaitForSeconds (_interval);
			}

		}

	}
}
