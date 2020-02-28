using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public enum Colors { red, green, cyan }
    public class Draw : MonoBehaviour
    {
        public Colors coler;
        private void OnDrawGizmos()
        {
            Gizmos.color = GetColor(coler);
            Gizmos.DrawCube(transform.position, Vector3.one);
        }

        private Color GetColor(Colors e)
        {
            switch (e)
            {
                case Colors.red:
                    return Color.red;
                case Colors.green:
                    return Color.green;
                case Colors.cyan:
                    return Color.cyan;
                default:
                    return Color.black;
            }
        }
    }
}


public enum Colors { red, green, cyan }
public class Draw : MonoBehaviour
{
    public Colors coler;
    private void OnDrawGizmos()
    {
        Gizmos.color = GetColor(coler);
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    private Color GetColor(Colors e)
    {
        switch(e)
        {
            case Colors.red:
                return Color.red;
            case Colors.green:
                return Color.green;
            case Colors.cyan:
                return Color.cyan;
            default:
                return Color.black;
        }
    }
}
