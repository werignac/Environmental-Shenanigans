using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(TextMesh))]
public class Timer : MonoBehaviour
{
    TextMesh display;

    private float lessSec;
    private int sec;
    private int min;
    private bool hasSwitched;

    private Transform camPos;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TextMesh>();
        DontDestroyOnLoad(gameObject);
        GetComponent<MeshRenderer>().sortingLayerName = "UI Sprites";
        camPos = Camera.main.transform;
        offset = transform.position - camPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (camPos != null)
        {
            camPos = Camera.main.transform;
        }

        transform.position = camPos.position + offset;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            lessSec += Time.deltaTime;
            if (hasSwitched)
            {
                Destroy(gameObject);
            }
        }
        else if (! hasSwitched)
        {
            hasSwitched = true;
        }

        if (lessSec > 1)
        {
            sec += (int) lessSec;
            lessSec = lessSec % 1;
        }

        if (sec > 60)
        {
            min += sec / 60;
            sec = sec % 60;
        }

        display.text = min + " : " + sec;
    }
}
