﻿// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/8/2020

using UnityEngine;

    public class Dissolve : MonoBehaviour
    {
        public float minStartTime = 2f;
        public float maxStartTime = 6f;
        public float dissolveTime = 3f;
        public AnimationCurve curve;

        float m_Timer;
        float m_EmissionRate;
         MeshRenderer[] m_Renderer;
        MaterialPropertyBlock m_PropertyBlock;
       public ParticleSystem m_ParticleSystem;
        ParticleSystem.EmissionModule m_Emission;
        float m_StartTime;
        float m_EndTime;

        const string k_CutoffName = "_Cutoff";

    public GameObject squirrelObject;

    void Start()
        {
            

            m_PropertyBlock = new MaterialPropertyBlock();
            m_Renderer = GetComponentsInChildren<MeshRenderer>();

            
            m_ParticleSystem = GetComponentInChildren<ParticleSystem>();

            m_Emission = m_ParticleSystem.emission;

            m_EmissionRate = m_Emission.rateOverTime.constant;
            m_Emission.rateOverTimeMultiplier = 0;


            m_Timer = 0;

            m_StartTime = Time.time + Random.Range(minStartTime, maxStartTime);
            m_EndTime = dissolveTime + m_ParticleSystem.main.startLifetime.constant;
    }

        void Update()
        {
            if (Time.time >= m_StartTime)
            {
                float cutoff = 0; bool flag = false;

                for (int i = 0; i < m_Renderer.Length; i++)
                {
                    m_Renderer[i].GetPropertyBlock(m_PropertyBlock);
                    cutoff = Mathf.Clamp01(m_Timer / dissolveTime);
                    m_PropertyBlock.SetFloat(k_CutoffName, cutoff);
                    m_Renderer[i].SetPropertyBlock(m_PropertyBlock);

                     if(i >= m_Renderer.Length - 1){flag = true;}
                }

                m_Emission.rateOverTimeMultiplier = curve.Evaluate(cutoff) * m_EmissionRate;
        

                if (flag)
                {
                    squirrelObject.SetActive(false);

                }

                m_Timer += Time.deltaTime;

          
            }

        }
    }

