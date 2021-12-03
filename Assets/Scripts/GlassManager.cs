using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GlassManager : NetworkBehaviour
{
    [SerializeField]
    GlassFactory glassFactory;

    [SerializeField]
    int GlassNumber;

    [SyncVar]
    int IsTemperedGlass = 0;

    Vector3 pos = new Vector3(-0.8f, 9.0f, 4.0f);

    List<Glass> glasses = new List<Glass>();

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            for (int i = 0; i < GlassNumber; i++)
            {
                if (UnityEngine.Random.Range(0, 10) >= 5)
                {
                    IsTemperedGlass = IsTemperedGlass << 1;
                    IsTemperedGlass += 1;
                    Debug.Log("Left");
                }
                else
                {
                    IsTemperedGlass = IsTemperedGlass << 1;
                    Debug.Log("Right");
                }
            }
        }
        for (int i = 0; i < GlassNumber; i++)
        {
            if ((IsTemperedGlass & (1 << GlassNumber - i - 1)) != 0)
            {
                GenerateTemperedGlass(pos);
                pos.x = -pos.x;
                GenerateNormalGlass(pos);
                pos.x = -pos.x;
                pos.z += 2;
            }
            else
            {
                GenerateNormalGlass(pos);
                pos.x = -pos.x;
                GenerateTemperedGlass(pos);
                pos.x = -pos.x;
                pos.z += 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GenerateTemperedGlass(Vector3 position)
    {
        GameObject go = glassFactory.Load(GlassFactory.TemperedGlassPath);
        if (!go)
        {
            Debug.LogError("GenerateTemperedGlass error!");
            return false;
        }

        go.transform.position = position;

        Glass glass = go.GetComponent<Glass>();

        glasses.Add(glass);
        return true;
    }

    public bool GenerateNormalGlass(Vector3 position)
    {
        GameObject go = glassFactory.Load(GlassFactory.NormalGlassPath);
        if (!go)
        {
            Debug.LogError("GenerateNormalGlass error!");
            return false;
        }

        go.transform.position = position;

        Glass glass = go.GetComponent<Glass>();

        glasses.Add(glass);
        return true;
    }
}
