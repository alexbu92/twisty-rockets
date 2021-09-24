using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	public float speed = 10.0f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*speed,0f,Input.GetAxis("Vertical")*Time.deltaTime*speed);
	}
}
