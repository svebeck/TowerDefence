using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int health;
    public bool targetable = true;
	public GameObject deadPrefab;
	int currentHealth;
	float dmgFlash = 1;
	Color startColor;

	// Use this for initialization
	void Start () {
		currentHealth = health;
		startColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			Destroy (gameObject);
			Instantiate(deadPrefab, gameObject.transform.position, gameObject.transform.localRotation);
		}

		renderer.material.color = Color.Lerp (Color.yellow, startColor, dmgFlash);

		dmgFlash += Time.deltaTime*1.2f;
	}

	public void TakeDamage(int dmg) {
		currentHealth -= dmg;

		dmgFlash = 0;
	}
}
