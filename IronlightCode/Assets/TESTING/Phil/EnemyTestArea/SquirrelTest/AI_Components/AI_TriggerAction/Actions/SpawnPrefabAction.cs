// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class SpawnPrefabAction : Phil_ActionBase
{
  public GameObject Prefab;
  public bool AttachToSelf = false;

  public bool IsTemporary;
  public float TemporaryLifespan;

  public override void Act() {
    var instance = GameObject.Instantiate(Prefab, transform.position, transform.rotation) as GameObject;

    if (IsTemporary) {
      GameObject.Destroy(instance, TemporaryLifespan);
    }

    if (AttachToSelf) {
      instance.transform.parent = transform;
    }
  }
}
