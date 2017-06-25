using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

	public enum Color {
		Blue,
		Green,
		Red,
	}
	public Color color = Color.Red;

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		int colorCode = 0;
		switch (color) {
			case Color.Blue: colorCode = 1; break;
			case Color.Green: colorCode = 2; break;
			case Color.Red: colorCode = 3; break;
		}
		LevelController.current.addCrystal(colorCode);
		this.CollectedHide();
	}
}
