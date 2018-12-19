using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mision_2Script : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] GameObject menuVictory;
    [SerializeField] Text targetText;
    private int bestScore;
    private int puntuation;
    private int actuallyPuntuation;
    private int healthPlayer;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        targetText.text = " Encuentra una salida";

        puntuation = player.GetComponent<HeroPlayer>().GetPuntuation();
        actuallyPuntuation = puntuation * (healthPlayer / 10);
        healthPlayer = player.GetComponent<HeroPlayer>().GetHealth();
        bestScore = PlayerPrefs.GetInt("bestScore1");

        if (player.GetComponent<HeroPlayer>().GetTouchDoor()) { //Se completaría el objetivo de la misión
            //Despliega menu de victoria
            menuVictory.SetActive(true);
            if (actuallyPuntuation > bestScore) {
                menuVictory.transform.Find("RecordTextVictoryPanel").gameObject.SetActive(true);
                PlayerPrefs.SetInt("bestScore1", actuallyPuntuation);
                PlayerPrefs.Save();
            }
            menuVictory.transform.Find("PuntuationValueVictoryPanel").GetComponent<Text>().text = actuallyPuntuation.ToString();
            menuVictory.transform.Find("BestScoreValueVictoryPanel").GetComponent<Text>().text = PlayerPrefs.GetInt("bestScore1").ToString();

            if (Input.GetKeyUp(KeyCode.Return)){
                SceneManager.LoadScene("Level_Main_Test");
            }

        }

    }
}
