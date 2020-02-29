using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardTailController : MonoBehaviour
{
    private List<GameObject> segments;
    public Transform copyRot;
    public float offset;
    public float radius;
    public float flip = 1;

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<GameObject>();
        GameObject segment = transform.GetChild(0).gameObject;
        while (segment != null)
        {
            segments.Add(segment);
            segment = segment.transform.GetChild(0).gameObject;
        }
    }

    private void GetCurve(float radius, float theta)
    {
        float innerCoef = Mathf.Pow(0.460421f, theta + 0.827743f) + 0.368267f;
        float outerCoef = radius / (1 + Mathf.Cos(innerCoef * theta));
        float maxAngle = Mathf.Acos(-1) / innerCoef;
        float lastAngle = maxAngle;

        float angleDivisions = maxAngle / segments.Count;

        foreach (GameObject segment in segments)
        {

            SpriteRenderer sprite = segment.GetComponent<SpriteRenderer>();

            float length = sprite.size.y*segment.transform.lossyScale.y;

            lastAngle = Mathf.Acos(length / outerCoef - 1) / innerCoef; //- lastAngle;

            segment.transform.localRotation = Quaternion.Euler(0, 0, lastAngle*Mathf.Rad2Deg*flip + offset);

            lastAngle -= angleDivisions;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetCurve(radius , copyRot.eulerAngles.z*Mathf.Deg2Rad);
    }
}
