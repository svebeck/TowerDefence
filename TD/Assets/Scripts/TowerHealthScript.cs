using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerHealthScript : MonoBehaviour {
	
	public int health;
	public GameObject deadPrefab;
	int currentHealth;
	float dmgFlash = 1;
	Color startColor;
	Text score;
	GameObject gameOverScreen;

	
	// Use this for initialization
	void Start () {
		currentHealth = health;
		startColor = renderer.material.color;
		score = GameObject.FindGameObjectWithTag ("Score").GetComponent<Text>();
		score.text = currentHealth.ToString();
		gameOverScreen = GameObject.FindGameObjectWithTag ("GameOverScreen");

	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			Destroy (gameObject);
			Instantiate(deadPrefab, gameObject.transform.position, gameObject.transform.localRotation);

			RectTransform rt = gameOverScreen.GetComponent<RectTransform>();
			rt.anchoredPosition = new Vector2();
		
		}
		
		renderer.material.color = Color.Lerp (Color.yellow, startColor, dmgFlash);
		
		dmgFlash += Time.deltaTime*1.2f;
	}
	
	public void TakeDamage(int dmg) {
		currentHealth -= dmg;


		score.text = currentHealth.ToString();
		
		dmgFlash = 0;
	}
}
