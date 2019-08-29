using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Camera m_playerCamera;

    [SerializeField] private UserInterfaceManager m_UIManager;

    [SerializeField] private KeyCode m_enterPingModeKey;

    [SerializeField] private KeyCode m_pingSpawnKey;

    [SerializeField] private KeyCode m_exitPingModeKey;

    [SerializeField] private float m_timeToPingRadialMenu = 0.1f;

    private Vector3 m_rayHitPosition;

    private Vector3 m_mousePosition;

    private bool m_pingMode = false;

    private bool m_pingMenuOpened = false;

    private float m_pingKeyTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_enterPingModeKey))
        {
            enterPingMode();

        }

        if (Input.GetKeyUp(m_enterPingModeKey))
        {

            

        }

        if (Input.GetKeyDown(m_exitPingModeKey))
        {

            exitPingMode();

        }

        if (Input.GetKeyDown(m_pingSpawnKey))
        {

            if (m_pingMode)
            {

                RaycastHit hit;
                Ray ray = m_playerCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {

                    Transform objectHit = hit.transform;

                    m_rayHitPosition = hit.point;
                    m_mousePosition = Input.mousePosition;

                }

            }

        }

        if (Input.GetKey(m_pingSpawnKey))
        {

            if (!m_pingMenuOpened)
            {

                if (m_pingKeyTime >= m_timeToPingRadialMenu)
                    enterPingMenu();
                else
                    m_pingKeyTime += Time.deltaTime;

            }

        }

        if (Input.GetKeyUp(m_pingSpawnKey))
        {

            if (m_pingMode)
            {

                if (m_pingMenuOpened)
                {

                    if (m_UIManager.hidePingMenuPingSelected())
                        m_UIManager.spawnPingMap(m_rayHitPosition, m_mousePosition);

                    m_pingMenuOpened = false;

                }
                else
                    m_UIManager.spawnNormalPingMap(m_rayHitPosition, m_mousePosition);

                if (!Input.GetKey(m_enterPingModeKey))
                    exitPingMode();

                m_pingKeyTime = 0f;

            }

        }

    }

    private void enterPingMode()
    {

        m_pingMode = true;
        m_UIManager.setPingCursor(true);


    }

    private void exitPingMode()
    {

        m_pingMode = false;
        m_UIManager.setPingCursor(false);

    }

    private void enterPingMenu()
    {

        m_UIManager.showPingMenu(m_mousePosition);
        m_pingMenuOpened = true;

    }
}
