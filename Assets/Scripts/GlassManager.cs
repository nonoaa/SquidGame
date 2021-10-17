using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{
    [SerializeField]
    GlassFactory glassFactory;

    [SerializeField]
    int GlassNumber;

    Vector3 pos = new Vector3(-0.8f, 9.0f, 4.0f);

    List<Glass> glasses = new List<Glass>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GlassNumber; i++)
        {
            if (UnityEngine.Random.Range(0, 10) >= 5)
            {
                GenerateTemperedGlass(pos);
                pos.x = -pos.x;
                GenerateNormalGlass(pos);
                pos.z += 2;
            }
            else
            {
                GenerateNormalGlass(pos);
                pos.x = -pos.x;
                GenerateTemperedGlass(pos);
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
