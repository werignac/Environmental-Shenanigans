using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageZone : HazardController
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnShieldCollision(GameObject encounter)//Enemy takes damage when hit with shield.
    {
        enemy.Damage();
    }
}
