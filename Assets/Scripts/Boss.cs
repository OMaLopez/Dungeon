using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] fireBallSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public int adds = 0;
    public GameObject[] miniBoss;
    public GameObject reward;
    public static Boss instance;
    private void Awake()
    {
        instance = this;
        foreach (GameObject t in miniBoss)
        {
            t.SetActive(false);
        }
    }
    private void Update()
    {
        for (int i = 0; i < adds; i++)
        {
            miniBoss[i].SetActive(true);
        }
        for (int i = 0; i < miniBoss.Length; i++)
        {

            miniBoss[i].transform.position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireBallSpeed[i]) * distance, Mathf.Sin(Time.time * fireBallSpeed[i]) * distance, 0);
        }
    }
    protected override void Death()
    {
        base.Death();
        reward.SetActive(true);
    }

}
