using UnityEngine;
using System.Collections;

public class StartTrigger : MonoBehaviour {
    public Phil_ActionBase Action;

    private void Start() {
        Action.Act();
    }
}
