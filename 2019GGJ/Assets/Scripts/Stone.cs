using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private CircleCollider2D circle2d;

    public float speed;
    public float maxradius;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        circle2d = GetComponent<CircleCollider2D>();
        rb2d.AddForce(transform.up * -speed);

        StartCoroutine(Wave());
    }

    private IEnumerator Wave()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("파동 시작");
        rb2d.velocity = new Vector2(0, 0);
        GetComponent<MeshRenderer>().enabled = false;

        float t = 0.5f;

        while (t < maxradius)
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
