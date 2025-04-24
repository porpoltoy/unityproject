using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;

    // public float verticalInput;
    private Vector2 moveInput;
    private Vector2 moveInputSide;

    [Header("Aim data")]
    public LayerMask whatIsAimMask;
    public Transform aimTransform;

    [Header("Tower data")]
    public Transform Gun;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //update is called once per frame // 120 fps is 120 per second
    void Update()
    {
        Movement();

        Debug.Log(Mouse.current.position.ReadValue());
        //Debug.Log(Input.mousePosition); //(older)
    }

    private void Movement()
    {
        UpdateAim();
        //verticalInput = Input.GetAxis("Vertical"); //(older)
        moveInput = Keyboard.current.wKey.isPressed ? Vector2.up :
                    Keyboard.current.sKey.isPressed ? Vector2.down :
                    Vector2.zero;
        moveInputSide = Keyboard.current.dKey.isPressed ? Vector2.right :
                    Keyboard.current.aKey.isPressed ? Vector2.left :
                    Vector2.zero;
        // moveInput = new Vector2(0, Input.GetAxis("Vertical"));    //(Older)
        //moveInputSide = new Vector2(Input.GetAxis("Horizontal"), 0);  //(Older)
    }

    // you can see timestep in project settings > time
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveSpeed * moveInputSide.x, 0, moveSpeed * moveInput.y);

        Gun.LookAt(aimTransform);

        //Vector3 direction = ainTrabsform
    }

    private void UpdateAim()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity,whatIsAimMask))
        {
            //Debug.Log(hit.point);
            float fixedY = aimTransform.position.y;
            aimTransform.position = new Vector3(hit.point.x, fixedY, hit.point.z);
            //aimTransform.position = hit.point;

         
        }
    }
}
