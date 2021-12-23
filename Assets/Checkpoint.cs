using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        SaveCheckpoint();
    }

    void SaveCheckpoint()
    {
        GameManager.Instance.playerSpawnPosition = transform.GetChild(0).position;
        gameObject.SetActive(false);
    }
}
