using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingMapBehavior : MonoBehaviour
{

    public ParticleSystem m_pingEffect;

    public void SetPingEffectColor(int pingColorIndex)
    {

        ParticleSystem.MainModule pingParticleSettings = m_pingEffect.main;

        if (pingColorIndex == 0)
            pingParticleSettings.startColor = new Color(1f, 0f, 0f); // Danger Color

        if (pingColorIndex == 1)
            pingParticleSettings.startColor = new Color(0f, 1f, 0f); // OMW Color

        if (pingColorIndex == 2)
            pingParticleSettings.startColor = new Color(0f, 0f, 1f); // Help Color

        if (pingColorIndex == 3)
            pingParticleSettings.startColor = new Color(1f, 1f, 0f); // Question Color

    }

}
