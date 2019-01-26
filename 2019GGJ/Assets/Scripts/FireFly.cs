using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour
{
    public ParticleSystem[] ps;

    private bool oneTime;

    private void Start()
    {
        ps = GetComponentsInChildren<ParticleSystem>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !oneTime)
        {
            oneTime = true;
            ps[0].gameObject.SetActive(false);
            ps[1].gameObject.SetActive(true);
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(5f);

        this.gameObject.SetActive(false);
    }
}
