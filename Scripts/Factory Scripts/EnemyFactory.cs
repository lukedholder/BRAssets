using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{

    public IEnemy GetEnemy(int ID) {

        switch (ID) {
            case 0:
                return new ThinClown();
            //case 1:
                //return new BazookaClown();
            //case 2:
                //return new Boss();
            default:
                return null;
        }
    }
}
