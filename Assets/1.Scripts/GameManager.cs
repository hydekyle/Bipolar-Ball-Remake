using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject testPrefab;
    public Spline2DComponent splineTest;

    void Update()
    {
        testPrefab.transform.position = splineTest.InterpolateWorldSpace(Mathf.PingPong(Time.time / 5, 1f));
    }
}
