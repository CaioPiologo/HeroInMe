﻿using UnityEngine;
using System.Collections;

public interface IPlayerController {

	//Moves Player according to inputs
	void MovePlayer(Vector2 movement);
	
	//Make Player attack in melee range
	void AttackMelee();

	//Make Player attack in distance
	void AttackRanged(Vector2 direction);

	//Make Player aim ranged attack
	void CastRangedAttack (Vector2 direction, float duration);

	//Makes Player equip item
	void EquipItem (Sprite sprite, int weaponPower);

	//Pauses Player Movement
	void Pause ();

	//Make player take damage
	void TakeDamage (int damage);

}
