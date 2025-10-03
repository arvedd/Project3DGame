using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    public Gunfire enemyGun;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Spawn()
    {
        float spawnPointX = Random.Range(0f, 5f); // float biar smooth
        float spawnPointY = 0f;                    // spawn di ground level
        float spawnPointZ = Random.Range(0f, 5f);  // contoh range Z

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        newEnemy.GetComponent<EnemyWeaponController>().gun = enemyGun;
        newEnemy.GetComponent<Damageable>().spawner = this;
    }
}

 // Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        // transform.position += transform.forward * 1f * Time.deltaTime;