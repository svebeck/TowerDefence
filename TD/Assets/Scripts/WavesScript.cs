using UnityEngine;
using System.Collections.Generic;

public class WavesScript : MonoBehaviour {

	public List<GameObject> waves;

	public float interval;
	public float waveInterval;
    public float startDelay;

    bool isBeyondDelay = false;

	float _interval;

	float timer;

	int waveCount = 0;
	int enemyCount = 0;

	bool isSpawning = false;

    bool isAllUnitsSpawned = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

		if (!isSpawning)
            return;
		
		if (waveCount == waves.Count)
        {
            if (isAllUnitsSpawned)
                return;

            isAllUnitsSpawned = true;
            isSpawning = false;
            Messenger.Broadcast(GameManagerScript.SPAWNER_DEPLETED);
            return;
        }

		timer += Time.deltaTime;

        if (!isBeyondDelay && timer < startDelay)
            return;
        else
            isBeyondDelay = true;

		if (timer > _interval)
		{
			WaveScript ws = waves[waveCount].GetComponent<WaveScript>();

			Debug.Log ("Spawn enemy, " + enemyCount);

			if (enemyCount == ws.wave.Count)
			{
				Debug.Log("New Wave, " + waveCount);
				enemyCount = 0;
				waveCount += 1;
				_interval = waveInterval;
				timer = 0;
				return;
			}

			GameObject obj = (GameObject)Instantiate (ws.wave[enemyCount], gameObject.transform.position, gameObject.transform.localRotation);
			AgentScript ags = obj.GetComponent<AgentScript>();

			enemyCount += 1;

			timer = 0;

			_interval = interval;
		}
	}

    public void StartSpawning()
    {
        isSpawning = true;
    }


}
