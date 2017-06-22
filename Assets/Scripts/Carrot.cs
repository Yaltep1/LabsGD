using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot: Collectable
{
    float dir;
    public float speed;
    protected override void OnRabitHit(HeroRabit rabit)
    {
        this.CollectedHide();
        if (rabit.giant) { rabit.becomeNormal(); }
        else
        {

            LevelController.current.onRabitDeath(rabit);
        }
    }
    void Update()
    {
        
       
            Vector3 tp = this.transform.position;
            tp.x += speed / 100*dir;
            this.transform.position = tp;
        }
        void Start()
    {
        StartCoroutine(destroyLater());
    }
    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
   public void launch(float direction) {
        dir = direction;
        if (direction == 1) { this.GetComponent<SpriteRenderer>().flipX = false; }
        else { this.GetComponent<SpriteRenderer>().flipX = true; }

        Start();
    }
}
