using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector3 TargetPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetPosition = SystemManager.Instance.GetCurrentSceneMain<IngameSceneMain>().Hero.transform.position;
        transform.position = TargetPosition + new Vector3(0.0f, 1.2f, 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1.0f, transform.eulerAngles.z);
    }
}
