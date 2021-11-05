using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameSceneMain : BaseSceneMain
{
    [SerializeField]
    Player player;

    public Player Hero
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }

    Player otherPlayer;
    public Player OtherPlayer
    {
        get
        {
            return otherPlayer;
        }
        set
        {
            otherPlayer = value;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
