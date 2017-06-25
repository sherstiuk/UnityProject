using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable {

	public int value = 1;

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		LevelController.current.addCoins(value);
		this.CollectedHide();
	}
}
