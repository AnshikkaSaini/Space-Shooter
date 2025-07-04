using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableOBject/PowerUpSpawner", fileName = "Spawner")]
public class ScriptableObjectExample : ScriptableObject
{
    public GameObject[] powerUp;
    public int spawnThreshold;
    public int spawnDelayMS;

    public void SpawnPowerUp(Vector3 spawnPos)
    {
        var randomChance = Random.Range(0, 100);
        if (randomChance > spawnThreshold) SpawnDelay(spawnPos);
    }

    private async void SpawnDelay(Vector3 spawnPos)
    {
        await Task.Delay(spawnDelayMS);

        var randomPowerUp = Random.Range(0, powerUp.Length);

        Instantiate(powerUp[randomPowerUp], spawnPos, Quaternion.identity);
    }
}