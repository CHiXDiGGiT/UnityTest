using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    [SerializeField]
    float speed;

    [SerializeField]
    float jump;

    [SerializeField]
    GameObject eff_smoke;

    bool isGround = true;
    bool isClear;
    float eff_interval;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        eff_interval += Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                isGround = false;
                transform.SetParent(null);

                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                animator.SetBool("IsAir", true);
            }
        }
    }

    private void FixedUpdate()
    {
        // ①落ちたら復活
        if (transform.position.y < -100)
        {

        }


        Vector2 v;
        v.x = -Input.GetAxis("Horizontal");
        v.y =  Input.GetAxis("Vertical");
        if (v.x != 0.0f || v.y != 0.0f)
        {
            if (v.magnitude > 1)
            {
                v = v.normalized;
            }

            transform.rotation = Quaternion.identity;
            transform.Rotate(0, Mathf.Atan2(v.y, v.x) * 360 / 6.283f, 0);
            rb.velocity = new Vector3(v.y * speed, rb.velocity.y, v.x * speed);

            if (isGround)
            {
                if (eff_smoke != null && eff_interval > 0.1f)
                {
                    GameObject obj = Instantiate(eff_smoke, transform.position, Quaternion.identity);
                    obj.GetComponentInChildren<ParticleSystem>().transform.localScale = Vector3.one * v.magnitude;
                    Destroy(obj, 2.0f);
                    eff_interval -= 0.1f;
                }
            }
        }

        if (isGround)
        {
            animator.SetFloat("Speed", v.magnitude);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ②クリア判定
        if(collision.gameObject.tag == "Finish")
        {





        }


        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "cube")
        {
            OnCollisionStay(collision);

            if (eff_smoke != null)
            {
                GameObject obj;
                obj = Instantiate(eff_smoke, transform.position, Quaternion.identity);
                Destroy(obj, 2.0f);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "cube")
        {
            if (!isGround)
            {
                isGround = true;
                animator.SetBool("IsAir", false);

                if (collision.gameObject.tag == "ground") transform.SetParent(collision.transform.parent);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "cube")
        {
            isGround = false;
            animator.SetBool("IsAir", true);

            transform.SetParent(null);
        }
    }

    void OnClear()
    {
        // ③クリア演出
        for (int i = 0; i < 10; i++)            // 「10」を変えると花火の数が変わる
        {
            Invoke("Burst", (i + 1) * 0.5f);    // 「0.5f」を変えると花火の間隔が変わる
        }
    }

    void Burst()
    {
        GameObject obj;
        obj = (GameObject)Instantiate(Resources.Load("Eff_Burst"),
            new Vector3(
                Random.Range(transform.position.x + 2.0f, transform.position.x + 15.0f),
                Random.Range(1.0f, 4.0f),
                Random.Range(-10.0f, 10.0f)),
            Quaternion.identity);

        Destroy(obj, 2.0f);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.yellow;
        style.fontSize = 64;

        if (transform.position.y < -30.0f)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Try Again!!", style);
        }
    }
}
