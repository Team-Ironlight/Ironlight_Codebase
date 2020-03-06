using UnityEngine;
using System.Collections;

public class LockCursorAction : Phil_ActionBase
{
    public override void Act() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
