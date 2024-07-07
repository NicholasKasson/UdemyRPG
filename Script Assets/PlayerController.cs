using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigid;
    public float movementSpeed;
    public static PlayerController instance;
    public string areaTransitionName;
    public Animator theAnim;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        { if(instance != this)
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Rigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, Input.GetAxisRaw("Vertical") * movementSpeed);

            theAnim.SetFloat("moveX", Rigid.velocity.x);
            theAnim.SetFloat("moveY", Rigid.velocity.y);
        }
        else
        {   
            Rigid.velocity = Vector2.zero;
        }
        

            if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                theAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                theAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            
            }
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(0.5f, 0.5f, 0f);
        topRightLimit = topRight + new Vector3(-0.5f, -0.5f, 0f);
    }
}
