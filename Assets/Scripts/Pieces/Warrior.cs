using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnt : Ant
{
    public int damageDealt;

    public override int Attack()
    {
        return damageDealt;
    }
}
