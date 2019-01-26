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

    private GameObject player;
    private GameObject particle;

    public float chaseSpeed;
    public float rushSpeed;
    public float maxChaseDistance;
    private float rot_z;

    private Vector3 diff;
    private Vector3 playerPos;

    private IEnumerator inSightCoroutine;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        particle = transform.Find("Particle").gameObject;
    }

    private void Update()
    {
        if (chase && state == STATE.CHASE)
        {
            transform.Translate(Vector2.down * chaseSpeed * Time.deltaTime);
            LookAtPlayer();

            float distnace = Vector2.Distance(this.transform.position, playerPos);

            if(distnace >= maxChaseDistance)
            {
                Stop();
                particle.SetActive(false);
            }
        }
    }

    public void Attack(GameObject _go, bool _stone)
    {
        chase = true;
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

        Stop();
        particle.SetActive(false);
    }
}
