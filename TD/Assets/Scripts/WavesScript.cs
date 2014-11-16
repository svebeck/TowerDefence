using UnityEngine;
using System.Collections.Generic;

public class WavesScript : MonoBehaviour {

	public List<GameObject> waves;

	public float interval;
	public float waveInterval;

	float _interval;

	float timer;

	int waveCount = 0;
	int enemyCount = 0;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start!");

	}
	
	// Update is called once per frame
	void Update () {
		
		if (waveCount == waves.Count)
			return;

		timer += Time.deltaTime;

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
}
