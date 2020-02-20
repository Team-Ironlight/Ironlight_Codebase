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
            public Vector3 scale;
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
                    GameObject obj = Instantiate(pool.prefab, transform);

                    obj.transform.localScale = pool.scale;

                    obj.SetActive(false);

                    objectPool.Enqueue(obj);
                }

                m_PoolDictionary.Add(pool.tag, objectPool);
            }
        }



        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!m_PoolDictionary.ContainsKey(tag))
            {
                Debug.Log("Object not found");
                return null;

            }

            GameObject objToSpawn = m_PoolDictionary[tag].Dequeue();

            objToSpawn.SetActive(true);
            objToSpawn.transform.parent = null;
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;

            

            m_PoolDictionary[tag].Enqueue(objToSpawn);


            return objToSpawn;
        }
    }
}