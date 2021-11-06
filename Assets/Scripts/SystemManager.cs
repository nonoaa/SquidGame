using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    static SystemManager instance = null;

    public static SystemManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("SystemMagager error! Singleton error!");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    [SerializeField]
    Player player;

    public Player Hero
    {
        get
        {
            return player;
        }
    }

    [SerializeField]
    GlassManager glassManager;

    public GlassManager GlassManager
    {
        get
        {
            return glassManager;
        }
    }

    [SerializeField]
    NetworkConnectionInfo connectionInfo = new NetworkConnectionInfo();

    public NetworkConnectionInfo ConnectionInfo
    {
        get
        {
            return connectionInfo;
        }
    }

    BaseSceneMain currentSceneMain;

    public BaseSceneMain CurrentSceneMain
    {
        set
        {
            currentSceneMain = value;
        }
    }
    public T GetCurrentSceneMain<T>()
     where T : BaseSceneMain
    {
        return currentSceneMain as T;
    }

    // Start is called before the first frame update
    void Start()
    {
        BaseSceneMain baseSceneMain = GameObject.FindObjectOfType<BaseSceneMain>();
        SystemManager.Instance.CurrentSceneMain = baseSceneMain;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
