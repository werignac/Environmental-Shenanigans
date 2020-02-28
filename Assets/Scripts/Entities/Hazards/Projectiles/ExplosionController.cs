using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float duration;
    public float speed;

    void Update()
    {
        float scale = (speed / 10) / Data.frameRate;
        transform.localScale = new Vector3(transform.localScale.x + scale, transform.localScale.y + scale, transform.localScale.z);
    }
}
