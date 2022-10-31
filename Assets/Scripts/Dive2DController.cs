using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Dive2DController : MonoBehaviour
//{
//
//    public float horizontalMoveSpeed;
//    public float verticalMoveSpeed;
//    public float speedMod;
//
//    public bool isUnderwater;
//    public GameObject seaTop;
//
//    public float oxygenCurrentCapacity;
//
//    public float currentDepth;
//    public float currentPressure;
//
//    public GameObject SpawnPoints;
//    public bool isShopOpen;
//
//    private PlayerLevelManager playerLevelManager;
//    private PlayerInventoryManager playerInventoryManager;
//
//    public GameObject respawnLocation;
//    private Vector2 respawnVector;
//    private Animator myAnim;
//	private Rigidbody2D myRigidBody;
//
//    private Collider2D currentlyCollidingFish;
//    public bool isCollidingWithFish;
//
//    void Start()
//    {
//        myAnim = GetComponent<Animator> ();
//		myRigidBody = GetComponent<Rigidbody2D> ();	
//        playerLevelManager = this.gameObject.GetComponent<PlayerLevelManager>();
//        playerInventoryManager = this.gameObject.GetComponent<PlayerInventoryManager>();
//        respawnVector = new Vector2 (respawnLocation.transform.position.x, 0.2f);
//    }
//
//    void Breath() { 
//        if(isUnderwater) {
//            oxygenCurrentCapacity -= playerLevelManager.oxygenRemovalRate * currentPressure;
//            if(oxygenCurrentCapacity < 0){
//                OutOfAir();
//            }
//        } else {
//            oxygenCurrentCapacity += playerLevelManager.oxygenReplenishRate;
//            if(oxygenCurrentCapacity >= playerLevelManager.oxygenMaxCapacity) {
//                oxygenCurrentCapacity = playerLevelManager.oxygenMaxCapacity;
//            }
//        }
//    }
//
//    void CheckUnderwater(){
//        if(transform.position.y >= 0) {
//            isUnderwater = false;
//            foreach(Transform child in SpawnPoints.transform) {
//                child.GetComponent<FishSpawnPoint>().CreateFish();
//            }
//        } else {
//            isUnderwater = true;
//        }
//    }
//
//    void UpdateDivingFactors() { 
//        currentDepth = -this.transform.position.y / 2;
//        currentPressure = 1 + (currentDepth / 10);
//        if(currentPressure < 1) {
//            currentPressure = 1;
//        }
//    }
//
//    void OutOfAir() {
//        transform.position = respawnVector;
//        playerLevelManager.currentShellCount /= 2;
//        playerInventoryManager.RemoveAllFishFromInventory();
//        oxygenCurrentCapacity = playerLevelManager.oxygenMaxCapacity;
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        CheckUnderwater();
//        UpdateDivingFactors();
//        Breath();
//
//        if(!isShopOpen) {
//            float moveVertical = Input.GetAxis ("Vertical");
//            if(!isUnderwater && moveVertical > 0) {
//                moveVertical = 0;
//            }
//
//
//            float moveHorizontal = Input.GetAxis ("Horizontal");
//            myRigidBody.velocity = new Vector3 (moveHorizontal * speedMod, myRigidBody.velocity.y, 0f);
//
//            if (moveHorizontal < 0f) {
//                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
//            } else 
//              if (moveHorizontal > 0f) {
//                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
//            }
//            
//            if (moveVertical != 0f) {
//			    myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, moveVertical * verticalMoveSpeed, 0f);
//		    } else {
//			    myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, moveVertical * verticalMoveSpeed, 0f);
//            }
//            
//            currentDepth = -this.transform.position.y;
//
//            Vector3 horizontalMovement = Vector3.right * moveHorizontal * horizontalMoveSpeed * Time.deltaTime;
//            Vector3 verticalMovement = Vector3.up * moveVertical * verticalMoveSpeed * Time.deltaTime;
//
//            //myRigidBody.velocity = new Vector3 (moveHorizontal * speedMod, myRigidBody.velocity.y, 0f);
//
//            transform.Translate(horizontalMovement);
//            transform.Translate(verticalMovement);
//        }
//
//        myAnim.SetFloat ("Speed", Mathf.Abs(myRigidBody.velocity.x));
//    }
//
//    void ControllerManager() 
//    {
//		if (Input.GetAxisRaw ("Horizontal") > 0f) {
//			transform.localScale = new Vector3(1f,1f,1f);
//			movePlayer ();
//		} else if (Input.GetAxisRaw ("Horizontal") < 0f) {			
//			transform.localScale = new Vector3(-1f,1f,1f);
//			movePlayer ();
//		} else if (Input.GetAxisRaw ("Vertical") > 0f) {
//			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, verticalMoveSpeed, 0f);
//		} else if (Input.GetAxis ("Vertical") < 0f) {
//			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, -verticalMoveSpeed, 0f);
//		}
//    }
//
//	void movePlayer(){
//		if (transform.localScale.x == 1) {
//			myRigidBody.velocity = new Vector3 (horizontalMoveSpeed + speedMod, myRigidBody.velocity.y, 0f);	
//		} else {
//			myRigidBody.velocity = new Vector3 (- (horizontalMoveSpeed + speedMod), myRigidBody.velocity.y, 0f);
//		}	
//	}
//
//    public void AddToInventory() {
//        if(playerInventoryManager.AddToInventory(currentlyCollidingFish.gameObject.GetComponent<Fish>())) {
//            currentlyCollidingFish.gameObject.GetComponent<Fish>().spawnPoint.isFishSpawned = false;
//            Destroy(currentlyCollidingFish.gameObject);
//        }
//    }
//
//    private void OnTriggerEnter2D(Collider2D collider) {
//        if(collider.gameObject.tag == "Fish") {
//            currentlyCollidingFish = collider;
//            isCollidingWithFish = true;
//        } else {
//            Debug.Log("collide not fish");
//        }
//    }
//
//    private void OnTriggerExit2D(Collider2D collider) {
//        if(collider.gameObject.tag == "Fish") {
//            currentlyCollidingFish = null;
//            isCollidingWithFish = false;
//        }
//    }
//}
//