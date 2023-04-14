using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public bool player;
    public Vector3Int origin;

    public virtual int Attack()
    {
        return 0;
    }
}
