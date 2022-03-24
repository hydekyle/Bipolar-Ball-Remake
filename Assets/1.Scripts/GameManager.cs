using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform playerT;
    [HideInInspector]
    public Player player;
    public Vector3 playerSpawnPosition;
    public List<GameObject> maps;

    void Awake()
    {
        Instance = Instance ?? this;
        Initialize();
    }

    void Initialize()
    {
        LoadMapList();
        playerSpawnPosition = playerT.position;
        player = playerT.GetComponent<Player>();
    }

    void SpawnMap()
    {
        var map = maps[Random.Range(0, maps.Count)];
        var mapGO = Instantiate(map, Vector3.zero, Quaternion.identity);
        RandomizeMap(map);
    }

    // TO-DO: Make maps pool
    void RandomizeMap(GameObject map)
    {
        foreach (Transform child in transform.Find("Random Color"))
        {
            child.GetComponent<Obstacle>().obstacleType.Value = GetRandomObstacleType();
        }
    }

    private ObstacleType GetRandomObstacleType()
    {
        var i = Random.Range(0, 3);
        if (i == 0) return ObstacleType.Blue;
        if (i == 1) return ObstacleType.Red;
        else return ObstacleType.Yellow;
    }

    void LoadMapList()
    {
        maps = Resources.LoadAll<GameObject>("Maps").ToList();
    }

    async void Start()
    {
        await UniTask.WaitUntil(() => Input.GetMouseButton(0));
        player.Spawn(playerSpawnPosition);
    }

    public void Retry()
    {
        player.Spawn(playerSpawnPosition);
    }

    void Update()
    {
        // testPrefab.transform.position = splineTest.InterpolateWorldSpace(Mathf.PingPong(Time.timeSinceLevelLoad / 5, 1f));
    }
}
