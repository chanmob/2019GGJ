using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector3? pos;
    private Vector3 mousePos;
    private Vector3 diff;

    public Sprite[] stepSprites = new Sprite[3];

    private SpriteRenderer spriteRenderer;

    public float speed;
    private float rot_z;

    private bool leftStep = false;

    public int hp;

    public GameObject stone;

    private IEnumerator stepCoroutine;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("이동 시작");
            rb2d.velocity = new Vector2(0, 0);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LookAt();
            if(stepCoroutine == null)
            {
                stepCoroutine = ChangeFootStep();
                StartCoroutine(stepCoroutine);
            }
            pos = mousePos;
        }

        if(pos != null)
        {
            Vector2 direction = (mousePos - this.transform.position).normalized;
            rb2d.AddForce(direction * speed * Time.deltaTime);
        }

        if (Vector2.Distance(this.transform.position, mousePos) < 0.1f)
        {
            StepStop();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ThrowStone();
        }
    }

    public void StepStop()
    {
        Debug.Log("정지");
        pos = null;
        rb2d.velocity = new Vector2(0, 0);
        if (stepCoroutine != null)
        {
            StopCoroutine(stepCoroutine);
            stepCoroutine = null;
        }

        spriteRenderer.sprite = stepSprites[0];
    }

    private IEnumerator ChangeFootStep()
    {
        while (true)
        {
            leftStep = !leftStep;

            if (leftStep)
            {
                spriteRenderer.sprite = stepSprites[1];
            }
            else
            {
                spriteRenderer.sprite = stepSprites[2];
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void LookAt()
    {
        diff = mousePos - transform.position;
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }

    public void ThrowStone()
    {
        Debug.Log("돌을 던진다");
        Instantiate(stone, this.transform.position, this.transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StepStop();
    }
}
