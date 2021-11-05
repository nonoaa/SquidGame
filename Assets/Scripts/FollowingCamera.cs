using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 offset;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Target = SystemManager.Instance.GetCurrentSceneMain<IngameSceneMain>().Hero.transform;
        transform.position = Target.position + offset;
    }
}
