using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D collider) {
		HeroRabbit rabbit = collider.gameObject.GetComponent<HeroRabbit> ();
		Orc orc = this.transform.parent.gameObject.GetComponent<Orc>();
		if(rabbit != null && !orc.dying) StartCoroutine(orc.attack(rabbit));
	}

}