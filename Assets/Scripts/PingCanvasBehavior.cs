using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingCanvasBehavior : MonoBehaviour
{

    [SerializeField] private Image m_image;

    private RectTransform m_rt;

    void Awake()
    {

        m_rt = GetComponent<RectTransform>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPingIcon(Sprite icon, Vector3 screenPosition)
    {

        m_image.sprite = icon;
        Vector3 screenPos = screenPosition;
        screenPos.y += 86;
        m_rt.position = screenPos;

    }
}
