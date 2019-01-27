using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSound : MonoBehaviour
{
	public enum STATE
	{
		TUTORIAL,
		STAGE,
		NONE
	}

	public STATE state = STATE.NONE;

	[SerializeField]
	private Monster_Tutorial monster_tutorial;
	private Monster monster;

	[SerializeField]
	private AudioSource audiosource;

    public AudioClip attackSound;
    public AudioClip boundarySound;

    public GameObject player;
	
	private CircleCollider2D circleCollider2D;

	private bool soundOn = false;
	private bool inBoundary = false;

	private float distance;
    public float colliderRadius;
	private float time;
    public float soundDelay;
    public float divisionValue;
    public float boundaryRange;

	// Use this for initialization
	void Start ()
    {
		switch(state)
		{
			case STATE.STAGE:
				monster = GetComponentInParent<Monster>();
				break;
			case STATE.TUTORIAL:
				monster_tutorial = GetComponentInParent<Monster_Tutorial>();
				break;
		}


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
            if(player == null)
            {
                return;
            }
            distance = Vector2.Distance(player.transform.position, this.transform.position);
            time += Time.deltaTime;

            if(distance <= boundaryRange)
            {
                if (!inBoundary)
                {
                    inBoundary = true;
					switch (state)
					{
						case STATE.STAGE:
							monster.Attack(player, false);
							break;
						case STATE.TUTORIAL:
							monster_tutorial.Attack(player, false);
							break;
					}
                    time = soundDelay;
                }

                PlaySound(attackSound);

            }

            else
            {
                PlaySound(boundarySound);
                inBoundary = false;
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
