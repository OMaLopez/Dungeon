using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
