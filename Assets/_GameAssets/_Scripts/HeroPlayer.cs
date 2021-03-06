﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroPlayer : MonoBehaviour {
    [Header("References")]
    [SerializeField] GameObject eyesPlayer;
    [SerializeField] GameObject interactuable;
    [SerializeField] GameObject panelMissions;
    [SerializeField] Text interactuableText;
    [SerializeField] Text puntuationValueText;

    [Header("Atributtes")]
    [SerializeField] const int TOTAL_HEALTH = 100;
    [SerializeField] int health = TOTAL_HEALTH;
    [SerializeField] Text healthText;
    [SerializeField] int puntuation = 0;

    [Header("Weapons")]
    private const int TOTAL_WEAPONS = 2;
    [SerializeField] Weapon[] weapons = new Weapon[TOTAL_WEAPONS];
    private int equipedWeapon = 0;

    [Header("MenuMissions")]
    [SerializeField] GameObject[] missions; // all panel misions of the menuMissions
    private int indexCurrentLevel = 1; // The initial position of the menuMissions
    private int missionsComplete = 0; // To unlock the next level
    private int[] bestScoresMission; //all best scores of the missions in an array


    private bool crounched = true;
    private string textInteractuable = "";
    private bool state; // If the player is alive or not
    private bool menuMissionsAvailable;
    private bool hasCard;
    private bool touchDoorWithCard;

    void Start() {
        interactuable.SetActive(false);
        panelMissions.SetActive(false);
        menuMissionsAvailable = false;
        bestScoresMission = new int[missions.Length];
        state = true;
        hasCard = false;
        touchDoorWithCard = false;
        missionsComplete = PlayerPrefs.GetInt("missionsComplete") +1;
    }
    void Update() {
        print (missionsComplete);
        print ("Index: "+indexCurrentLevel);
        if (IsAlive()) {
            print(PlayerPrefs.GetInt("bestScore0"));
            healthText.text = health + " / " + TOTAL_HEALTH;
            puntuationValueText.text = puntuation + "";
            ShootAction();
            //CrouchAction(); Don't use this method

            DoAvailable_MenuMissions();
            MoveInMenuMissions();
            GoToMissionInMenuMission();
            PutBestScoreInMenuMissions();

            Ray ray = new Ray(eyesPlayer.transform.position, eyesPlayer.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10)) {
                if (hit.collider.gameObject.tag == "Allie") {
                    if (hit.collider.gameObject.name == "Computer_Missions") {
                        print(hit.collider.gameObject.tag + " esta siendo INTERACTUADO");
                        interactuableText.text = "Pulsa E: Abrir Misiones";
                        if (Input.GetKeyUp(KeyCode.E)) {
                            menuMissionsAvailable = true;
                            panelMissions.SetActive(true);
                        }
                    }else if (hit.collider.gameObject.name == "SpaceDoor") {
                        print(hit.collider.gameObject.tag + " esta siendo INTERACTUADO");
                        interactuableText.text = "Pulsa E: Abrir Puerta";
                        if (Input.GetKeyUp(KeyCode.E)) {
                            if(this.hasCard == true){
                                touchDoorWithCard = true;
                            }else{

                            }
                        }
                    }
                    Debug.DrawLine(ray.origin, hit.point);
                }
            } else {
                interactuable.SetActive(false);
                menuMissionsAvailable = false;
                panelMissions.SetActive(false);
            }
        } else {
            SceneManager.LoadScene("GameOverScreen");
        }
    }
    // Write the best scores when appears the MenuMission
    private void PutBestScoreInMenuMissions() {
        for (int i = 0; i < bestScoresMission.Length; i++) {
            if (PlayerPrefs.HasKey("bestScore" + i)) {
                bestScoresMission[i] = PlayerPrefs.GetInt("bestScore" + i);
            }
        }
        for (int i = 0; i < missions.Length; i++) {
            missions[i].transform.Find("BestValue_Mission").GetComponent<Text>().text = bestScoresMission[i].ToString();
        }
    }
    private void GoToMissionInMenuMission() {
        if (Input.GetKeyUp(KeyCode.Return) && menuMissionsAvailable && missionsComplete >= indexCurrentLevel) {
            SceneManager.LoadScene("Level_" + indexCurrentLevel);
        }
    }
    //To move in the menuMission when is enabled
    private void MoveInMenuMissions() {
        if (Input.GetKeyUp(KeyCode.Tab) && menuMissionsAvailable) {
            if (indexCurrentLevel == missions.Length) {
                missions[indexCurrentLevel - 1].transform.Find("IconSelected_Mission").gameObject.SetActive(false);
                indexCurrentLevel = 1;
                missions[indexCurrentLevel - 1].transform.Find("IconSelected_Mission").gameObject.SetActive(true);
            } else {
                missions[indexCurrentLevel - 1].transform.Find("IconSelected_Mission").gameObject.SetActive(false);
                indexCurrentLevel++;
                missions[indexCurrentLevel - 1].transform.Find("IconSelected_Mission").gameObject.SetActive(true);
            }
        }
    }
    // We check when the Missions Menu is open or not
    private void DoAvailable_MenuMissions() {
        if (menuMissionsAvailable) {
            interactuable.SetActive(false);
        } else {
            interactuable.SetActive(true);
            indexCurrentLevel = 1;
        }
    }
    public void ShootAction() {
        if (Input.GetMouseButton(0)) {
            //weapons[equipedWeapon].GetComponent<Animator>().SetBool("Shooting", true);
            weapons[equipedWeapon].PullTrigger();
        } else {
            weapons[equipedWeapon].GetComponent<Animator>().SetBool("Shooting", false);
        }
    }
    private void JumpAction() {
        //For now, doesn't has any use
    }
    private void CrouchAction() {
        // Don't use, this method because it does the player inmortal
        if (Input.GetKeyDown(KeyCode.C)) {
            if (crounched) {
                GetComponent<CharacterController>().height = 2f;
                crounched = false;
            } else {
                GetComponent<CharacterController>().height = 4.6f;
                crounched = true;
            }
        }
    }
    private bool IsAlive() {
        return state;
    }
    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            state = false;
        }
    }
    // Calculated when an enemy is down
    public void EnemyKilled(int killPoints, int healthPoints) {
        puntuation += killPoints;
        RecuperateHealth(healthPoints);

    }
    private void RecuperateHealth(int healthPoints) {
        for (int i = health; i < TOTAL_HEALTH; i++) {
            health++;
            healthPoints--;
            if (healthPoints <= 0) {
                i = TOTAL_HEALTH;
            }
        }
    }
    public void TakeCard(){
        hasCard = true;
    }
    public int GetPuntuation() {
        return puntuation;
    }
    public int GetHealth() {
        return health;
    }
    public bool GethasCard()
    {
        return hasCard;
    }
    public bool GetTouchDoor()
    {
        return touchDoorWithCard;
    }
    public int GetMissionsComplete(){
        return missionsComplete;
    }
}
