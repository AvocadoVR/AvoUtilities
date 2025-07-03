#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public static class AvoPref
{
    public static void SetColor(this Color color, string key)
    {
        var _color = SetColor(color);

        EditorPrefs.SetString(key, _color);
    }
    
    public static string SetColor(this Color color)
    {
        return $"{color.r}/{color.g}/{color.b}/{color.a}";
    }

    public static Color GetColor(this string color)
    {
        var rankColorData = color.Split('/');

        Color _color = new Color(float.Parse(rankColorData[0]), float.Parse(rankColorData[1]), float.Parse(rankColorData[2]), float.Parse(rankColorData[3]));

        return _color;
    }

    public static void SetTexture(this Texture2D texture, string key)
    {
        var _texture = SetTexture(texture);
        
        EditorPrefs.SetString(key, _texture);
    }

    public static string SetTexture(this Texture2D texture)
    {
        return AssetDatabase.GetAssetPath(texture);
    }

    public static Texture2D GetTexture(this string texture)
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

    public static void SetAsset<T>(this UnityEngine.Object asset, string key) where T : UnityEngine.Object
    {
        var _asset = SetAsset<T>(asset);
        
        EditorPrefs.SetString(key, _asset);
    }

    public static string SetAsset<T>(this UnityEngine.Object asset) where T : UnityEngine.Object
    {
        if (asset == null)
        {
            return null;
        }

        string path = AssetDatabase.GetAssetPath(asset);

        return path;
    }

    public static UnityEngine.Object GetAsset<T>(this string path) where T : UnityEngine.Object
    {
        return AssetDatabase.LoadAssetAtPath<T>(path);
    }

    public static void SetSceneObject(this GameObject obj, string key)
    {
        var _obj = SetSceneObject(obj);
        
        EditorPrefs.SetString(key, _obj);
    }
    
    public static string SetSceneObject(this GameObject obj)
    {
        if (obj == null) return "";

        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }

    public static GameObject GetSceneObject(this string obj)
    {
        if (obj == "") return null;

        GameObject find = GameObject.Find(obj);

        if (find == null)
        {
            var objects = Resources.FindObjectsOfTypeAll<GameObject>();


            for (int i = 0; i < objects.Length; i++)
            {
                var _object = objects[i];
                if (SetSceneObject(_object) == obj)
                {
                    find = _object;
                    return find;
                }
            }
        }


        return find;
    }

    public static void SetComponent<T>(this T component, string key) where T : Component
    {
        var _component = SetComponent(component);
        
        EditorPrefs.SetString(key, _component);
    }
    
    public static string SetComponent<T>(this T component) where T : Component
    {
        if (component == null) return null;

        string path = SetSceneObject(component.gameObject) + "/" + component.GetType().Name;

        return path;
    }

    public static T GetComponent<T>(this string component) where T : Component
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

        T _component = gameObject.GetComponent<T>();

        return _component;
    }
}
#endif