using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCreate : MonoBehaviour {

	public GameObject[] mapObj1 = new GameObject[5];
	public GameObject[] mapObj2 = new GameObject[5];
	public GameObject[] mapObj3 = new GameObject[5];
	public GameObject[] mapObj4 = new GameObject[5];
	public GameObject[] mapObj5 = new GameObject[5];

	private Vector3[,] mapPos = new Vector3[5,5];

	// Use this for initialization
	void Start () {
		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				mapPos[i,j] = new Vector3(j*2,0,8 - 2*i);

				GameObject[] mapObj = new GameObject[5]{null,null,null,null,null};
				switch(i){
				case 0:
					mapObj = mapObj1;
					break;
				case 1:
					mapObj = mapObj2;
					break;
				case 2:
					mapObj = mapObj3;
					break;
				case 3:
					mapObj = mapObj4;
					break;
				case 4:
					mapObj = mapObj5;
					break;
				default:
					break;
				}
				if(mapObj[j] != null){
					GameObject obj = (GameObject)Instantiate(mapObj[j]);
					obj.transform.SetParent(this.transform,false);
					obj.transform.localPosition = mapPos[i,j];
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
