using UnityEngine;
using System.Collections;

public class ButtonUpTrigger : MonoBehaviour {
    public Phil_ActionBase Action;
    public string ButtonName = "Fire1";

    void Update () {
        if (Input.GetButtonUp(ButtonName)) {
            Action.Act();
        }
    }
}
