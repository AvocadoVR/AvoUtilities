#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class AvoPref
{
    public static string SetColor(this Color color)
    {
        return $"{color.r}/{color.g}/{color.b}/{color.a}";
    }

    public static Color GetColor(string color)
    {
        var rankColorData = color.Split('/');

        Color _color = new Color(float.Parse(rankColorData[0]), float.Parse(rankColorData[1]), float.Parse(rankColorData[2]), float.Parse(rankColorData[3]));

        return _color;
    }

    public static string SetTexture(this Texture2D texture)
    {
        return AssetDatabase.GetAssetPath(texture);
    }

    public static Texture2D GetTexture(string texture)
    {
        Texture2D _texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texture);

        return _texture == null ? null : _texture;

    }


    public static string[] SetEnum(Type enumType)
    {
        if (!enumType.IsEnum) return null;

        var enumNames = Enum.GetNames(enumType);

        return enumNames.ToArray();
    }

    public static TEnum GetEnum<TEnum>(string[] enumValues) where TEnum : Enum
    {
        if (enumValues == null || enumValues.Length == 0)
        {
            throw new ArgumentException("Enum values array cannot be null or empty.");
        }

        TEnum _enum = default;

        foreach (var enumValue in enumValues)
        {
            _enum = (TEnum)Enum.Parse(typeof(TEnum), enumValue);
        }

        return _enum;
    }


    public static string SetSceneObject(this GameObject sceneObject)
    {
        if (sceneObject == null) return "";

        string path = "/" + sceneObject.name;
        while (sceneObject.transform.parent != null)
        {
            sceneObject = sceneObject.transform.parent.gameObject;
            path = "/" + sceneObject.name + path;
        }
        return path;
    }

    public static GameObject GetSceneObject(string sceneObject)
    {
        if (sceneObject == "") return null;

        GameObject find = GameObject.Find(sceneObject);

        if (find == null)
        {
            var objects = Resources.FindObjectsOfTypeAll<GameObject>();


            for (int i = 0; i < objects.Length; i++)
            {
                var _object = objects[i];
                if (SetSceneObject(_object) == sceneObject)
                {
                    find = _object;
                    return find;
                }
            }
        }


        return find;
    }

    public static string SetAsset(this UnityEngine.Object asset)
    {
        return AssetDatabase.GetAssetPath(asset);
    }

    public static T GetAsset<T>(this string asset) where T : UnityEngine.Object
    {
        return AssetDatabase.LoadAssetAtPath<T>(asset);
    }

    public static string SetComponent<T>(this T component) where T : Component
    {
        if (component == null) return null;

        string path = SetSceneObject(component.gameObject) + "/" + component.GetType().Name;

        return path;
    }

    public static T GetComponent<T>(string component) where T : Component
    {
        if (string.IsNullOrEmpty(component) || string.IsNullOrWhiteSpace(component)) return null;

        var data = component.Split('/');

        string objectPath = "";

        for (int i = 0; i < data.Length - 1; i++)
        {
            if (i == 0)
            {
                objectPath += data[i];
            }
            else
            {
                objectPath += "/" + data[i];
            }
        }

        var gameObject = GetSceneObject(objectPath);

        if (gameObject == null) return null;

        T actualComponent = gameObject.GetComponent<T>();

        return actualComponent;
    }


    #region To Class

    public static Color ToColor(this string s)
    {
        return GetColor(s);
    }

    public static Texture2D ToTexture(this string s)
    {
        return GetTexture(s);
    }
    
    public static GameObject ToSceneObject(this string s)
    {
        return GetSceneObject(s);
    }

    public static T ToAsset<T>(this string s) where T : UnityEngine.Object
    {
        return GetAsset<T>(s);
    }
    
    public static T ToComponent<T>(this string s) where T : Component
    {
        return GetComponent<T>(s);
    }

    #endregion
}
#endif