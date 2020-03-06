using UnityEngine;
using System.Collections;

public class ButtonDownTrigger : MonoBehaviour {
    public Phil_ActionBase Action;
    public string ButtonName = "Fire1";

    void Update () {
        if (Input.GetButtonDown(ButtonName)) {
            Action.Act();
        }
    }
}
