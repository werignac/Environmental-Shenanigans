using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDisplay : MonoBehaviour
{
    private int numDash;
    private GameObject[] dashDisplays;

    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        dashDisplays = new GameObject[transform.childCount];
        int i = 0;
        float xPos = Camera.main.orthographicSize * Screen.width / Screen.height + offset / Screen.width;
        foreach (Transform child in transform)
        {
            dashDisplays[i] = child.gameObject;
            xPos -= dashDisplays[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            dashDisplays[i].transform.position = new Vector3(xPos, dashDisplays[i].transform.position.y);
            xPos -= dashDisplays[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            child.gameObject.SetActive(false);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numDash != Data.playerDashes)
        {
            numDash = Data.playerDashes;
            for (int i = 0; i < Mathf.Min(numDash, dashDisplays.Length); ++i)
            {
                dashDisplays[i].SetActive(true);
            }
            for (int i = numDash; i < dashDisplays.Length; ++i)
            {
                dashDisplays[i].SetActive(false);
            }
        }
    }
}
