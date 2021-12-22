using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Obstacle : MonoBehaviour
{
    public ObstacleTypeObs obstacleType = new ObstacleTypeObs() { Value = ObstacleType.Red };
    public SpriteRenderer mySpriteRenderer;
    public ScriptableColors tableColors;

    void Start()
    {
        OnObstacleTypeChanged();
        obstacleType.OnChanged += OnObstacleTypeChanged;
    }

    void OnObstacleTypeChanged()
    {
        Color obstacleColor;
        string tag;
        switch (obstacleType.Value)
        {
            case ObstacleType.Red: obstacleColor = tableColors.red1; tag = "Red"; break;
            case ObstacleType.Blue: obstacleColor = tableColors.blue1; tag = "Blue"; break;
            default: obstacleColor = tableColors.yellow1; tag = "Yellow"; break;
        }
        mySpriteRenderer.color = obstacleColor;
        transform.tag = tag;
        foreach (Transform t in transform) t.tag = tag;
    }
}
