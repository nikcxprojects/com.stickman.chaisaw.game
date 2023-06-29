using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChainsawController : MonoBehaviour
{

    [SerializeField] private GameObject chainsawPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnOffset;
    [SerializeField] private AudioClip spawnClip;

    private void Start()
    {
        StartCoroutine(SpawnChainsaws());
    }
    
    private IEnumerator SpawnChainsaws()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(chainsawPrefab, GetRandomPos(), Quaternion.identity);
            AudioManager.getInstance().PlayAudio(spawnClip);
        }
    }

    private Vector2 GetRandomPos()
    {
        var x = Random.Range((int) DisplayManager.BottomY + spawnOffset, (int) DisplayManager.TopY - spawnOffset);
        var y = Random.Range((int) DisplayManager.LeftX + spawnOffset, (int) DisplayManager.RightX - spawnOffset);
        return new Vector2(x, y);
    }
}
