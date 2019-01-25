using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector3 mousePos;

    public float speed;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - this.transform.position).normalized;
            rb2d.AddForce(direction * speed * Time.deltaTime);

            if(Vector2.Distance(this.transform.position, mousePos) < 0.2f)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
	}
}
