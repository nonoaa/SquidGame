using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkConnectionInfo
{
    /// <summary>
    /// 호스트로 실행 여부
    /// </summary>
    public bool Host;
    /// <summary>
    /// 클라이언트로 실행시 접속할 호스트의 IP 주소
    /// </summary>
    public string IPAddress;
    /// <summary>
    /// 클라이언트로 실행시 접속할 호스트의 Port
    /// </summary>
    public int Port;
}