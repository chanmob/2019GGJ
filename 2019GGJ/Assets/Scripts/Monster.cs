using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum STATE
    {
        CHASE,
        RUSH,
        NONE
    }

    public STATE state = STATE.NONE;

    public bool chase;

    private Rigidbody2D rb2d;

	[SerializeField]
    private GameObject player;
	public GameObject particle;

    public float chaseSpeed;
    public float rushSpeed;
    public float maxChaseDistance;
    private float distance;
    private float rot_z;

    private Vector3 diff;
    private Vector3 playerPos;

    private IEnumerator inSightCoroutine;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        particle = transform.Find("Particle").gameObject;
		player = GameObject.FindGameObjectWithTag("Player");
	}

    private void Update()
    {
        if (chase && state == STATE.CHASE)
        {
            transform.Translate(Vector2.down * chaseSpeed * Time.deltaTime);
            LookAtPlayer();

            distance = Vector2.Distance(this.transform.position, player.transform.position);

            if(distance >= maxChaseDistance)
            {
                Stop();
                particle.SetActive(false);
            }

            if(player == null)
            {
                Stop();
                particle.SetActive(false);
            }
        }
    }

    public void Attack(GameObject _go, bool _stone)
    {
        switch (state)
        {
            case STATE.CHASE:
                chase = true;
                break;
            case STATE.RUSH:
                StartCoroutine(RushToPlayer());
                break;
        }
        particle.SetActive(true);
        player = _go;
        playerPos = player.transform.position;

        if (_stone)
        {
            if (inSightCoroutine == null)
            {
                inSightCoroutine = ParticleOn();
                StartCoroutine(inSightCoroutine);
            }
            else
            {
                StopCoroutine(inSightCoroutine);
                inSightCoroutine = ParticleOn();
                StartCoroutine(inSightCoroutine);
            }
        }
    }

    private void LookAtPlayer()
    { 
        diff = playerPos - transform.position;
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }


    public void Stop()
    {
        chase = false;
    }

    private IEnumerator ParticleOn()
    {
        yield return new WaitForSeconds(2f);

        particle.SetActive(false);
    }

    private IEnumerator RushToPlayer()
    {
        yield return new WaitForSeconds(1f);

        Vector2 direction;

        if(player == null)
        {
            direction = (playerPos - this.transform.position).normalized;
        }
        else
        {
            direction = (player.transform.position - this.transform.position).normalized;
        }

        rb2d.AddForce(direction * rushSpeed * Time.deltaTime);

        yield return new WaitForSeconds(3f);

        particle.SetActive(false);
    }
}
