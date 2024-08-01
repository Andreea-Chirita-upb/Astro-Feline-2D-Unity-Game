using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //the structure will appear in the unity inspector
    [System.Serializable]
    public class Wave
    {
        // all the enemies that can be spawn
        public Enemy[] enemies;

        //how many enemies can be spawn inside of the wave
        public int count;

        
        public float timeBetweenSpawns;
    }

    public Wave[] waves;

    // the points for the enemy, ( where they can spawn)
    public Transform[] spawnPoints;
    
    public float timeBetweenWaves;

    //current wave in the game
    private Wave currentWave;
    // index od the current wave
    private int currentWaveIndex;
    private Transform player;

    // to know when the waves are finished
    private bool finishedSpawning;

    
    public GameObject boss;
    // the tranform for the boss spawn position
    private  Transform bossSpawnPoint;

    //the sound for boss entry crack effect 
    public GameObject soundCrack;

    //offset to spawn boss near by player
    public float spawnOffset;

    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bossSpawnPoint = player.transform;

        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        // the current wave 
        currentWave = waves[index]; 

        for (int i = 0; i < currentWave.count; i++)
        {
            if(player == null)
            {
                yield break;
            }
            else
            {
                //select random enemy from the array of enemies
                Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
                //select a random spawn point for that enemy
                Transform randomSpot = spawnPoints[Random.Range(0,spawnPoints.Length)];

                Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

                //check if we finish all the enemies of the wave
                if(i == currentWave.count - 1)
                {
                    finishedSpawning = true;
                }
                else
                {
                    finishedSpawning = false;
                }

                yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // check if all the enemies of the wave are dead
        if(finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            //check we have other wave left
            if(currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                //if not, the boss character will spawn, near the player
                Instantiate(boss, bossSpawnPoint.position + new Vector3(spawnOffset, spawnOffset), bossSpawnPoint.rotation);

                Instantiate(soundCrack, bossSpawnPoint.position + new Vector3(spawnOffset, spawnOffset), bossSpawnPoint.rotation);
                //the healthBar form the canva will appear for the boss health 
                healthBar.SetActive(true);

            }
        }
    }
}
