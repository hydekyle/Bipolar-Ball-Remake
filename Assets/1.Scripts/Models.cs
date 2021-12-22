using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityObservables;

public enum ObstacleType { Red, Blue, Yellow }
[System.Serializable]
public class ObstacleTypeObs : Observable<ObstacleType> { };
[System.Serializable]
public class ColorObs : Observable<Color> { };