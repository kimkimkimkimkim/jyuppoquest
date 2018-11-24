using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
			PlayerPrefs.SetInt("canMove",0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
