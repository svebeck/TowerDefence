using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public int level = 0;
	bool spawnStarted = false;

    public static string SPAWNER_DEPLETED = "spawnerDepleted";

    GameObject[] spawners;

    int nrOfDepletedSpawners = 0;

	void Start()
	{
        Messenger.AddListener(SPAWNER_DEPLETED, SpawnerDepleted);
        Messenger.MarkAsPermanent(SPAWNER_DEPLETED);
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
	}

	// Update is called once per frame
	void Update () {


		if (!spawnStarted && Application.loadedLevelName == "level"+(level-1)) 
		{
			SpawnEnemies();
		}
	}

	public void LoadNextLevel()
	{
		Application.LoadLevel("level"+level);
		level++;
	}

	public void SpawnEnemies()
	{
        foreach (GameObject obj in spawners)
        {
            WavesScript ws = obj.GetComponent<WavesScript>();
            ws.StartSpawning();
        }
	}

    public void SpawnerDepleted()
    {
        nrOfDepletedSpawners++;
        
        if (nrOfDepletedSpawners == spawners.Length)
        {
            LoadNextLevel();
        }
    }
}
