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
            for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
