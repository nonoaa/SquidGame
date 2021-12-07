using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField]
    [SyncVar]
    Vector3 MoveVector = Vector3.zero;

    [SerializeField]
    float Speed;

    [SerializeField]
    float JumpPower;

    [SerializeField]
    NetworkIdentity NetworkIdentity = null;

    [SerializeField]
    [SyncVar]
    bool Host = false;  // Host 플레이어인지 여부

    int IsEnd = 0;

    CapsuleCollider CapsuleCollider;
    Rigidbody Rigid;
    Animator Anim;
    InputController inputController = new InputController();

    bool IsJump;
    bool IsDead;
    int DeathCount = 0;
    private float timer;
    public Text myDeathCount;
    CheckWinner WinnerText;
    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
        myDeathCount = GameObject.Find("DeathCount").GetComponent<Text>();
        WinnerText = GameObject.Find("Winner").GetComponent<CheckWinner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IngameSceneMain inGameSceneMain = SystemManager.Instance.GetCurrentSceneMain<IngameSceneMain>();

        if (isLocalPlayer)
            inGameSceneMain.Hero = this;
        else
            inGameSceneMain.OtherPlayer = this;

        if (isServer && isLocalPlayer)
        {
            Host = true;
            RpcSetHost();
        }

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
        if (!isLocalPlayer)
            return;

        UpdateInput();
        UpdateMove();
        UpdateText();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("OnStartClient");
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("OnStartLocalPlayer");
    }

    [ClientRpc]
    public void RpcStart()
    {
        Start();
    }

    [Command]
    public void CmdStart()
    {
        Start();
    }

    void UpdateMove()
    {
        if (IsDead)
        {
            Anim.SetFloat("MoveSpeed", 0.0f);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                if (isServer)
                    RpcStart();
                else
                {
                    CmdStart();
                    Start();
                }
                DeathCount++;
            }
            return;
        }
        if (isServer)
        {
            RpcMove(MoveVector);        // Host 플레이어인경우 RPC로 보내고
        }
        else
        {
            CmdMove(MoveVector);        // Client 플레이어인경우 Cmd로 호스트로 보낸후 자신을 Self 동작
            if (isLocalPlayer)
            {
                if (MoveVector.sqrMagnitude == 0)
                {
                    Anim.SetFloat("MoveSpeed", 0.0f);
                    return;
                }
                Anim.SetFloat("MoveSpeed", Speed);
                transform.LookAt(transform.position + MoveVector);
                transform.position += MoveVector;
            }
        }
    }

    [ClientRpc]
    public void RpcMove(Vector3 moveVector)
    {
        if (moveVector.sqrMagnitude == 0)
        {
            Anim.SetFloat("MoveSpeed", 0.0f);
            base.SetDirtyBit(1);
            return;
        }
        MoveVector = moveVector; 
        Anim.SetFloat("MoveSpeed", Speed);
        transform.LookAt(transform.position + moveVector);
        transform.position += moveVector;
        base.SetDirtyBit(1);
        MoveVector = Vector3.zero; // 타 플레이어가 보낸경우 Update를 통해 초기화 되지 않으므로 사용후 바로 초기화
    }

    [ClientCallback]
    public void UpdateInput()
    {
        inputController.UpdateInput();
    }

    [ClientRpc]
    public void RpcSetHost()
    {
        Host = true;
        base.SetDirtyBit(1);
    }

    [Command]
    public void CmdMove(Vector3 moveVector)
    {
        if (moveVector.sqrMagnitude == 0)
        {
            Anim.SetFloat("MoveSpeed",0.0f);
            base.SetDirtyBit(1);
            return;
        }
        MoveVector = moveVector;
        Anim.SetFloat("MoveSpeed",Speed);
        transform.LookAt(transform.position + moveVector);
        transform.position += moveVector;
        base.SetDirtyBit(1);
        MoveVector = Vector3.zero; // 타 플레이어가 보낸경우 Update를 통해 초기화 되지 않으므로 사용후 바로 초기화
    }

    [ClientRpc]
    public void RpcJump()
    {
        Rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }

    [Command]
    public void CmdJump()
    {
        Rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }

    public void ProcessInput(Vector3 moveDirection)
    {
        if (moveDirection.y == 1)
        {
            if (!IsJump)
            {
                if (isServer)
                {
                    RpcJump();
                }
                else
                {
                    CmdJump();
                    if (isLocalPlayer)
                        Rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                }
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
            Rigid.freezeRotation = true;
            IsJump = false;
        }
        if (collision.gameObject.tag == "DeadFloor")
        {
            IsDead = true;
            Anim.SetBool("Grounded", true);
            Rigid.freezeRotation = false;
            transform.eulerAngles = new Vector3(UnityEngine.Random.Range(-20, 20), transform.eulerAngles.y, UnityEngine.Random.Range(-20, 20));
        }
        if (collision.gameObject.tag == "EndFloor")
        {
            Anim.SetBool("Grounded", true);
            Rigid.freezeRotation = true;
            IsJump = false;
            if (WinnerText.GetWinner() == 0)
            {
                if (isServer)
                {
                    if (isLocalPlayer)
                        WinnerText.SetWinner(1);
                    else
                        WinnerText.SetWinner(2);
                }
                else if (!isServer)
                {
                    if (isLocalPlayer)
                        WinnerText.SetWinner(2);
                    else
                        WinnerText.SetWinner(1);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Anim.SetBool("Grounded", false);
            IsJump = true;
        }
        if (collision.gameObject.tag == "EndFloor")
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
