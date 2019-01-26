using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public GameObject player;   

	void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");
	}

	void LateUpdate()
	{
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
}