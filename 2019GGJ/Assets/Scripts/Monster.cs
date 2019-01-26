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

    public float chaseSpeed;
    public float rushSpeed;
    public float maxChaseDistance;
    private float rot_z;

    private Vector3 diff;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (chase && state == STATE.CHASE)
        {
            transform.Translate(Vector2.down * chaseSpeed * Time.deltaTime);
            LookAtPlayer();

            float distnace = Vector2.Distance(this.transform.position, player.transform.position);

            if(distnace >= maxChaseDistance)
            {
                Stop();
            }
        }
    }

    public void Attack(GameObject _go)
    {
        chase = true;
        player = _go;
    }

    private void LookAtPlayer()
    {
        diff = player.transform.position - transform.position;
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }


    public void Stop()
    {
        chase = false;
    }
}
