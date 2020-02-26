using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomData : MonoBehaviour
{
    public string roomName;
    public float width;
    public int roomType;
    public int nextRoomType;
    public GameObject healthSection;
    public GameObject hideSection;

    private void Update()
    {
#if UNITY_EDITOR
        Color color = new Color(0, 255, 0);
        Debug.DrawLine(new Vector3(width * -0.5f, 0), new Vector3(width * 0.5f, 0), color);
        Debug.DrawLine(new Vector3(width * 0.5f, 0), new Vector3(width * 0.5f, 25), color);
        Debug.DrawLine(new Vector3(width * 0.5f, 25), new Vector3(width * -0.5f, 25), color);
        Debug.DrawLine(new Vector3(width * -0.5f, 25), new Vector3(width * -0.5f, 0), color);
        gameObject.name = roomName;
#endif
    }

    public string GetRoomData()
    {
        return (roomName + "," + width + "," + roomType + "," + nextRoomType);
    }

    public void HealthRoom()
    {
        if (healthSection != null)
        {
            healthSection.SetActive(true);
        }
        if (hideSection != null)
        {
            hideSection.SetActive(false);
        }
    }

    public void NormalRoom()
    {
        if (healthSection != null)
        {
            healthSection.SetActive(false);
        }
        if (hideSection != null)
        {
            hideSection.SetActive(true);
        }
    }
}
