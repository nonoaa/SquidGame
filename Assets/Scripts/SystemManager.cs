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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
