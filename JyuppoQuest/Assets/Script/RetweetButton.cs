using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetweetButton : MonoBehaviour {

	public void OnClick(){
		int winlose = PlayerPrefs.GetInt("questcomplete",0);
		if(winlose == 1){
			//勝ち
			naichilab.UnityRoomTweet.TweetWithImage ("jyuppo-quest", "ラスボスを倒した！次の勇者は君だ！", "unityroom", "unity1week");
		}else{
			//負け
			naichilab.UnityRoomTweet.TweetWithImage ("jyuppo-quest", "勇者は力尽きた... 助けを求む...", "unityroom", "unity1week");
		}
		
	}
}
