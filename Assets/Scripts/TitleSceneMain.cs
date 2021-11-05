using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneMain : BaseSceneMain
{
    protected override void OnStart()
    {
        MyNetworkManager[] fWNetworkManagers = GameObject.FindObjectsOfType<MyNetworkManager>();
        if (fWNetworkManagers != null)
        {
            for (int i = 0; i < fWNetworkManagers.Length; i++)
            {
                //fWNetworkManagers[i].dontDestroyOnLoad = false;
                DestroyImmediate(fWNetworkManagers[i].gameObject);
            }
        }
    }

    public void OnStartButton()
    {
        PanelManager.GetPanel(typeof(NetworkConfigPanel)).Show();
    }

    public void GotoNextScene()
    {
        SceneManager.LoadScene("Loading");
    }
}