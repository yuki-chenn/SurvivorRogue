using Survivor.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace Survivor.Utils
{
    class XmlUtil
    {
        private static XmlDocument _xml;
        private static XmlDocument Xml
        {
            get
            {
                if (_xml == null) _xml = new XmlDocument();
                return _xml;
            }
        }

        

        public static T LoadXMLTpl<T, V>(string path)
        where T : BaseTpl<V>, new()
        where V : BaseTplInfo, new()
        {
            string xmlContent;

            if (Application.platform == RuntimePlatform.Android)
            {
                // In Android, use UnityWebRequest to get the file content
                string fullPath = "jar:file://" + Application.dataPath + "!/assets/" + path;
                xmlContent = LoadFileFromStreamingAssetsAndroid(fullPath);
            }
            else
            {
                // On other platforms, use standard file I/O
                string fullPath = Path.Combine(Application.streamingAssetsPath, path);
                xmlContent = File.ReadAllText(fullPath);
            }

            // Deserialize the XML content
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T tpl;

            using (StringReader reader = new StringReader(xmlContent))
            {
                tpl = (T)serializer.Deserialize(reader);
            }

            // Convert the deserialized List<V> to Dictionary<int, V>
            Dictionary<int, V> dataDictionary = new Dictionary<int, V>();
            foreach (var item in tpl.List)
            {
                dataDictionary.Add(item.ID, item);
            }

            tpl.Dic = dataDictionary;

            return tpl;
        }

        private static string LoadFileFromStreamingAssetsAndroid(string fullPath)
        {
            UnityWebRequest www = UnityWebRequest.Get(fullPath);
            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                // Wait until the request is done
            }

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Failed to load XML file from {fullPath}: {www.error}");
                return null;
            }
            else
            {
                return www.downloadHandler.text;
            }
        }

    }
}
