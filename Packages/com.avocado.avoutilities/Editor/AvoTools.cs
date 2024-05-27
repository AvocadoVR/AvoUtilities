using System.IO;
using UnityEditor;
using UnityEngine;

namespace AvoUtils.Editor
{
    public static class AvoTools
    {

        public static void CreateMaterial(GameObject gameObject, string folderName, string materialName, Shader shader)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();

            if (gameObject == null) return;
            if (renderer == null) return;


            string path = Application.dataPath + folderName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }

            Material mat = new Material(shader)
            {
                name = materialName
            };

            string materialPath = "Assets/" + folderName + "/" + materialName + ".mat";

            AssetDatabase.CreateAsset(mat, materialPath);
            AssetDatabase.Refresh();
        }

        public static Material CreateMaterial(GameObject gameObject, string folderName, string materialName, Shader shader, bool returnMaterial)
        {
            CreateMaterial(gameObject, folderName, materialName, shader);

            if (returnMaterial)
            {
                string materialPath = "Assets/" + folderName + "/" + materialName + ".mat";
                return AssetDatabase.LoadAssetAtPath<Material>(materialPath);
            }
            else
            {
                return null;
            }
        }


        public static void CreateSprite(Texture2D texture, string path, string spriteName)
        {
            string _path = path;
            Rect rect = new Rect(0, 0, texture.width, texture.height);

            Vector2 pivot = new Vector2(0.5f, 0.5f);

            Sprite sprite = Sprite.Create(texture, rect, pivot);

            string spritePath = _path + "/" + spriteName + ".asset";

            if (!path.ToLower().Contains("assets"))
            {
                _path = "Assets/" + path;
            }

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            
            AssetDatabase.CreateAsset(sprite, spritePath);
            AssetDatabase.Refresh();
        }

        public static Sprite CreateSprite(Texture2D texture, string path, string spriteName, bool returnSprite)
        {
            CreateSprite(texture, path, spriteName);

            if (returnSprite)
            {
                string spritePath = path + "/" + spriteName + ".asset";
                return AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
            }
            else
            {
                return null;
            }
        }
    }
}