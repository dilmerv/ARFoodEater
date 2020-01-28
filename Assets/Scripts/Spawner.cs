using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float spawnInMinSeconds = 1.0f;

    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float spawnInMaxSeconds = 1.0f;

    private float generatedSeconds;

    [SerializeField]
    private float destroyInSeconds = 20.0f;

    private float spawnTimer;

    void Awake() 
    {
        spawnTimer = generatedSeconds = Random.Range(spawnInMinSeconds, spawnInMaxSeconds);    
    }

    void Update()
    {
        if(spawnTimer >= generatedSeconds)
        {
            var go = Instantiate(prefab, transform.position, prefab.transform.rotation);
            Destroy(go, destroyInSeconds);

            spawnTimer = 0;
            generatedSeconds = Random.Range(spawnInMinSeconds, spawnInMaxSeconds);    
        }    
        else
        {
            spawnTimer += Time.deltaTime * 1.0f;
        }
    }
}
