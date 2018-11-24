using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour {

	public int target;

	// Use this for initialization
	void Start () {
		Vector3 pos = transform.localPosition;
		pos.y += 1;
		transform.localPosition = pos;
		target = int.Parse(this.name[4].ToString());
	}
}
