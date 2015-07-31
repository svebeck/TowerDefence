using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

	public float health;
    public bool targetable = true;
    public int credits = 10;
	public GameObject deadPrefab;
	float currentHealth;
	float dmgFlash = 1;
	Color startColor;

	// Use this for initialization
	void Start () {
		currentHealth = health;
		startColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().AddCredits(credits);
			Destroy (gameObject);
			Instantiate(deadPrefab, gameObject.transform.position, gameObject.transform.localRotation);
		}

		GetComponent<Renderer>().material.color = Color.Lerp (Color.yellow, startColor, dmgFlash);

		dmgFlash += Time.deltaTime*1.2f;
	}

	public void TakeDamage(float dmg) {
		currentHealth -= dmg;

		dmgFlash = 0;
	}
}
