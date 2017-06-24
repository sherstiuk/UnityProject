using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collider) {
		HeroRabbit rabbit = collider.gameObject.GetComponent<HeroRabbit> ();
		Orc orc = this.transform.parent.gameObject.GetComponent<Orc>();
		if(rabbit != null) {
			rabbit.jump();
			StartCoroutine(orc.death());
		}
	}

}