using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    Vector3 MoveVector = Vector3.zero;

    [SerializeField]
    float Speed;

    [SerializeField]
    float JumpPower;

    CapsuleCollider CapsuleCollider;
    Rigidbody Rigid;
    Animator Anim;

    bool IsJump;
    bool IsDead;
    int DeathCount = 0;
    private float timer = 2.0f;
    public Text myDeathCount;
    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myDeathCount = GameObject.Find("DeathCount").GetComponent<Text>();
        IsDead = false;
        IsJump = false;
        timer = 2.0f;
        transform.position = new Vector3(0, 10.5f, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        Rigid.freezeRotation = true;
        Anim.SetBool("Grounded", false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        UpdateText();
    }

    void UpdateMove()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, 9, 0);
        }
        if (IsDead)
        {
            Anim.SetFloat("MoveSpeed", 0.0f);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Start();
                DeathCount++;
            }
            return;
        }
        if (MoveVector.sqrMagnitude == 0)
        {
            Anim.SetFloat("MoveSpeed", 0.0f);
            return;
        }
 
        Anim.SetFloat("MoveSpeed", Speed);
        transform.LookAt(transform.position + MoveVector);
        transform.position += MoveVector;
    }

    public void ProcessInput(Vector3 moveDirection)
    {
        if (moveDirection.y == 1)
        {
            if (!IsJump)
            {
                Rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
        }
        else
            MoveVector = moveDirection * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Anim.SetBool("Grounded", true);
            IsJump = false;
        }
        if (collision.gameObject.tag == "DeadFloor")
        {
            IsDead = true;
            Anim.SetBool("Grounded", true);
            Rigid.freezeRotation = false;
            transform.eulerAngles = new Vector3(UnityEngine.Random.Range(-20, 20), transform.eulerAngles.y, UnityEngine.Random.Range(-20, 20));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Anim.SetBool("Grounded", false);
            IsJump = true;
        }
    }
    private void UpdateText()
    {
        myDeathCount.text = "Death : " + DeathCount.ToString();
    }
}
