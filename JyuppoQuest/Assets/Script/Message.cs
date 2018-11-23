using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Message : MonoBehaviour {

	public GameObject commandArea;
	public GameObject explainArea;

	public GameObject attackArea;

	public GameObject textHpHero;
	public GameObject textHpEnemy;

	public GameObject textWinLose;
 
	//　メッセージUI
	private Text messageText;
	//　表示するメッセージ
	private string message;
	//　1回のメッセージの最大文字数
	[SerializeField]
	private int maxTextLength = 90;
	//　1回のメッセージの現在の文字数
	private int textLength = 0;
	//　メッセージの最大行数
	[SerializeField]
	private int maxLine = 3;
	//　現在の行
	private int nowLine = 0;
	//　テキストスピード
	[SerializeField]
	private float textSpeed = 0.05f;
	//　経過時間
	private float elapsedTime = 0f;
	//　今見ている文字番号
	private int nowTextNum = 0;
	//　マウスクリックを促すアイコン
	private Image clickIcon;
	//　クリックアイコンの点滅秒数
	[SerializeField]
	private float clickFlashTime = 0.2f;
	//　1回分のメッセージを表示したかどうか
	private bool isOneMessage = false;
	//　メッセージをすべて表示したかどうか
	private bool isEndMessage = false;
 
	void Start () {
		PlayerPrefs.SetInt("isHeroTurn",0);
		PlayerPrefs.SetInt("isAnimation",0);
		clickIcon = transform.Find("Panel/Image").GetComponent<Image>();
		clickIcon.enabled = false;
		messageText = GetComponentInChildren<Text>();
		messageText.text = "";
		SetMessage("敵が現れた！\n");
	}
 
	void Update () {
		if(Input.GetKeyUp(KeyCode.Return)){
			PlayerPrefs.SetInt("isReturn",0);
		}

		//　メッセージが終わっていない、または設定されている
		if (isEndMessage || message == null) {
			return;
		}
 
		//　1回に表示するメッセージを表示していない	
		if (!isOneMessage) {
 
			//　テキスト表示時間を経過したら
			if (elapsedTime >= textSpeed) {
				messageText.text += message [nowTextNum];
				//　改行文字だったら行数を足す
				if (message [nowTextNum] == '\n') {
					nowLine++;
				}
				nowTextNum++;
				textLength++;
				elapsedTime = 0f;
 
				//　メッセージを全部表示、または行数が最大数表示された
				if (nowTextNum >= message.Length || textLength >= maxTextLength || nowLine >= maxLine) {
					isOneMessage = true;
				}
			}
			elapsedTime += Time.deltaTime;
 
			//　メッセージ表示中にマウスの左ボタンを押したら一括表示
			bool isReturn = (PlayerPrefs.GetInt("isReturn",0) == 0)? false : true; 
			if ((Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Return)) && !isReturn) {
				isReturn = true;
				//　ここまでに表示しているテキストを代入
				var allText = messageText.text;
 
				//　表示するメッセージ文繰り返す
				for (var i = nowTextNum; i < message.Length; i++) {
					allText += message [i];
 
					if (message [i] == '\n') {
						nowLine++;
					}
					nowTextNum++;
					textLength++;
 
					//　メッセージがすべて表示される、または１回表示限度を超えた時
					if (nowTextNum >= message.Length || textLength >= maxTextLength || nowLine >= maxLine) {
						messageText.text = allText;
						isOneMessage = true;
						break;
					}
				}
			}
		//　1回に表示するメッセージを表示した
		} else {
			elapsedTime += Time.deltaTime;
 
			//　クリックアイコンを点滅する時間を超えた時、反転させる
			if(elapsedTime >= clickFlashTime) {
				clickIcon.enabled = !clickIcon.enabled;
				elapsedTime = 0f;
			}

			if(PlayerPrefs.GetInt("isAnimation") == 1)return;
			//　マウスクリックされたら次の文字表示処理
			bool isReturn = (PlayerPrefs.GetInt("isReturn",0) == 0)? false : true; 
			if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) && !isReturn) {
				Debug.Log (messageText.text.Length);
				messageText.text = "";
				nowLine = 0;
				clickIcon.enabled = false;
				elapsedTime = 0f;
				textLength = 0;
				isOneMessage = false;
				PlayerPrefs.SetInt("isReturn",1);

				//　メッセージが全部表示されていたらゲームオブジェクト自体の削除
				if(nowTextNum >= message.Length) {
					
					if(int.Parse(textHpEnemy.GetComponent<Text>().text) == 0){
						textWinLose.SetActive(true);
						textWinLose.GetComponent<Text>().color = Color.red;
						textWinLose.GetComponent<Text>().text = "WIN";
						iTween.MoveFrom(textWinLose, iTween.Hash("x",-10));
						nowTextNum = 0;
						isEndMessage = true;
						SetMessagePanel("戦いに勝利した！\nステータスが上昇したぞ");
					}else if(int.Parse(textHpHero.GetComponent<Text>().text) == 0){
						textWinLose.SetActive(true);
						textWinLose.GetComponent<Text>().color = Color.blue;
						textWinLose.GetComponent<Text>().text = "LOSE";
						iTween.MoveFrom(textWinLose, iTween.Hash("x",-10));
						nowTextNum = 0;
						isEndMessage = true;
						SetMessagePanel("負けてしまった...");
					}else{
						
						//攻撃選択に進む
						PlayerPrefs.SetInt("isHeroTurn",1);
						nowTextNum = 0;
						isEndMessage = true;
						transform.Find("Panel").GetChild(0).gameObject.SetActive (false);
						transform.Find("Panel").GetChild(1).gameObject.SetActive (false);
						//transform.Find("Panel").GetChild(2).gameObject.SetActive (true);
						commandArea.SetActive(true);
						explainArea.SetActive(true);
						PlayerPrefs.SetInt("isBattle",1);
						//それ以外はテキスト処理関連を初期化して次の文字から表示させる
					}/*else{
						//敵の攻撃に入る
						nowTextNum = 0;
						isEndMessage = true;
						PlayerPrefs.SetInt("isHeroTurn",0);
						PlayerPrefs.SetInt("enemycommand",UnityEngine.Random.Range(0,4));

						//行動
						int hero = PlayerPrefs.GetInt("herocommand");
						int enemy = PlayerPrefs.GetInt("enemycommand");

						int heroAttack = PlayerPrefs.GetInt("attack");
						int enemyAttack = PlayerPrefs.GetInt("enemyattack");

						commandArea.SetActive(false);
						explainArea.SetActive(false);

						if(enemy == 0){
							//攻撃
							if(hero == 0){
								//ヒーローが攻撃していた時
								attackArea.GetComponent<SelectCommand>().EnemyAttack((int)enemyAttack);
								SetMessagePanel(
								"敵の攻撃！\n"
								+"\n"
								+"\n"
								+"く。いいダメージだ。");
							}else if(hero == 1){
								//ヒーローが防御していた時
								attackArea.GetComponent<SelectCommand>().EnemyAttack((int)enemyAttack/2);
								SetMessagePanel(
								"敵の攻撃！\n"
								+"\n"
								+"\n"
								+"防御していたので、敵の攻撃を防いだ。");
							}else if(hero == 2){
								//ヒーローが回復していた時
								attackArea.GetComponent<SelectCommand>().EnemyAttack((int)enemyAttack*2);
								SetMessagePanel(
								"敵の攻撃！\n"
								+"\n"
								+"\n"
								+"回復している隙に攻撃された。\nより多くのダメージを食らった。");
							}
						}else if(enemy == 1){
							//防御
							attackArea.GetComponent<SelectCommand>().EnemyDefense();
							SetMessagePanel(
							"敵の防御！\n"
							+"\n"
							+"\n"
							+"次の攻撃のダメージを抑えられてしまう。");
						}else if(enemy == 2){
							//回復
							attackArea.GetComponent<SelectCommand>().EnemyHeal(enemyAttack);
							SetMessagePanel(
							"敵の回復！\n"
							+"\n"
							+"\n"
							+"回復している隙がチャンスだ！");
						}else if(enemy == 3){
							//休憩
							SetMessagePanel(
							"敵の休憩！\n"
							+"\n"
							+"\n"
							+"敵がボーッとしている！");
						}		
					}*/
				}
			}
		}
	}
 
	void SetMessage(string message) {
		this.message = message;
	}

	//　他のスクリプトから新しいメッセージを設定
	public void SetMessagePanel(string message) {
		SetMessage (message);
		transform.Find("Panel").GetChild(0).gameObject.SetActive (true);
		transform.Find("Panel").GetChild(1).gameObject.SetActive (true);
		isEndMessage = false;
	}
}
