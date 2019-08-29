using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Ping Radial Menu/Ping Menu Script")]
public class PingRadialMenu : MonoBehaviour
{

    [HideInInspector]
    public RectTransform m_rt;

    [HideInInspector]
    public bool m_cancelActive = true;

    [HideInInspector]
    public int m_index = 0; //The current index of the element we're pointing at.

    [SerializeField] private RectTransform m_arrowEnd;

    [SerializeField] private RectTransform m_arrowLine;

    [Tooltip("This is the list of radial menu elements. This is order-dependent. The first element in the list will be the first element created, and so on.")]
    [SerializeField] private List<PingElement> m_pingElements = new List<PingElement>();

    [SerializeField] private PingElement m_pingCancel;

    [Tooltip("Controls the total angle offset for all elements. For example, if set to 45, all elements will be shifted +45 degrees. Good values are generally 45, 90, or 180")]
    [SerializeField] private float m_globalOffset = 0f;

    private float m_currentAngle = 0f; //Our current angle from the center of the radial menu.

    private int m_elementCount;

    private float m_angleOffset; //The base offset. For example, if there are 4 elements, then our offset is 360/4 = 90

    private int m_previousActiveIndex = 0; //Used to determine which buttons to unhighlight in lazy selection.


    void Awake()
    {

        m_rt = GetComponent<RectTransform>();
        m_elementCount = m_pingElements.Count;
        m_angleOffset = (360f / (float)m_elementCount);

        //Loop through and set up the elements.
        for (int i = 0; i < m_elementCount; i++)
        {

            m_pingElements[i].m_parentRM = this;

            m_pingElements[i].setAllAngles((m_angleOffset * i) + m_globalOffset, m_angleOffset);

            m_pingElements[i].m_assignedIndex = i;

        }

    }
    
    // Start is called before the first frame update
    void Start()
    {

        if (m_arrowEnd != null)
            m_arrowEnd.rotation = Quaternion.Euler(0, 0, -m_globalOffset); //Point the selection follower at the first element.

    }

    // Update is called once per frame
    void Update()
    {

        float rawAngle;
        rawAngle = Mathf.Atan2(Input.mousePosition.y - m_rt.position.y, Input.mousePosition.x - m_rt.position.x) * Mathf.Rad2Deg;
        m_currentAngle = normalizeAngle(-rawAngle + 90 - m_globalOffset + (m_angleOffset / 2f));
        Vector3 centerToMouseVector = Input.mousePosition - m_pingCancel.m_rt.position;

        if (centerToMouseVector.magnitude < (m_pingCancel.m_rt.sizeDelta.x / 2) )
        {

            selectCancel();

        }
        else
        {

            //Handles lazy selection. Checks the current angle, matches it to the index of an element, and then highlights that element.
            if (m_angleOffset != 0)
            {

                //Current element index we're pointing at.
                m_index = (int)(m_currentAngle / m_angleOffset);

                if (m_pingElements[m_index] != null)
                {

                    //Select it.
                    selectButton(m_index);

                }

            }

        }

        //Updates the selection follower if we're using one.
        if (m_arrowEnd != null)
        {

            m_arrowEnd.rotation = Quaternion.Euler(0, 0, rawAngle + 270);
            m_arrowEnd.position = Input.mousePosition;

        }

        if (m_arrowLine != null)
        {

            Vector3 differenceVector = Input.mousePosition - m_arrowLine.position;
            m_arrowLine.sizeDelta = new Vector2(differenceVector.magnitude, 1f);

            m_arrowLine.pivot = new Vector2(0, 0.5f);
            float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
            m_arrowLine.rotation = Quaternion.Euler(0, 0, angle);

        }

    }

    public Sprite getSelectedIcon()
    {
        return m_pingElements[m_index].m_pingIcon.sprite;
    }

    public int getElementsCount()
    {

        return m_elementCount;

    }

    public void resetValues()
    {

        m_arrowEnd.rotation = Quaternion.identity;
        m_arrowEnd.position = Vector3.zero;
        m_arrowLine.rotation = Quaternion.identity;
        m_arrowLine.sizeDelta = Vector2.zero;
        m_pingElements[m_previousActiveIndex].unHighlightThisElement();

    }


    //Selects the button with the specified index.
    private void selectButton(int i)
    {

        if (m_pingElements[i].m_active == false)
        {

            m_pingElements[i].highlightThisElement(); //Select this one

            if (m_previousActiveIndex != i)
                m_pingElements[m_previousActiveIndex].unHighlightThisElement(); //Deselect the last one.

            if (m_cancelActive)
                m_pingCancel.unHighlightThisElement();

            m_cancelActive = false;

        }

        m_previousActiveIndex = i;

    }

    private void selectCancel()
    {

        m_pingCancel.highlightThisElement();
        m_pingElements[m_previousActiveIndex].unHighlightThisElement();
        m_cancelActive = true;

    }

    //Keeps angles between 0 and 360.
    private float normalizeAngle(float angle)
    {

        angle = angle % 360f;

        if (angle < 0)
            angle += 360;

        return angle;

    }

}
