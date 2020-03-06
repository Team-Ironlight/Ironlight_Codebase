// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace UPool.Demo
{
    public class Demo : MonoBehaviour
    {
        UnityEvent m_MyEvent = new UnityEvent();

        [SerializeField]
        private Button _getObjButton;

        private Pool<PoolableObject> _pool;

        const int poolSize = 5;

        void Awake()
        {
            //     _getObjButton.onClick.AddListener(GetObjFromPool);

            m_MyEvent.AddListener(GetObjFromPool);

            _pool = new Pool<PoolableObject>(3, Resources.Load<GameObject>("DemoObject"), null, false);


           // //Loop Number of Object.
           //if(m_MyEvent != null)
           // {
           //     //Begin the action
           //     m_MyEvent.Invoke();
           // }
           // if (m_MyEvent != null)
           // {
           //     //Begin the action
           //     m_MyEvent.Invoke();
           // }
           // if (m_MyEvent != null)
           // {
           //     //Begin the action
           //     m_MyEvent.Invoke();
           // }
        }
        private void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                if (m_MyEvent != null)
                {
                    //Begin the action
                    m_MyEvent.Invoke();
                }
            }
        }
        void Start()
        {

        }

        private void GetObjFromPool()
        {
            //spawn point
            _pool.Acquire().transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
    }
}