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
    int DeadCount = 0;
    bool IsJump;
    bool IsDead;
    private float timer = 2.0f;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;
        IsJump = false;
        timer = 2.0f;
        transform.position = new Vector3(0, 10, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        Rigid.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
    }

    void UpdateMove()
    {
        if (IsDead)
        {
            Anim.SetFloat("MoveSpeed", 0.0f);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                DeadCount++;
                Start();
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

}
