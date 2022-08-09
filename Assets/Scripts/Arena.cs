using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : Collidable
{
    public GameObject boss;


    private bool startBattle = true;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && startBattle)
        {
            StartBattle();
            startBattle = false;
        }
    }

    private void StartBattle()
    {
        Debug.Log("StartBattle");
        boss.SetActive(true);
    }
}
