using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        this.CollectedHide();
        if (rabit.giant) { rabit.becomeNormal(); }
        else {
           
            LevelController.current.onRabitDeath(rabit); }
    }
}
