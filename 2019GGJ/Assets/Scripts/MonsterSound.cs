using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSound : MonoBehaviour
{
    private AudioSource audiosource;

    public AudioClip attackSound;
    public AudioClip boundarySound;

    public GameObject player;

    private CircleCollider2D circleCollider2D;

    private bool soundOn = false;

    public float distance;
    public float colliderRadius;
    private float time;
    public float soundDelay;
    public float divisionValue;
    public float boundaryRange;

	// Use this for initialization
	void Start ()
    {
        audiosource = GetComponent<AudioSource>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        audiosource.playOnAwake = false;
        circleCollider2D.radius = colliderRadius;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (soundOn)
        {
            distance = Vector2.Distance(player.transform.position, this.transform.position);
            time += Time.deltaTime;

            if(distance <= boundaryRange)
            {
                PlaySound(attackSound);
            }

            else
            {
                PlaySound(boundarySound);
            }
        }	
	}

    private void PlaySound(AudioClip _clip)
    {
        if (time >= soundDelay)
        {
            time = 0f;
            audiosource.clip = _clip;
            audiosource.volume = 1;
            audiosource.volume -= distance / divisionValue;
            audiosource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundOn = false;
            time = 0f;
        }
    }
}
