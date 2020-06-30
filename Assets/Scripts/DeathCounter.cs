using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    TextMesh display;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TextMesh>();
        GetComponent<MeshRenderer>().sortingLayerName = "UI Sprites";
    }

    // Update is called once per frame
    void Update()
    {
        display.text = "" + Data.currentDeaths;
    }
}
