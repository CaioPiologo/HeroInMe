using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Player : MonoBehaviour, IPlayerController {
	
	public float speed;
	public float shotSpeed;
	public float walkingDuration;
	[HideInInspector]
	public static int coins;
	[HideInInspector]
	public int coinsInLevel = 0;
	public int hp = 3;
	public static string currentLevel = null;
	public static bool startOnCheckPoint = false;
	public bool isEquipped = true;
	public GameObject sword;
	public GameObject projectile;
	public GameObject castingHands;
	public GameObject hpUI;
	public GameObject coinUI;
	public GameObject deathAnimationBody;
	public GameObject cape;
	public AudioClip[] swoosh;
	public AudioClip[] groundFootstep;
	public AudioClip castingAudio;
	public AudioClip rangedAttackAudio;
	public AudioClip coinPickUp;
	public UnityEvent onPlayerDeath;

	private Rigidbody2D myBody;
	private Animator animator;
	private CircleCollider2D sphereCollider;
	private HPController hpController;
	private CoinController coinController;
	private bool pause = false;
	private bool facingRight = true;
	private bool isWalking = false;
	private bool isCastingRangedAttack = false;
	private bool grounded = true;
	private bool isDead = false;
	private bool inCombo = false;
	private bool canAttack = true;
	private bool castReady = false;
	private bool alreadyJumped = false;
	private int comboCount = -1;
	private float comboWindow = 0.5f;
	private float comboTimer;
	private float currentSpeed;
	private float walkingTimer;
	private GameObject deathAnimation;
	private AudioSource audioSource;
	private TypesOfGrounds currentGround;
	private static Vector3 checkPointPosition = new Vector3 (0, 3, 0);

	
	void Awake () {

		myBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sphereCollider = GetComponent<CircleCollider2D>();
		audioSource = GetComponent<AudioSource>();
//		animator.SetBool ("Grounded", false);
		currentSpeed = speed;
		walkingTimer = walkingDuration;
		hpController = hpUI.GetComponent<HPController>();
		coinController = coinUI.GetComponent<CoinController>();
		if(isEquipped == false){
			canAttack = false;
			sword.SetActive(false);
		}
	}

	void FixedUpdate(){
		//Timer until player starts running
		if (isWalking) {
			walkingTimer -= Time.deltaTime;
			if(walkingTimer <= 0 && currentSpeed < 7 ){
				walkingTimer = walkingDuration;
				currentSpeed++;
			}
		}
		//Timer for players combo
		if (comboCount >= 0) {
			comboTimer -= Time.deltaTime;
			if (comboTimer <= 0) {
				comboCount = -1;
				comboTimer = comboWindow;
				animator.SetBool ("Combo1", false);
				animator.SetBool ("Combo2", false);
				inCombo = false;
			}
		}
	}

	// Will move the player given a movement vector
	public void MovePlayer(Vector2 movement){

		if (pause == false) {
			//Horizontal movement
			if (movement.y == 0) {
				// Sets player movement
				isWalking = true;
				myBody.velocity = new Vector2 (currentSpeed * movement.x, myBody.velocity.y);
				animator.SetFloat ("Speed", Mathf.Abs (movement.x) * currentSpeed);

				if ((facingRight && movement.x < 0) || (!facingRight && movement.x > 0)) {
					Flip ();
				} else if (movement.x == 0) { // Player stoped
					isWalking = false;
					walkingTimer = walkingDuration;
					currentSpeed = speed;
				}

			} else if (grounded == true && !alreadyJumped) { //Vertical movement	
				animator.SetTrigger ("Jump");
				myBody.velocity = new Vector2 (myBody.velocity.x, 3 * speed);
				alreadyJumped = true;
			}
		}
	}

	public void playMovementAudio() {
		audioSource.priority = 200;
		audioSource.PlayOneShot(groundFootstep[(int)currentGround], 0.2f);
	}
	
	public void playAttackAudio (int index) {
		audioSource.priority = 128;
		audioSource.PlayOneShot(swoosh[index], 1f);
	}

	public void AttackMelee(){
		if (pause == false && canAttack == true) {
			comboCount++;
			if(animator.GetCurrentAnimatorStateInfo(1).IsName("PlayerAttacking1")){
				comboCount = 1;
			} else if(animator.GetCurrentAnimatorStateInfo(1).IsName("PlayerAttacking2")){
				comboCount = 2;
			}
			if (comboCount > 2 && inCombo) {

				comboCount = 3;
			} else if (comboCount > 2) {
				comboCount = 0;
				inCombo = false;
			}

			if (comboCount == 0) {
				animator.SetTrigger ("Attack");
			} else if (comboCount == 1) {
				comboTimer = animator.GetCurrentAnimatorStateInfo (1).length;
				animator.SetBool ("Combo1", true);
				inCombo = true;
			} else if (comboCount == 2) {
				comboTimer = animator.GetCurrentAnimatorStateInfo (1).length;
				animator.SetBool ("Combo2", true);
				inCombo = true;
			}
		}
	}

	public 	void AttackRanged(Vector2 direction){
		if(pause == false){
			direction.Normalize ();
			if(direction.magnitude == 0) {
				if (facingRight) {
					direction = new Vector2 (1, 0);
				} else {
					direction = new Vector2 (-1, 0);
				}
			}
			GameObject clone = (GameObject) Instantiate(projectile, castingHands.transform.position, castingHands.transform.rotation);
			Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), clone.GetComponent<Collider2D>());
			Rigidbody2D shotRigidbody = clone.GetComponent<Rigidbody2D>();
			if(!facingRight){
				Vector3 newScale = clone.transform.localScale;
				newScale.y *= -1;
				clone.transform.localScale = newScale;
			}

			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			clone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			shotRigidbody.velocity = new Vector2(direction.x*shotSpeed, direction.y*shotSpeed);
			CastRangedAttack(Vector2.zero, 0f);
			animator.SetTrigger("Shot");
			animator.SetFloat("Casting", 0f);
		}
	}

	public void CastRangedAttack (Vector2 direction, float duration) {

		if (isDead) {
			return;
		}
		if(pause == false && grounded == true){
			Cloth capeCloth = cape.GetComponent<Cloth>();

			if(duration == 0f){
				capeCloth.externalAcceleration = new Vector3(0, 0, 0);
			} else if(facingRight){
				capeCloth.externalAcceleration = new Vector3(Random.Range(-100, -50), 0, 0);
			} else {
				capeCloth.externalAcceleration = new Vector3(Random.Range(50, 100), 0, 0);
			}


			animator.SetFloat("Casting", duration);

			//Vira o player para o sentido em que está mirando
			if ((facingRight && direction.x < 0) || (!facingRight && direction.x > 0)) {
				Flip ();
			}

			//Para o player caso esteja em movimento
			if ( isWalking && grounded == true) {
				MovePlayer(new Vector2(0, 0));
			}

			if(animator.GetCurrentAnimatorStateInfo(1).IsName("PlayerReadyToShoot")){
				float angle = Mathf.Atan2(direction.y, direction.x);
				float sin = Mathf.Sin(angle);
				animator.SetFloat("CastAngle", sin);
				castReady = true;
			}
		}
	}

	public void playCastAudio(){
		audioSource.priority = 200;
		audioSource.volume = 0.4f;
		audioSource.loop = true;
		audioSource.clip = castingAudio;
		audioSource.Play();
	}
	
	public void playRangedAttackAudio() {
		audioSource.priority = 200;
		audioSource.volume = 1f;
		audioSource.Stop();
		audioSource.loop = false;
		
		if(castReady) {
			audioSource.PlayOneShot(rangedAttackAudio, 0.6f);
		}
		audioSource.priority = 128;
		castReady = false;
	}

	// Flips player sprites scale
	void Flip (){
		facingRight = !facingRight;
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}
	
	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.CompareTag("PickUp")) {
			this.EquipItem(col.gameObject.GetComponent<SpriteRenderer>().sprite, col.gameObject.GetComponent<SwordScript>().damage);
			GameObject swipeUpTutorial = GameObject.Find("SwipeUpTutorial(Clone)");
			Destroy(swipeUpTutorial);
			Destroy(col.gameObject);
		} else if (col.gameObject.CompareTag("DeathTrigger") && !isDead) {
			DeathAnimation();
			Invoke ("Resurrect", 3);
		} else if(col.transform.CompareTag("Platform")) {
			transform.parent = col.transform;
		} else if(col.gameObject.CompareTag("Coin")){
			coins++;
			coinsInLevel++;
			audioSource.PlayOneShot(coinPickUp, 1f);
			coinController.UpdateCoinsUI(coins);
			Destroy(col.gameObject);
			PlayerPrefs.SetInt("Coins", coins);
			PlayerPrefs.Save();
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "ground" || col.gameObject.CompareTag ("Platform")) {
			grounded = true;
			animator.SetBool ("Grounded", grounded);
			alreadyJumped = false;
			StopCoroutine("JumpTime");
			currentGround = col.gameObject.GetComponent<TypeOfGround>().getType();
			if(col.transform.CompareTag("Platform")) {
				transform.parent = col.transform;
			}
		} else if (col.gameObject.CompareTag("DeathTrigger") && !isDead) {
			DeathAnimation();
			Invoke ("Resurrect", 3);
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "ground" || col.gameObject.CompareTag("Platform")) {
			if(!col.IsTouching(sphereCollider)){
				StartCoroutine("JumpTime");
				animator.SetBool("Grounded", false);
				if (col.transform.CompareTag ("Platform")) {
					transform.SetParent(null);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("DeathTrap") && !isDead){
			DeathAnimation();
			Invoke ("Resurrect", 3);
		}
	}

	public void EquipItem (Sprite sprite, int weaponPower) {
		sword.SetActive(true);
		canAttack = true;
		SpriteRenderer swordSR = sword.GetComponent<SpriteRenderer>();
		swordSR.sprite = sprite;
		sword.GetComponent<SwordScript>().damage = weaponPower;
	}

	public void Pause(){
		MovePlayer(Vector2.zero);
		pause = true;
		//REMINDER: Setar os triggers do animator
		animator.SetFloat("Speed", 0f);
		animator.SetFloat("Casting", 0f);
		animator.SetFloat("CastAngle", 0f);
		animator.SetBool("Combo1", false);
		animator.SetBool("Combo2", false);
	}

	public void Unpause(){
		pause = false;
	}

	public void TakeDamage(int damage) {
		hp -= damage;

		animator.SetTrigger("Damaged");

		hpController.UpdateHP(hp);
		
		if(hp <= 0 && !isDead) {
			
			DeathAnimation();
			Invoke ("Resurrect", 3);
		}
	}

	//Função teste para testar o recebimento teste de dano testável
//	public void takeDamageTest() {
//		hp -= 1;
//		
//		hpController.UpdateHP(hp);
//		
//		if(hp <= 0 && !isDead) {
//
//			DeathAnimation();
//			Invoke ("Resurrect", 3);
//		}
//	}

	private void DeathAnimation() {
		isDead = true;
		
		//REMINDER: Setar os triggers do animator
		Debug.Log("morri");
		//onPlayerDeath.Invoke();
		animator.SetTrigger("JustDied");
		GetComponent<PlayerControls>().ResetControlls();

		deathAnimation = (GameObject) Instantiate(deathAnimationBody, gameObject.transform.position, gameObject.transform.rotation);
		deathAnimation.transform.localScale = gameObject.transform.localScale;
		transform.SetParent(null);
		gameObject.SetActive(false);
	}

	private void Resurrect () {
		isDead = false;
		Destroy(deathAnimation);
		gameObject.SetActive(true);
		transform.position = checkPointPosition;
		hp = 3;
		hpController.UpdateHP(hp);
		onPlayerDeath.Invoke();
		this.Reset();
	}

	public static void RefreshCheckPointPos (Vector3 newPos) {
		checkPointPosition = newPos;

		PlayerPrefs.SetFloat("CheckPointX" ,newPos.x);
		PlayerPrefs.SetFloat("CheckPointY" ,newPos.y);
		PlayerPrefs.SetFloat("CheckPointZ", newPos.z);
		PlayerPrefs.Save();
	}

	private void Reset(){
		animator.SetFloat("Speed", 0f);
		animator.SetFloat("Casting", 0f);
		animator.SetFloat("CastAngle", 0f);
		animator.SetBool("Combo1", false);
		animator.SetBool("Combo2", false);		
		transform.SetParent(null);
		pause = false;
		isWalking = false;
		isCastingRangedAttack = false;
		grounded = true;
		isDead = false;
		inCombo = false;
		comboCount = -1;
		comboWindow = 0.5f;
	}

	public void RefreshCurrentPosition(Vector3 newPos) {
		transform.position = newPos;
	}

	void OnLevelWasLoaded(){
		if(startOnCheckPoint){
			this.RefreshCurrentPosition(checkPointPosition);
		}
		coinController.UpdateCoinsUI(coins);
		coinsInLevel = 0;
	}

	private IEnumerator JumpTime(){
		yield return new WaitForSeconds(0.2f);
		grounded = false;
	}
}