using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool doubleJump = false;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;

    private bool grounded = false;
    private Rigidbody2D rb2d;

    Light light;

    public static int i;
    Color[] colors;
    private bool isChangingColor;
    private bool isIntensityDecreasing;
    private int destinationColor;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        light = GetComponent<Light>();

        i = 0;
        colors = new Color[] { Color.yellow, Color.red, Color.blue };
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (grounded || i!=2)
            doubleJump = false;

        if(Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                jump = true;
                if(i == 2)
                    doubleJump = true;
            }
            else if (!grounded && doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            i = 0;
            destinationColor = 0;
            isIntensityDecreasing = true;
            isChangingColor = true;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            i = 1;
            destinationColor = 1;
            isIntensityDecreasing = true;
            isChangingColor = true;
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            i = 2;
            destinationColor = 2;
            isIntensityDecreasing = true;
            isChangingColor = true;
        }

        if (isChangingColor)
        {
            if (light.intensity <= 0)
            {
                isIntensityDecreasing = false;
                light.color = colors[destinationColor];
            }

            if (isIntensityDecreasing)
            {
                light.intensity -= 0.5f;
            }

            else
            {
                if (light.intensity >= 10f)
                {
                    isChangingColor = false;
                }
                light.intensity += 0.5f;
            }
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if(jump)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
