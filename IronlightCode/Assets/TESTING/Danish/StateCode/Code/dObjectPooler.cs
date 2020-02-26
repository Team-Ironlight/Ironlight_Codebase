using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Tools
{


    public class dObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
            public float scale;
            public Transform parentObj;
        }


        public static dObjectPooler Instance;


        public List<Pool> m_PoolList;

        public Dictionary<string, Queue<GameObject>> m_PoolDictionary;



        private void Awake()
        {
            Instance = this;
        }


        void Start()
        {
            m_PoolDictionary = new Dictionary<string, Queue<GameObject>>();
            //m_Pools = new List<Pool>();

            foreach (Pool pool in m_PoolList)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, pool.parentObj);

                    obj.transform.localScale *= pool.scale;

                    obj.SetActive(false);

                    objectPool.Enqueue(obj);
                }

                m_PoolDictionary.Add(pool.tag, objectPool);
            }
        }



        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            GameObject objToSpawn = null;

            if (!m_PoolDictionary.ContainsKey(tag))
            {
                Debug.Log("Object not found");
                return null;

            }

            for (int i = 0; i < m_PoolDictionary[tag].Count; i++)
            {
                objToSpawn = m_PoolDictionary[tag].Dequeue();

                if (objToSpawn.activeSelf)
                {
                    m_PoolDictionary[tag].Enqueue(objToSpawn);
                    objToSpawn = null;
                    continue;
                }
                else if(!objToSpawn.activeSelf)
                {
                    objToSpawn.SetActive(true);
                    //objToSpawn.transform.parent = null;
                    objToSpawn.transform.position = position;
                    objToSpawn.transform.rotation = rotation;
                    m_PoolDictionary[tag].Enqueue(objToSpawn);
                    break;
                }

            }

            return objToSpawn;

            //objToSpawn.SetActive(true);
            ////objToSpawn.transform.parent = null;
            //objToSpawn.transform.position = position;
            //objToSpawn.transform.rotation = rotation;

            

            //m_PoolDictionary[tag].Enqueue(objToSpawn);


            //return objToSpawn;
        }

    }
}