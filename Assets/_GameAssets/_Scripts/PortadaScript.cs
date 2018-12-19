using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortadaScript : MonoBehaviour {
	[SerializeField] Animator musicAnimator;
	[SerializeField] Animator blackImageAnimator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
        {
			GetComponent<AudioSource>().Play();
			musicAnimator.SetBool("PulsedEnter", true);
			blackImageAnimator.SetBool("PulsedEnter", true);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Level_Main_Test");
    }
}
