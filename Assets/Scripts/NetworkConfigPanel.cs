using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkConfigPanel : BasePanel
{
    const string DefaultIPAddress = "localhost";
    const string DefaultPort = "7777";

    [SerializeField]
    InputField IPAddressInputField;

    [SerializeField]
    InputField PortInputField;

    public override void InitializePanel()
    {
        base.InitializePanel();
        // IP와 Port 입력을 기본 값으로 셋팅한다
        IPAddressInputField.text = DefaultIPAddress;
        PortInputField.text = DefaultPort;
        Close();
    }

    public void OnHostButton()
    {
        SystemManager.Instance.ConnectionInfo.Host = true;
        TitleSceneMain sceneMain = SystemManager.Instance.GetCurrentSceneMain<TitleSceneMain>();
        sceneMain.GotoNextScene();
    }

    public void OnClientButton()
    {
        SystemManager.Instance.ConnectionInfo.Host = false;

        TitleSceneMain sceneMain = SystemManager.Instance.GetCurrentSceneMain<TitleSceneMain>();

        // IP 입력 값
        if (!string.IsNullOrEmpty(IPAddressInputField.text) || IPAddressInputField.text != DefaultIPAddress)
            SystemManager.Instance.ConnectionInfo.IPAddress = IPAddressInputField.text;

        if (!string.IsNullOrEmpty(PortInputField.text) || PortInputField.text != DefaultPort)
        {
            int port = 0;
            if (int.TryParse(PortInputField.text, out port))
                SystemManager.Instance.ConnectionInfo.Port = port;
            else
            {
                Debug.LogError("OnClientButton error port = " + PortInputField.text);
                return;
            }

        }

        sceneMain.GotoNextScene();
    }
}