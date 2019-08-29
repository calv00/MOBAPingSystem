using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{

    [SerializeField] private Canvas m_playerCanvas;

    [SerializeField] private PingRadialMenu m_pingMenu;

    [SerializeField] private GameObject m_pingMap;

    [SerializeField] private GameObject m_pingCanvas;

    [SerializeField] private Image m_mouseCursor;

    [SerializeField] private Sprite m_mousePingCursorSprite;

    [SerializeField] private Sprite m_normalPingSprite;

    [SerializeField] private List<AudioClip> m_pingElementsSounds;

    [SerializeField] private AudioClip m_pingNormalSound;

    private Sprite m_mouseCursorSprite;

    private AudioSource m_UIAudioSource;


    // Start is called before the first frame update
    void Start()
    {

        m_UIAudioSource = GetComponent<AudioSource>();
        Cursor.visible = false;
        m_mouseCursor.gameObject.SetActive(true);
        m_mouseCursorSprite = m_mouseCursor.sprite;

    }

    // Update is called once per frame
    void Update()
    {

        m_mouseCursor.rectTransform.position = Input.mousePosition;

    }

    public void setPingCursor(bool pingCursor)
    {

        if (pingCursor)
            m_mouseCursor.sprite = m_mousePingCursorSprite;
        else
            m_mouseCursor.sprite = m_mouseCursorSprite;

    }

    public void showPingMenu(Vector3 position)
    {

        m_pingMenu.gameObject.SetActive(true);
        m_pingMenu.m_rt.position = position;

        hideMouseCursor(true);

    }

    public bool hidePingMenuPingSelected()
    {

        hideMouseCursor(false);

        bool cancelledPing = m_pingMenu.m_cancelActive;
        m_pingMenu.resetValues();
        m_pingMenu.gameObject.SetActive(false);
        return !cancelledPing;

    }

    public void spawnPingMap(Vector3 mapPosition, Vector3 screenPosition)
    {

        GameObject pingMapGO = (Instantiate(m_pingMap) as GameObject);
        PingMapBehavior pingMap = pingMapGO.GetComponent<PingMapBehavior>();
        pingMap.gameObject.transform.position = mapPosition;
        pingMap.SetPingEffectColor(m_pingMenu.m_index);
        pingMap.m_pingEffect.Play();
        Destroy(pingMapGO, 2.45f);

        GameObject pingCanvasGO = Instantiate(m_pingCanvas) as GameObject;
        pingCanvasGO.transform.SetParent(m_playerCanvas.transform);
        PingCanvasBehavior pingCanvas = pingCanvasGO.GetComponent<PingCanvasBehavior>();
        pingCanvas.SetPingIcon(m_pingMenu.getSelectedIcon(), screenPosition);
        Destroy(pingCanvasGO, 2.45f);

        playPingSpawnSound(m_pingElementsSounds[m_pingMenu.m_index]);

    }

    public void spawnNormalPingMap(Vector3 mapPosition, Vector3 screenPosition)
    {

        GameObject pingMapGO = (Instantiate(m_pingMap) as GameObject);
        PingMapBehavior pingMap = pingMapGO.GetComponent<PingMapBehavior>();
        pingMap.gameObject.transform.position = mapPosition;
        pingMap.m_pingEffect.Play();
        Destroy(pingMapGO, 2.45f);

        GameObject pingCanvasGO = Instantiate(m_pingCanvas) as GameObject;
        pingCanvasGO.transform.SetParent(m_playerCanvas.transform);
        PingCanvasBehavior pingCanvas = pingCanvasGO.GetComponent<PingCanvasBehavior>();
        pingCanvas.SetPingIcon(m_normalPingSprite, screenPosition);
        Destroy(pingCanvasGO, 2.45f);

        playPingSpawnSound(m_pingNormalSound);

    }

    private void playPingSpawnSound(AudioClip spawnSound)
    {

        if (m_UIAudioSource.isPlaying)
            m_UIAudioSource.Pause();

        m_UIAudioSource.PlayOneShot(spawnSound);

    }

    private void hideMouseCursor(bool hide)
    {

        Color mouseCursorColor = m_mouseCursor.color;

        if (hide)
            mouseCursorColor.a = 0f;
        else
            mouseCursorColor.a = 1f;

        m_mouseCursor.color = mouseCursorColor;

    }

}
