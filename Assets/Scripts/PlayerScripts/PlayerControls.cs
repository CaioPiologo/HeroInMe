﻿using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	//REMINDER: Se alterar castingDuration, alterar a transição no animator
	public float castingDuration = 1.5f;
	public float longPressDuration = 0.4f;
	public bool inTutorial = false;

	protected IPlayerController player = null;
	private Vector2 startPos;
	private Vector2 endPos;
	private Vector2 direction;
	private Vector2 normalizedDirection;
	private Vector2 movement = new Vector2(0, 0);
	private bool attack = false;
	private bool swipeUpDetected = false;
	private bool longPressDetected = false;
	private bool isMoving = false;
	private float touchTime;
	private float castingTime = 0;
	private int castingTouch = 2;
	private float touchingBounds;

	//move o player para a direita
	public void MoveRight () {
		movement = new Vector2(1, 0);
		isMoving = true;
	}

	//move o player para a esquerda
	public void MoveLeft () {
		movement = new Vector2(-1, 0);
		isMoving = true;
	}

	//para de mover o player
	public void StopMove () {
		isMoving = false;
		movement = new Vector2(0, 0);
	}
	
	void Awake () {
		// Gets the script associated with the player controller interface
		player = GetComponent<IPlayerController>();
		touchingBounds = Screen.width/2.5f;
	}

	void Update() {

		// Track a single touch as a direction control.

		int count = Input.touchCount;

		//Reconhece no máximo 2 toques simultaneos

		if (longPressDetected && !isMoving){
			count = castingTouch+1;
		}

		if (count >= 2) {
			count = 2;
		}

		for (int i = 0; i < count; i++) {

			Touch touch = Input.GetTouch(i);
			// Handle finger movements based on touch phase.
			switch (touch.phase) {

				// Record initial touch position.
			case TouchPhase.Began:
				if(longPressDetected){
					break;
				}
				startPos = touch.position;
				touchTime = Time.time;
				break;

				//Detecta long press, onde o tempo necessário é decidido no inspetor segundo
			case TouchPhase.Stationary:
				if(!longPressDetected && Time.time - touchTime > longPressDuration && touch.position.x > touchingBounds){
					longPressDetected = true;
					castingTouch = i;
					castingTime = Time.time - touchTime;
				} else if(longPressDetected && touch.position.x > touchingBounds){
					castingTime = Time.time - touchTime;
				}
				swipeUpDetected = false;
				break;

				// Determine direction by comparing the current touch position with the initial one.
			case TouchPhase.Moved:
				//Mathf.Abs(startPos.magnitude - touch.position.magnitude) > 15 função para margem de erro, se necessário
				//TODO: aumentar tela de alcance do player
				if(touch.position.x > touchingBounds) {
					direction = touch.position - startPos;

					//Impede que o personagem pule enquando carrega a magia
					if(!longPressDetected){
						swipeUpDetected = true;
					}
				} 
				break;
				
				// Report that a direction has been chosen when the finger is lifted.
			case TouchPhase.Ended:
				if (touch.position.x > touchingBounds && Vector2.Distance(startPos, touch.position) < 40 || longPressDetected) {
					if (!swipeUpDetected) {
						attack = true;
					} else {
						swipeUpDetected = false;
					}
				}
				castingTime = 0f;
				break;
			}
		}
	}

	void FixedUpdate() {

		int count = Input.touchCount;
		int touches = count;

		//Reconhece de zero a 2 toques na tela

		if(count == 0){
			count = 1;
		} else if (count > 2) {
			count = 2;
		}

		for (int i = 0; i < count; i++) {

			//Player atacando, seja melee ou ranged
			if (attack) { 
				//ranged attack
				if (longPressDetected) {
					if(Time.time - touchTime > castingDuration){
						player.AttackRanged(direction);
					} else {
						player.CastRangedAttack(direction, 0f);
					}
					direction = new Vector2 (0, 0);
					longPressDetected = false;
					touchTime = Time.time;//Impede que o player ataque duas vezes
				//melee attack
				} else {
					player.AttackMelee ();
					if(inTutorial){
						GameObject attackTutorial = GameObject.Find("AttackTutorial(Clone)");
						Destroy(attackTutorial);
					}
				}
				attack = false;

			//Reconhece um long press
			} else if (longPressDetected){
				//Manda a direção que o player está mirando e o tempo que ficou pressionando o botao
				player.CastRangedAttack(direction, castingTime);

			//Pulo
			}else if (direction.y > 80 && normalizedDirection.x > -0.5f && normalizedDirection.x < 0.5f && !longPressDetected) {
				player.MovePlayer (new Vector2 (0, 1));
				direction = new Vector2(0, 0);
			//Movimentação
			} else {
				//Se não existir toque na tela pare o player
				if(touches == 0){
					movement = new Vector2(0, 0);
				}
				player.MovePlayer(movement);
			}
		}
	}

	public void ResetControlls(){
		attack = false;
		swipeUpDetected = false;
		longPressDetected = false;
		isMoving = false;
		movement = Vector2.zero;
		direction = Vector2.zero;
		castingTime = 0f;
		touchTime = 0f;
		castingTouch = 2;
	}
}
