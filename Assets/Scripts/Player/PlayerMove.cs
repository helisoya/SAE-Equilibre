using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public GameObject charModel;
    public float moveSpeed = 3;
    public float leftRightSpeed = 4;
    public static bool canMove = false;
    

    // Update is called once per frame
    void Update()
    {

        // Continous movement

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if (canMove)
        {

            // Left - Right Movement

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
                }
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
                }
            }
        }
    }

    public void Jump()
    {
        StartCoroutine(JumpMovement());
        charModel.GetComponent<Animator>().Play("Jump");
    }

    IEnumerator JumpMovement()
    {
        transform.Translate(1 * Time.deltaTime * new Vector3(0, 1, 1), Space.World); // Vector3(0, 1, 1) means forward + up vector
        yield return new WaitForSeconds(1.0f);
    }

}
