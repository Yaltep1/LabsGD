using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    float timeForDeath = 0.7f;
    public static LevelController current;
    void Awake()
    {
        current = this;
    }
    Vector3 startingPosition;
    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabitDeath(HeroRabit rabit)
    {
        rabit.GetComponent<Animator>().SetTrigger("die");
       timeForDeath -= Time.deltaTime;

        if (timeForDeath <= 0)
       {
           rabit.transform.position = startingPosition;
        }
        
    }
}