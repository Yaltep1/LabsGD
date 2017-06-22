using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongOrcBehavior : MonoBehaviour
{
    public float time_to_wait;
    float timeW;
    float timeForDeath = 0.7f;
    public float speed;
    public float carrotSpeed;
    bool proc = true;
    float X;
    float Y;
    float speed1;
    float speed2;
    bool live=true;
    float speed3 = 0;
    Transform orc_transform;
    public float MoveBy;
    float last_carrot = 0;
    public GameObject Carrot;
    public enum Mode
     {
        GoToA,
        GoToB,
        Attack
        //...
    }
    Mode mode = Mode.GoToB;
    // Use this for initialization
    void Start()
    {
      
        if (MoveBy > 0)
        {
            timeW = time_to_wait;
            speed1 = speed;
            speed2 = -speed;

            this.X = this.transform.position.x;
            this.Y = this.X + MoveBy;
        }
        if (MoveBy < 0)
        {
            timeW = time_to_wait;
            speed1 = -speed;
            speed2 = speed;
            mode = Mode.GoToA;
            this.Y = this.transform.position.x;
            this.X = this.Y + MoveBy;
        }
        if (X == Y) { proc = false; }


    }

    // Update is called once per frame
    void Update()
    {
        if (!live)
        {
            timeForDeath -= Time.deltaTime;

            if (timeForDeath <= 0)
            {
                Destroy(this.gameObject);
                timeForDeath = 0.7f;
            }
            
        }
        if (Carrot) {
            Vector3 tp = Carrot.transform.position;
            tp.x += carrotSpeed/100;
            Carrot.transform.position = tp;
        }
        
       
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.flipX = true;
        orc_transform = this.transform;
        Vector3 my_pos = this.transform.position;
        Animator animator = GetComponent<Animator>();
        Vector3 vec = my_pos;

        if (rabit_pos.x >= Mathf.Min(X, Y)
&& rabit_pos.x <= Mathf.Max(X, Y))
        {
            mode = Mode.Attack;
        }

        
        if (mode == Mode.Attack && rabit_pos.x >= Mathf.Min(X, Y)
&& rabit_pos.x <= Mathf.Max(X, Y))
        {
            if (collider.IsTouching(HeroRabit.lastRabit.GetComponent<BoxCollider2D>()))
            {
                

                if (die(rabit_pos.y))
                {    live = false;
                    animator.SetTrigger("die");
                    
                   
                }
                
 }
            else if (my_pos.x < rabit_pos.x)
            {
                sr = GetComponent<SpriteRenderer>();
                sr.flipX = true;
                speed = speed1;
                animator.SetBool("run", true);
            }
            else 
            {
                sr = GetComponent<SpriteRenderer>();
                sr.flipX = false;
                speed = speed2;
                animator.SetBool("run", true);
            }
            if (Mathf.Abs(rabit_pos.x - my_pos.x) < 5.0f)
            {
                animator.SetBool("run", false);
                if (speed2 == speed) { 
                speed3 = speed;}
                else if (speed1 == speed)
                {
                    speed3 = speed;
                }
                speed = 0;
                
                if (Time.time - last_carrot > 2.0f)
                {GetComponent<Animator>().SetTrigger("hit");
                    last_carrot = Time.time;
                    //GetComponent<Animator>().ResetTrigger("hit");


                    if (sr.flipX == true)
                    {

                        this.launchCarrot(1);
                    }
                    else {
                        this.launchCarrot(-1);
                    }
                }


            }
            if (MoveBy > 0)
            {
                vec.x += speed / 100;
            }
            else { vec.x -= speed / 100; }
            orc_transform.position = vec;

        }
        else if (mode == Mode.Attack && (rabit_pos.x < Mathf.Min(X, Y)
|| rabit_pos.x > Mathf.Max(X, Y)))
        {

            if (speed3 == speed2)
            {
                speed = speed2;
                mode = Mode.GoToA;
            }
            else
            {
                speed = speed1;
                mode = Mode.GoToB;
            }
            if (MoveBy > 0)
            {
                vec.x += speed / 100;
            }
            else { vec.x -= speed / 100; }
            orc_transform.position = vec;

        }

        else if (proc)
        {
            animator.SetBool("run", true);



            if (mode == Mode.GoToA)
            {
                sr = GetComponent<SpriteRenderer>();
                sr.flipX = false;
                if (isArrived(my_pos.x, X))
                {
                    animator.SetBool("run", false);
                    sr.flipX = true;
                    speed = 0;
                    time_to_wait -= Time.deltaTime;
                    if (mode == Mode.Attack) time_to_wait = 0;
                    if (time_to_wait <= 0)
                    {
                        mode = Mode.GoToB;
                        speed = speed1;
                        time_to_wait = timeW;
                    }

                }
            }
            else if (mode == Mode.GoToB)
            {
                sr = GetComponent<SpriteRenderer>();
                sr.flipX = true;

                if (isArrived(my_pos.x, Y))
                {
                    animator.SetBool("run", false);
                    sr.flipX = false;
                    speed = 0;

                    time_to_wait -= Time.deltaTime;
                    if (mode == Mode.Attack) time_to_wait = 0;
                    if (time_to_wait <= 0)
                    {
                        mode = Mode.GoToA;
                        speed = speed2;
                        time_to_wait = timeW;
                    }



                }


            }
            if (MoveBy > 0)
            {
                vec.x += speed / 100;
            }
            else { vec.x -= speed / 100; }
            orc_transform.position = vec;
        }
       
    }

    bool isArrived(float pos, float target)
    {

        return Mathf.Abs(target - pos) < 0.02f;
    }

    bool die(float y)
    {
        if (y - this.transform.position.y > 1) return true;
        return false;
    }
   
    void launchCarrot(float direction)
    {
        
        GameObject obj = GameObject.Instantiate(this.Carrot);
     
        Vector3 tp = this.transform.position;
        
        tp.y+=0.6f;
         tp.x += direction*0.7f ;
        obj.transform.position = tp;
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.launch(direction);
        
    }
}