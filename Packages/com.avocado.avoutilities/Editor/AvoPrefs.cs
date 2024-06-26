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


    public static string SetGameObject(this GameObject obj)
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

    public static GameObject GetGameObject(string obj)
    {
        if (obj == "") return null;

        GameObject find = GameObject.Find(obj);

        if (find == null)
        {
            var objects = Resources.FindObjectsOfTypeAll<GameObject>();


            for (int i = 0; i < objects.Length; i++)
            {
                var _object = objects[i];
                if (SetGameObject(_object) == obj)
                {
                    find = _object;
                    return find;
                }
            }
        }


        return find;
    }
    public static string SetComponent<T>(this T Component) where T : Component
    {
        if (Component == null) return null;

        string path = SetGameObject(Component.gameObject) + "/" + Component.GetType().Name;

        return path;
    }

    public static T GetComponent<T>(string Component) where T : Component
    {
        if (string.IsNullOrEmpty(Component) || string.IsNullOrWhiteSpace(Component)) return null;

        var data = Component.Split('/');

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

        var gameObject = GetGameObject(objectPath);

        if (gameObject == null) return null;

        T component = gameObject.GetComponent<T>();

        return component;
    }
}
#endif