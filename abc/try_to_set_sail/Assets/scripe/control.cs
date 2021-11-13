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
    public int power_up;
    public UnityEngine.UI.Text power_number;
    // Start is called before the first frame update
    void Start()  //��ʼʱ���ø���
    {
        
    } 

    // Update is called once per frame
    void Update()  //��֡�ϴ� 
    {
        movement();
        SwitchAnim();

    }
    void movement()
    {
        float horizontalMove= Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
     //��ɫ�ƶ�
        if(horizontalMove !=0)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            anmi.SetFloat("running", Mathf.Abs(facedirection));
        }
        if(facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //��ɫ��Ծ
         if (Input.GetButtonDown("Jump"))
        { if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anmi.SetBool("jumping", true);
            }
        }
    }
    //�л�����Ч��
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
    //�ռ���Ʒ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection")
        {
            Destroy(collision.gameObject);
            power_up++;
            power_number.text = power_up.ToString();
        }
    }
}
