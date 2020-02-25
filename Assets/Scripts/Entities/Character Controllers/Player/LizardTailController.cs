using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardTailController : MonoBehaviour
{
    private List<GameObject> segments;
    public float angle;
    public Transform copyRot;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<GameObject>();
        GameObject segment = gameObject;
        while (segment != null)
        {
            segments.Add(segment);
            segment = segment.transform.GetChild(0).gameObject;
        }
    }

    private void SetRots(float angle)
    {
        foreach (GameObject segment in segments)
        {
            segment.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetRots(angle / segments.Count);
        segments[0].transform.localRotation = Quaternion.Euler(0, 0, copyRot.rotation.eulerAngles.z + offset);
    }
}
