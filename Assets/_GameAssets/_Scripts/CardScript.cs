using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {
	[SerializeField] GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player"){
			print("Tocaste la tarjeta");
			player.GetComponent<HeroPlayer>().TakeCard();
		}
		Destroy(this.gameObject);
	}
}
