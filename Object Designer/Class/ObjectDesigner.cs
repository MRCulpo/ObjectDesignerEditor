using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class ObjectDesigner : MonoBehaviour
{

    private const string PATH = "/Object Designer/Activity Resource/{0}.txt";

    public string m_nameActivity = "none";
    public int m_amountObject;

    public TextAsset m_assetActivity;
    public Transform[] m_object;
    public List<ResourceObjectDesigner> m_activityDesigner;


    public bool loadResourceActivity(TextAsset _assetText)
    {
        try
        {
            string _text = _assetText.text;

            if (_assetText == null)
            {
                ///Busca o caminho do Json no Editor
                string path = string.Format(Application.dataPath + PATH, m_nameActivity);
                //Usa o FILE para buscar a informação dentro do caminhos passado por parametro
                _text = File.ReadAllText(path);
            }

            ///Transforma a informação do json e alimenta a lista de teclados virtuais configurados
            if (_text != null)
                this.m_activityDesigner = new List<ResourceObjectDesigner>(JsonMapper.ToObject<List<ResourceObjectDesigner>>(_text));


            /// Load objects ref
            this.m_amountObject = this.m_activityDesigner[0].m_structObjectDesigner.Count;
            this.m_nameActivity = _assetText.name;
            this.m_object = new Transform[this.m_amountObject];

            for (int i = 0; i < this.m_amountObject; i++)
            {
                
                GameObject obj = GameObject.Find(this.m_activityDesigner[0].m_structObjectDesigner[i].m_nameRef).gameObject;
                print(obj.name);
                this.m_object[i] = obj.transform;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public void saveActivityObjectDesigner()
    {
        if (m_activityDesigner == null)
            m_activityDesigner = new List<ResourceObjectDesigner>();


        ResourceObjectDesigner resource = new ResourceObjectDesigner();
        resource.m_structObjectDesigner = new List<StructObjectDesigner>();

        for (int i = 0; i < m_object.Length; i++)
        {
            StructObjectDesigner _object = new StructObjectDesigner();
            if (m_object[i] != null)
            {
                _object.m_nameRef = m_object[i].name;
                _object.m_idRef = m_object[i].GetInstanceID();
                _object.m_activity = m_object[i].gameObject.activeSelf;
                _object.m_positionRef = m_object[i].gameObject.transform.position;
                _object.m_scaleRef = m_object[i].gameObject.transform.localScale;
                _object.m_rotationRef = new Vector3(m_object[i].gameObject.transform.rotation.x, m_object[i].gameObject.transform.rotation.y, m_object[i].gameObject.transform.rotation.z);
            }
            resource.m_structObjectDesigner.Add(_object);
        }

        m_activityDesigner.Add(resource);
    }

    public void deleteObjectDesigner(int _indexObject)
    {
        m_activityDesigner.RemoveAt(_indexObject);
    }

    public bool createFile()
    {
#if UNITY_EDITOR
        string path = string.Format(Application.dataPath + PATH, m_nameActivity);

        if (!File.Exists(path))
            File.Create(path);
        else
            return false;

        return true;
#endif
    }

    public bool saveFileActivity()
    {
#if UNITY_EDITOR

        string path = string.Format(Application.dataPath + "/Object Designer/Activity Resource/{0}.txt", m_nameActivity);
        if (File.Exists(path))
        {
            try
            {
                File.WriteAllText(path, JsonMapper.ToJson(m_activityDesigner));
            }
            catch
            {
                return false;
            }
        }
        else
            return false;
        return true;
#endif
    }
}


public class activityObjectDesigner
{
    public string m_titleRef;
    public bool m_toolBarObjectAcitivity = true;
    public List<ResourceObjectDesigner> m_resource;
}

public class ResourceObjectDesigner
{
    public List<StructObjectDesigner> m_structObjectDesigner { set; get; }
}

public class StructObjectDesigner
{
    public string m_nameRef { set; get; }
    public int m_idRef { set; get; }
    public bool m_activity { set; get; }
    public Vector3 m_positionRef { set; get; }
    public Vector3 m_rotationRef { set; get; }
    public Vector3 m_scaleRef { set; get; }
}