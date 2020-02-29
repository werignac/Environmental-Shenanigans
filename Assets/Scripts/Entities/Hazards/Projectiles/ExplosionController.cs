using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : HazardController
{
    public float duration;
    public float speed;
    private float count;

    void Update()
    {
        float scale = (speed / 10) / Data.frameRate;
        transform.localScale = new Vector3(transform.localScale.x + scale, transform.localScale.y + scale, transform.localScale.z);
        count += Time.deltaTime;
        if(count >= duration)
        {
            Destroy(gameObject);
        }
    }

    public override void OnShieldCollision(GameObject encounter)
    {

    }
}
