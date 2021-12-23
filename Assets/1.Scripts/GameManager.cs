using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject testPrefab;
    public Transform playerT;
    public Player player;
    public Spline2DComponent splineTest;
    public Vector3 playerSpawnPosition;

    void Awake()
    {
        Instance = Instance ?? this;
        playerSpawnPosition = playerT.position;
        player = playerT.GetComponent<Player>();
    }

    async void Start()
    {
        await UniTask.DelayFrame(60);
        player.Spawn(playerSpawnPosition);
    }

    public void Retry()
    {
        player.Spawn(playerSpawnPosition);
    }

    void Update()
    {
        testPrefab.transform.position = splineTest.InterpolateWorldSpace(Mathf.PingPong(Time.timeSinceLevelLoad / 5, 1f));
    }
}
