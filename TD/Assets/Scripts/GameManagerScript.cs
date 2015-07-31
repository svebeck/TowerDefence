using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public int level = 0;
    public int startCredits = 300;
    private int credits = 0;
	bool spawnStarted = false;

    public static string SPAWNER_DEPLETED = "spawnerDepleted";

    int nrOfDepletedSpawners = 0;
    bool checkForEnemiesLeft = false;


	void Start()
	{
        Messenger.AddListener(SPAWNER_DEPLETED, SpawnerDepleted);
        Messenger.MarkAsPermanent(SPAWNER_DEPLETED);

        AddCredits(startCredits);
	}

	// Update is called once per frame
	void Update () {
        if (checkForEnemiesLeft)
        {
            if (GameObject.FindGameObjectsWithTag("Agent").Length == 0)
            {
                LoadNextLevel();
                ResetCredits();
                checkForEnemiesLeft = false;
            }
        }

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
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject obj in spawners)
        {
            WavesScript ws = obj.GetComponent<WavesScript>();
            ws.StartSpawning();
        }
	}

    public void SpawnerDepleted()
    {
        nrOfDepletedSpawners++;
        
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
    
        if (nrOfDepletedSpawners == spawners.Length)
        {
            checkForEnemiesLeft = true;
        }
    }

    private void ResetCredits()
    {
        this.credits = startCredits;
        UpdateCreditText();
    }

    public void AddCredits(int credits) 
    {
        this.credits += credits;
        UpdateCreditText();
    }

    public bool TryRemoveCredits(int credits)
    {
        Debug.Log("current credits " + this.credits);
        if (credits > this.credits)
        {
            return false;
        }

        Debug.Log("try remove credits true!");
        this.credits -= credits;

        UpdateCreditText();
        return true;
    }

    private void UpdateCreditText()
    {
        GameObject.Find("CreditText").GetComponent<Text>().text = "$" + this.credits.ToString();
    }
}
