using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector3? pos;
    private Vector3 mousePos;
    private Vector3 diff;

    public float speed;
    private float rot_z;

    private bool torchlightOn = false;
    private bool leftstep = false;

    public int hp;

    public GameObject stone;
    public GameObject[] step = new GameObject[2];
    private List<GameObject> leftSteps = new List<GameObject>();
    private List<GameObject> rightSteps = new List<GameObject>();
    private GameObject torch;

    private IEnumerator stepCoroutine;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        torch = transform.Find("Torch").gameObject;
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
                stepCoroutine = FootStep();
                StartCoroutine(stepCoroutine);
            }
            pos = mousePos;
        }

        if(pos != null)
        {
            Vector2 direction = (mousePos - this.transform.position).normalized;
            transform.Translate(Vector2.up * -speed * Time.deltaTime);
            //rb2d.AddForce(direction * speed * Time.deltaTime);

            if (Vector2.Distance(this.transform.position, mousePos) < 0.1f)
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

            yield return new WaitForSeconds(0.7f);
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
        rb2d.velocity = new Vector2(0, 0);
        if(stepCoroutine != null)
        {
            StopCoroutine(stepCoroutine);
            stepCoroutine = null;
            leftstep = false;
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

        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("AA");
        }
    }
}
