// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Created Date:   02/8/2020
//
// Enhancement : Version 4 - 3/3/2020
using UnityEngine;
using System.Collections;

public class RandomPositionAction : Phil_ActionBase
{
  public GameObject target;
  public Vector3 positionMin = Vector3.zero;
  public Vector3 positionMax = Vector3.one;

  private void Start() {
    if (target == null) {
      target = gameObject;
    }
  }

  public override void Act() {
    float x = Random.Range(positionMin.x, positionMax.x);
    float y = Random.Range(positionMin.y, positionMax.y);
    float z = Random.Range(positionMin.z, positionMax.z);
    target.transform.localPosition = new Vector3(x, y, z);
  }
}
