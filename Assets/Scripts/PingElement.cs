using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Ping Radial Menu/Ping Element Script")]
public class PingElement : MonoBehaviour
{

    public Image m_imageToHighlight;

    public Image m_pingIcon;

    [HideInInspector]
    public RectTransform m_rt;

    [HideInInspector]
    public PingRadialMenu m_parentRM;

    [HideInInspector]
    public float m_angleMin, m_angleMax;

    [HideInInspector]
    public float m_angleOffset;

    [HideInInspector]
    public bool m_active = false;

    [HideInInspector]
    public int m_assignedIndex = 0;


    void Awake()
    {

        m_rt = gameObject.GetComponent<RectTransform>();

    }

    // Start is called before the first frame update
    void Start()
    {

        m_rt.rotation = Quaternion.Euler(0, 0, -m_angleOffset); //Apply rotation determined by the parent radial menu.

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used by the parent radial menu to set up all the approprate angles. Affects master Z rotation and the active angles for lazy selection.
    public void setAllAngles(float offset, float baseOffset)
    {

        m_angleOffset = offset;
        m_angleMin = offset - (baseOffset / 2f);
        m_angleMax = offset + (baseOffset / 2f);

    }

    public void highlightThisElement()
    {

        setImageAlpha(1f);
        m_active = true;

    }

    public void unHighlightThisElement()
    {

        setImageAlpha(.3f);
        m_active = false;

    }

    private void setImageAlpha(float alpha)
    {

        Color colorAux = m_imageToHighlight.color;
        colorAux.a = alpha;
        m_imageToHighlight.color = colorAux;

    }
}
