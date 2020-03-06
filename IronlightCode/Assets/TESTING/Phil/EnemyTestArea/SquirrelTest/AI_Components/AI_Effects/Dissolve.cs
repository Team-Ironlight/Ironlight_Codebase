// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine.AI;
using IronLight;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float minStartTime = 2f;
    public float maxStartTime = 6f;
    public float dissolveTime = 3f;
    public AnimationCurve curve;

    float m_Timer;
    float m_EmissionRate;
    // MeshRenderer[] m_Renderer;
    SkinnedMeshRenderer[] m_Renderer;
    MaterialPropertyBlock m_PropertyBlock;
    public ParticleSystem m_ParticleSystem;
    ParticleSystem.EmissionModule m_Emission;
    float m_StartTime;
    float m_EndTime;

    const string k_CutoffName = "_Cutoff";

    private Dissolve mDissolveComponent;
    private Rigidbody m_rigidbody;
    private NavMeshAgent m_navAgent;
    private Animator m_aniMator;
    private Phil_StateMa m_stateMa;
    private GameObject m_DissolveObj;
    public GameObject m_Fill;

    private AI_AbilityManager mAbilityManager;

    public GameObject squirrelObject;

    void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        mAbilityManager = GetComponent<AI_AbilityManager>();
        m_DissolveObj = GameObject.Find("DissolveParticles");
        m_stateMa = GetComponent<Phil_StateMa>();
        mDissolveComponent = GetComponent<Dissolve>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_aniMator = GetComponent<Animator>();

        m_PropertyBlock = new MaterialPropertyBlock();
      
            GameObject de = GameObject.Find("BodyMesh");
            m_Renderer = de.GetComponentsInChildren<SkinnedMeshRenderer>();
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
        m_aniMator.SetTrigger("isDead");
        m_rigidbody.isKinematic = false;
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
                    m_navAgent.velocity = Vector3.zero;
                    m_navAgent.isStopped = true;

                    mAbilityManager.enabled = true;
                    mAbilityManager.StopCoroAll();

                    m_DissolveObj.SetActive(false);
                    m_stateMa.isActive = true;
                    m_rigidbody.isKinematic = true;

                //if (this.gameObject.layer == 11)  //Squirrel_C 11  Squirrel_W 15
                //{
                //    AI_Manager.instance.EnemyDied(false);
                //}
                //else if (this.gameObject.layer == 15)
                //{
                //    AI_Manager.instance.EnemyDied(true);
                //}


                mDissolveComponent.enabled = false;

                if (m_Fill)
                    m_Fill.SetActive(true);

               
                 //Invoke("Destroy_AIobject", 0.1f);                  //for workaround spawning , if we can reuse gameObject from the pooler.
                   squirrelObject.SetActive(false);


            }

                m_Timer += Time.deltaTime;

          
        }

    }

    void Destroy_AIobject()
    {
        Destroy(squirrelObject);
    }
}

