using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Rigidbody2D rb2d;

    private CircleCollider2D circle2d;

    public GameObject waveParticle;

    public float maxRadius;

    public float speed;
    public float increaseRadiusTime;

    private IEnumerator WaveCoroutine;

	// Use this for initialization
	void Start ()
    {
        circle2d = GetComponent<CircleCollider2D>();
        rb2d.AddForce(transform.up * -speed);

        waveParticle = transform.Find("Wave").gameObject;

        WaveCoroutine = CreateWave(1f);
        StartCoroutine(WaveCoroutine);

        Destroy(this.gameObject, 5f);
    }

    private IEnumerator CreateWave(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        Debug.Log("파동 시작");
        rb2d.velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;

        waveParticle.SetActive(true);

        float t = 0;

        while (t < maxRadius)
        {
            t += (Time.deltaTime / increaseRadiusTime) * maxRadius;
            circle2d.radius = t;

            yield return null;
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map"))
        {
            Debug.Log("벽과 부딪힘");
            StopCoroutine(WaveCoroutine);
            WaveCoroutine = CreateWave(0f);
            StartCoroutine(WaveCoroutine);
        }
        else if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().Attack(this.gameObject, true);
        }
    }
}
