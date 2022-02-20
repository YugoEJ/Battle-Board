using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    public float speed;
    public float horizontalMove = 0f;
    public float runSpeed = 1f;
    public float MovementSpeed = 60;
    public float JumpForce = 6f;
    public bool appliedEffects = false;

    public Animator animator;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector2(0, -90); //flip the character on its x axis - to the right
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * MovementSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector2(0, 90); //flip the character on its x axis - to the left
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * MovementSpeed;
        }
    }
}
