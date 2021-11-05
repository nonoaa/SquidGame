using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneMain : BaseSceneMain
{
    /// <summary>
    /// 다음 Scene 이동전 대기시간
    /// </summary>
    const float NextSceneIntaval = 1.0f;
    const float TextUpdateIntaval = 0.15f;
    const string LoadingTextValue = "Loading...";

    [SerializeField]
    Text LoadingText;

    int TextIndex = 6;
    float LastUpdateTime;

    float SceneStartTime;
    bool NextSceneCall = false;

    protected override void OnStart()
    {
        SceneStartTime = Time.time;
    }

    protected override void UpdateScene()
    {
        base.UpdateScene();

        float currentTime = Time.time;
        if(currentTime - LastUpdateTime > TextUpdateIntaval)
        {

            LoadingText.text = LoadingTextValue.Substring(0, TextIndex + 1);

            TextIndex++;
            if(TextIndex >= LoadingTextValue.Length)
            {
                TextIndex = 6;
            }


            LastUpdateTime = currentTime;
        }
        //
        if(currentTime - SceneStartTime > NextSceneIntaval)
        {
            if(!NextSceneCall)
                GotoNextScene();
        }
    }

    void GotoNextScene()
    {
        NetworkConnectionInfo info = SystemManager.Instance.ConnectionInfo;
        if (info.Host)
        {
            MyNetworkManager.singleton.StartHost();
        }
        else
        {
            if (!string.IsNullOrEmpty(info.IPAddress))
                MyNetworkManager.singleton.networkAddress = info.IPAddress;

            if (info.Port != MyNetworkManager.singleton.networkPort)
                MyNetworkManager.singleton.networkPort = info.Port;

            MyNetworkManager.singleton.StartClient();
        }

        NextSceneCall = true;
    }

}