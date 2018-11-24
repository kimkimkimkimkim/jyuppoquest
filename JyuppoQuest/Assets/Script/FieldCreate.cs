using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldCreate : MonoBehaviour {

	public int stageNum;

	public bool reset;

	public GameObject hero;

	public GameObject textFoot;
	public GameObject textHp;
	public GameObject textAttack;

	public GameObject[] mapObj1 = new GameObject[5];
	public GameObject[] mapObj2 = new GameObject[5];
	public GameObject[] mapObj3 = new GameObject[5];
	public GameObject[] mapObj4 = new GameObject[5];
	public GameObject[] mapObj5 = new GameObject[5];

	private GameObject objectStatus;

	private Vector3[,] mapPos = new Vector3[5,5];

	// Use this for initialization
	void Awake(){
		/* 
		if(PlayerPrefs.GetInt("First",0) == 0 || reset){
			for(int i=0;i<4;i++){
				for(int j=0;j<5;j++){
					for(int k=0;k<5;k++){
						string pos = (i+1).ToString() + j.ToString() + k.ToString();
						PlayerPrefs.SetInt(pos,1);
					}
				}
			}
			PlayerPrefs.SetInt("posx",4);
			PlayerPrefs.SetInt("posz",4);
			PlayerPrefs.SetInt("foot",10);
			PlayerPrefs.SetInt("hp",100);
			PlayerPrefs.SetInt("attack",10);
			PlayerPrefs.SetInt("nowStage",1);
			PlayerPrefs.SetInt("isScreenChange",0);
		}
		*/
	}
	void Start () {
		PlayerPrefs.SetInt("isAnimation",0);
		if(PlayerPrefs.GetInt("foot") == 0){

			StartCoroutine(DelayMethod(2.1f, () =>
			{
				//ボス戦
				FadeManager.Instance.LoadScene ("BossBattleScene", 2.0f);
				RemainAudio.Instance.ChangeBgm(2);
			}));
		}
		PlayerPrefs.SetInt("isScreenChange",0);
		objectStatus = GameObject.Find("ObjectStatus");
		StartCoroutine(DelayMethod(2.0f,() => {
			PlayerPrefs.SetInt("canMove",1);
		}));

		PlayerPrefs.SetInt("First",1);
		hero.transform.localPosition = new Vector3(PlayerPrefs.GetInt("posx"),1,PlayerPrefs.GetInt("posz"));
		textFoot.GetComponent<Text>().text = PlayerPrefs.GetInt("foot").ToString();
		textHp.GetComponent<Text>().text = PlayerPrefs.GetInt("hp").ToString();
		textAttack.GetComponent<Text>().text = PlayerPrefs.GetInt("attack").ToString();

		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				mapPos[i,j] = new Vector3(j*2,0,8 - 2*i);

				GameObject[] mapObj = new GameObject[5]{null,null,null,null,null};
				string[] status = new string[5]{null,null,null,null,null};
				switch(i){
				case 0:
					mapObj = mapObj1;
					status = objectStatus.GetComponent<ObjectStatus>().status1;
					break;
				case 1:
					mapObj = mapObj2;
					status = objectStatus.GetComponent<ObjectStatus>().status2;
					break;
				case 2:
					mapObj = mapObj3;
					status = objectStatus.GetComponent<ObjectStatus>().status3;
					break;
				case 3:
					mapObj = mapObj4;
					status = objectStatus.GetComponent<ObjectStatus>().status4;
					break;
				case 4:
					mapObj = mapObj5;
					status = objectStatus.GetComponent<ObjectStatus>().status5;
					break;
				default:
					break;
				}
				if(mapObj[j] != null){
					string id = stageNum.ToString() + i.ToString() + j.ToString();
					if(PlayerPrefs.GetInt(id) == 0)continue;
					GameObject obj = (GameObject)Instantiate(mapObj[j]);
					obj.GetComponent<ItemID>().id = id;
					obj.transform.SetParent(this.transform,false);
					obj.transform.localPosition = mapPos[i,j];

					if(status[j] == "") continue;
					string str = status[j];
					string[] array = str.Split(' ');
					if(array.Length == 2){
						int hp = int.Parse(array[0]);
						int attack = int.Parse(array[1]);
						obj.GetComponent<ItemStatus>().hp = hp;
						obj.GetComponent<ItemStatus>().attack = attack;
					}else{
						int hp = int.Parse(array[0]);
						int attack = int.Parse(array[1]);
						int uphp = int.Parse(array[2]);
						int upattack = int.Parse(array[3]);
						obj.GetComponent<ItemStatus>().hp = hp;
						obj.GetComponent<ItemStatus>().attack = attack;
						obj.GetComponent<ItemStatus>().uphp = uphp;
						obj.GetComponent<ItemStatus>().upattack = upattack;
					}
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
