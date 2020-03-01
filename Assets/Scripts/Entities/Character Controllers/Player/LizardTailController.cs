using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardTailController : MonoBehaviour
{
    private List<GameObject> segments;
    public Transform copyRot;
    public float offset;
    public float radius;
    public float innerMultiply;

    private bool flipped;

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<GameObject>();
        GameObject segment = gameObject;
        while (segment.transform.childCount > 0)
        {
            segments.Add(segment);
            segment = segment.transform.GetChild(0).gameObject;
        }
    }

    /// <summary>
    /// Sets whether the tail is flipped or not.
    /// </summary>
    /// <param name="b">Whether the tail is flipped or not.</param>
    public void SetFlip(bool b)
    {
        flipped = b;
    }

    //Set curve based on the angle the shield is pointing in.
    private void GetCurve(float radius, float theta)
    {
        bool isNegative = theta > Mathf.PI;
        if (isNegative)
        {
            theta = theta - 2 * (theta - Mathf.PI);
        }

        if (theta < 0)
        {
            theta *= -1;
            isNegative = true;
        }

        float innerCoef = Mathf.Pow(0.460421f, theta + 0.827743f) + 0.368267f;
        innerCoef *= innerMultiply;
        float outerCoef = radius / (1 + Mathf.Cos(innerCoef * theta));
        float maxAngle = Mathf.Acos(-1) / innerCoef;

        float lastAngle = maxAngle;

        float angleDivisions = maxAngle / segments.Count;

        foreach (GameObject segment in segments)
        {

            SpriteRenderer sprite = segment.GetComponent<SpriteRenderer>();

            float instantRadius = outerCoef * (1 - Mathf.Cos(innerCoef * lastAngle));
            Vector2 funcPos = new Vector2(instantRadius * Mathf.Cos(lastAngle), instantRadius * Mathf.Sin(lastAngle));

            lastAngle -= angleDivisions;

            instantRadius = outerCoef * (1 - Mathf.Cos(innerCoef * lastAngle));
            Vector2 nextFuncPos = new Vector2(instantRadius * Mathf.Cos(lastAngle), instantRadius * Mathf.Sin(lastAngle));

            Vector2 difference = funcPos - nextFuncPos;

            float slopeAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (flipped)
            {
                slopeAngle = 180 - slopeAngle;
            }

            if (isNegative)
            {
                slopeAngle *= -1;
            }
            segment.transform.rotation = Quaternion.Euler(0, 0, slopeAngle + offset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float theta = copyRot.eulerAngles.z * Mathf.Deg2Rad;

        if (flipped)
        {
            theta = Mathf.PI - theta;
        }

        GetCurve(radius , theta);
    }
}
