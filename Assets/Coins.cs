using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable {

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		this.CollectedHide();
	}
}
