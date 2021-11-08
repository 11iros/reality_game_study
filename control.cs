using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    public Animator anmi;
    public Collider2D coll;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()  //开始是调用该类
    {
        
    }

    // Update is called once per frame
    void Update()  //逐帧上传 
    {
        movement();
        SwitchAnim();

    }
    void movement()
    {
        float horizontalMove= Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
     //角色移动
        if(horizontalMove !=0)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            anmi.SetFloat("running", Mathf.Abs(facedirection));
        }
        if(facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //角色跳跃
         if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce  );
            anmi.SetBool("jumping", true);
        }
    }

    void SwitchAnim() 
    {
        anmi.SetBool("ideling", false);
        if (anmi.GetBool("jumping"))
        {
            if (rb.velocity.y <0)
            {
                anmi.SetBool("jumping", false);
                anmi.SetBool("falling", true);
            }
        }else if (coll.IsTouchingLayers(ground))
        { 
            anmi.SetBool("falling", false);
            anmi.SetBool("ideling", true);

        }
    }
}
