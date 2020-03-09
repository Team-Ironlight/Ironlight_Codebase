using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{

	//public SOEnemy enemyType;

	public GameObject modelHolder;
	private string poolTag;

	Vector3 position;
	Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
		poolTag = " "; //enemyType.enemyType.ToString();
		Debug.Log(poolTag);
		position = transform.position;
		rotation = transform.rotation;

		SpawnEnemy(poolTag, position, rotation);
    }

	void SpawnEnemy(string tag, Vector3 pos, Quaternion rot)
	{
		//EnemyPooler.Instance.SpawnFromPool(tag,pos,rot);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
