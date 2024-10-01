using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
public float min;
public float max;
void Start () {
	min=transform.position.x;
	max=transform.position.x+16;
}
	void Update () {
		transform.position=new Vector3(Mathf.PingPong(Time.time*3,max-min)+min, transform.position.y, transform.position.z);
	}
}