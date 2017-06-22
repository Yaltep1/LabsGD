using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float time_to_wait;
    float timeW;
    public  float speed;
    float speedX;
    float speedY;
    Vector3 speedXY;
    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    bool tarA = false;
    // Use this for initialization
    void Start()
    {timeW = time_to_wait; 
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        
    }
    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 my_pos = this.transform.position;
        Vector3 target;
        if (tarA)
        {
            target = this.pointA;
            if (isArrived(my_pos, target))
            {
                time_to_wait -= Time.deltaTime;
                if (time_to_wait <= 0)
                {target = this.pointB;
                tarA = false;
                    time_to_wait = timeW;
                }
                
            }
        }
        else
        {
            target = this.pointB;
            if (isArrived(my_pos, target))
            { time_to_wait -= Time.deltaTime;
                if (time_to_wait <= 0)
                {
                    target = this.pointA;
                    tarA = true;
                    time_to_wait = timeW;
                }
            }
        }
            Vector3 destination = target - my_pos;
            Transform platform_transform = this.transform;
            destination.z = 0;
        if (MoveBy.x != 0 && MoveBy.y != 0)
        {
            if (MoveBy.x >= MoveBy.y)
            {
                speedX = speed/100;
                speedY = destination.y / destination.x * speed/100;
            }
            else {
                speedY= speed/100;
                speedX = destination.x / destination.y * speed/100;
            }
            if (destination.x < 0&& destination.y<0)
            {
                speedXY = new Vector3(-speedX, -speedY, 0);
                platform_transform.position = my_pos + speedXY;
            }
            else if (destination.x < 0 && destination.y > 0)
            {
                speedXY = new Vector3(-speedX, speedY, 0);
                platform_transform.position = my_pos + speedXY;
            }
            else if (destination.x > 0 && destination.y < 0)
            {
                speedXY = new Vector3(speedX, -speedY, 0);
                platform_transform.position = my_pos + speedXY;
            }

            else if (destination.x > 0 && destination.y > 0)
            {
                speedXY = new Vector3(speedX, speedY, 0);
                platform_transform.position = my_pos + speedXY;
            }
        }
        else if(MoveBy.y == 0) {
            if (destination.x < 0 )
            {
                speedXY = new Vector3(-speed/100, 0, 0);
                platform_transform.position = my_pos + speedXY;
            }
            else if (destination.x > 0)
            {
                speedXY = new Vector3(speed/100, 0, 0);
                platform_transform.position = my_pos + speedXY;
            }
        }
        else if (MoveBy.x == 0)
        {
            if (destination.y < 0)
            {
                speedXY = new Vector3(0, -speed/100, 0);
                platform_transform.position = my_pos + speedXY;
            }
            else if (destination.y > 0)
            {
                speedXY = new Vector3(0, speed/100, 0);
                platform_transform.position = my_pos + speedXY;
            }
        }

    }
}
