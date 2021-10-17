using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFactory : MonoBehaviour
{
    public const string NormalGlassPath = "Prefabs/NormalGlass";
    public const string TemperedGlassPath = "Prefabs/TemperedGlass";

    Dictionary<string, GameObject> GlassFileCache = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Load(string resourcePath)
    {
        GameObject go = null;

        if (GlassFileCache.ContainsKey(resourcePath))
        {
            go = GlassFileCache[resourcePath];
        }
        else
        {
            go = Resources.Load<GameObject>(resourcePath);
            if (!go)
            {
                Debug.LogError("Load Error! path = " + resourcePath);
                return null;
            }

            GlassFileCache.Add(resourcePath, go);
        }

        GameObject InstancedGO = Instantiate<GameObject>(go);

        return InstancedGO;
    }
}

