using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public static class AvoObjects
{
    public static void DeleteAllChildren(this GameObject gameObject)
    {
        if (gameObject.transform.childCount > 0)
        {
            while (gameObject.transform.childCount > 0)
            {
                GameObject.DestroyImmediate(gameObject.transform.GetChild(0));
            }
        }
    }
}
