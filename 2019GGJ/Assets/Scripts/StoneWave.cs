using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWave : MonoBehaviour {

    private CircleCollider2D circle2d;
    public float maxRadius;

	// Use this for initialization
	void Start () {
        circle2d = GetComponent<CircleCollider2D>();
        StartCoroutine(Wave());
	}
	
    private IEnumerator Wave()
    {
        float t = circle2d.radius;

        while (t < maxRadius)
        {
            t += (Time.deltaTime / 1);

            circle2d.radius = t;

            yield return null;
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().Attack(this.gameObject, true);
        }
    }
}
