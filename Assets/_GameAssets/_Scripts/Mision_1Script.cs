using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mision_1Script : MonoBehaviour {
    private GameObject[] enemyGameObjects;
    [SerializeField] int enemyNumber;
    [SerializeField] int enemiesKilled;
    [SerializeField] GameObject player;
    [SerializeField] GameObject menuVictory;
    [SerializeField] Text targetText;
    private int bestScore;
    private int puntuation;
    private int actuallyPuntuation;
    private int healthPlayer;

    // Use this for initialization
    void Start() {
        enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemyNumber = enemyGameObjects.Length;
    }

    // Update is called once per frame
    void Update() {
        targetText.text = "Enemigos eliminados " + enemiesKilled +" / "+ enemyNumber;

        puntuation = player.GetComponent<HeroPlayer>().GetPuntuation();
        actuallyPuntuation = puntuation * (healthPlayer / 10);
        healthPlayer = player.GetComponent<HeroPlayer>().GetHealth();
        enemiesKilled = puntuation / 100; //One enemy give 100 points. The sum of all points / 100 give us the total enemy
        bestScore = PlayerPrefs.GetInt("bestScore0");

        if (enemiesKilled == enemyNumber) { //Se completaría el objetivo de la misión
            //Despliega menu de victoria
            menuVictory.SetActive(true);
            if (actuallyPuntuation > bestScore) {
                menuVictory.transform.Find("RecordTextVictoryPanel").gameObject.SetActive(true);
                PlayerPrefs.SetInt("bestScore0", actuallyPuntuation);
                PlayerPrefs.Save();
            }
            menuVictory.transform.Find("PuntuationValueVictoryPanel").GetComponent<Text>().text = actuallyPuntuation.ToString();
            menuVictory.transform.Find("BestScoreValueVictoryPanel").GetComponent<Text>().text = PlayerPrefs.GetInt("bestScore0").ToString();

            if (Input.GetKeyUp(KeyCode.Return)){
                SceneManager.LoadScene("Level_Main_Test");
            }

        }

    }
}
