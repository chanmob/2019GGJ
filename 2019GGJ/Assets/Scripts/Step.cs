using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float delay;
    public float fadeTime;

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    private void OnEnable()
    {
        StartCoroutine(FadeStep());
    }

    private IEnumerator FadeStep()
    {
        yield return new WaitForSeconds(delay);

        float t = 1.0f;

        while (t >= 0)
        {
            t -= (Time.deltaTime / fadeTime);

            spriteRenderer.color = new Color(1, 1, 1, t);

            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
