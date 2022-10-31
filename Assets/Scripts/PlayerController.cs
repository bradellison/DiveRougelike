using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator myAnim; 
    private Rigidbody2D myRigidBody;

    public float horizontalMoveSpeed;
    public float initialJumpSpeed;

    bool isGrounded;

    bool isFacingRight = true; 

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator> ();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    IEnumerator Jump() {
        myAnim.SetBool ("isJump", true);
        yield return new WaitForSeconds(0.2f);
        myAnim.SetBool ("isJump", false);
    }

    void Flip ()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void MovePlayer() {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        myRigidBody.velocity = new Vector3 (moveHorizontal * horizontalMoveSpeed, myRigidBody.velocity.y, 0f);
        myAnim.SetFloat ("Horizontal Speed", Mathf.Abs(myRigidBody.velocity.x));

        if(moveHorizontal > 0 && !isFacingRight) {
            Flip();
        } else if(moveHorizontal < 0 && isFacingRight) {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Jump());
            myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, 1 * initialJumpSpeed, 0f);
        }

        myAnim.SetFloat ("Vertical Speed", myRigidBody.velocity.y);

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
}
