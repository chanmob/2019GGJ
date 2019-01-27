using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private Rigidbody2D rb2d;

	private AudioSource audiosource;

	private Vector3? pos;
	private Vector3 startPos;
	private Vector3 downMousePos;
	private Vector3 upMousePos;
	private Vector3 diff;

	public float speed;
	private float rot_z;
	private float clickTime;

	private bool torchlightOn = false;
	private bool interactTorch;
	private bool leftstep = false;
	public bool death = false;

	public int hp;

	public GameObject stone;
	public GameObject[] step = new GameObject[2];
	private List<GameObject> leftSteps = new List<GameObject>();
	private List<GameObject> rightSteps = new List<GameObject>();
	private GameObject torch;

	public Text childDialogue;
	public string[] dialogueContents = new string[3];

    private IEnumerator stepCoroutine;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
        startPos = transform.position;
        torch = transform.Find("Torch").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickTime = 0f;
            downMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            interactTorch = false;
        }

        else if (Input.GetMouseButton(0) && !interactTorch)
        {
            clickTime += Time.deltaTime;

            if (clickTime >= 1f)
            {
                Debug.Log("Long Click : Torch");
                interactTorch = true;
                clickTime = 0f;
                torchlightOn = !torchlightOn;
                torch.SetActive(torchlightOn);
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            upMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(Vector2.Distance(downMousePos, upMousePos)) >= 2f)
            {
                if(stone == null)
                {
                    return;
                }
                Debug.Log("Swipe : Throw Stone");
                StepStop();
                LookAt(upMousePos);
                ThrowStone();
            }
            else
            {
                Debug.Log("Short Click : Move");
                rb2d.velocity = new Vector2(0, 0);
                if (!audiosource.isPlaying)
                {
                    audiosource.Play();
                }
                LookAt(downMousePos);
                if (stepCoroutine == null)
                {
                    stepCoroutine = FootStep();
                    StartCoroutine(stepCoroutine);
                }
                pos = downMousePos;
            }
        }

        if (pos != null)
        {
            Vector2 direction = (downMousePos - this.transform.position).normalized;
            transform.Translate(Vector2.up * -speed * Time.deltaTime);
            //rb2d.AddForce(direction * speed * Time.deltaTime);

            if (Vector2.Distance(this.transform.position, downMousePos) < 0.1f)
            {
                StepStop();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ThrowStone();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            torchlightOn = !torchlightOn;
            torch.SetActive(torchlightOn);
        }
    }

    private IEnumerator FootStep()
    {
        while (true)
        {
            leftstep = !leftstep;

            if (leftstep)
            {
                StepPooling(step[0], leftSteps);
            }
            else
            {
                StepPooling(step[1], rightSteps);
            }

            yield return new WaitForSeconds(0.6f);
        }
    }

    private void StepPooling(GameObject _go, List<GameObject> _goList)
    {
        for (int i = 0; i < rightSteps.Count; i++)
        {
            if (!_goList[i].activeInHierarchy)
            {
                _goList[i].transform.position = this.transform.position;
                _goList[i].transform.rotation = this.transform.rotation;
                _goList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                _goList[i].SetActive(true);
                return;
            }
        }

        var go = Instantiate(_go, this.transform.position, this.transform.rotation);
        _goList.Add(go);
    }

    public void StepStop()
    {
        Debug.Log("정지");
        pos = null;
        audiosource.Stop();
        rb2d.velocity = new Vector2(0, 0);
        if(stepCoroutine != null)
        {
            StopCoroutine(stepCoroutine);
            stepCoroutine = null;
            leftstep = false;
        }
    }

    private void LookAt(Vector3 _pos)
    {
        diff = _pos - transform.position;
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

        if (collision.gameObject.CompareTag("Monster") && !death)
        {
            Debug.Log("몬스터와 충돌");
            death = true;
            GameManager.instance.IncreaseDeath();
            StartCoroutine(ReStart());
            Vector2 direction = (collision.transform.position - this.transform.position).normalized;
            rb2d.AddForce(direction * -100);
        }
    }

    private IEnumerator ReStart()
    {
        yield return new WaitForSeconds(1f);

        this.transform.position = startPos;
        death = false;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Cold&Juicy"))
		{
			childDialogue.text = dialogueContents[0];
			StartCoroutine(ContentsClear());
		}
		else if (collision.CompareTag("ManyMe"))
		{
			childDialogue.text = dialogueContents[1];
			StartCoroutine(ContentsClear());
		}
		if (collision.CompareTag("BumpyStick"))
		{
			childDialogue.text = dialogueContents[2];
			StartCoroutine(ContentsClear());
		}
	}

	private IEnumerator ContentsClear()
	{
		yield return new WaitForSeconds(2);
		childDialogue.text = "";
	}
}
