using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System;

public class ObjectDesignerManager : MonoBehaviour
{
    Transform[] m_objects;
    List<ResourceObjectDesigner> m_struct;
    public TextAsset m_textAsset;

    void Awake()
    {
        create();
    }

    public void create(string _path)
    {
        try
        {
            if (m_textAsset == null)
                m_textAsset = Resources.Load("DesignerActivity/" + _path, typeof(TextAsset)) as TextAsset;

            if (m_textAsset != null)
            {
                m_struct = new List<ResourceObjectDesigner>(JsonMapper.ToObject<List<ResourceObjectDesigner>>(m_textAsset.text));

                m_objects = new Transform[m_struct[0].m_structObjectDesigner.Count];

                for (int i = 0; i < m_objects.Length; i++)
                {
                    m_objects[i] = GameObject.Find(m_struct[0].m_structObjectDesigner[i].m_nameRef).transform;
                }
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }

    public void create()
    {
        try
        {
            if (m_textAsset != null)
                m_struct = new List<ResourceObjectDesigner>(JsonMapper.ToObject<List<ResourceObjectDesigner>>(m_textAsset.text));

            m_objects = new Transform[m_struct[0].m_structObjectDesigner.Count];

            for (int i = 0; i < m_objects.Length; i++)
            {
                m_objects[i] = GameObject.Find(m_struct[0].m_structObjectDesigner[i].m_nameRef).transform;
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }

    }

    public void randomDesigner()
    {
        try
        {
            if (m_textAsset != null)
            {
                ResourceObjectDesigner _resourceObjectDesigner = m_struct[UnityEngine.Random.Range(0, m_struct.Count)];

                for (int i = 0; i < m_objects.Length; i++)
                {
                    m_objects[i].gameObject.SetActive(_resourceObjectDesigner.m_structObjectDesigner[i].m_activity);

                    if (m_objects[i].gameObject.activeSelf)
                    {
                        Vector3 _rot = _resourceObjectDesigner.m_structObjectDesigner[i].m_rotationRef;

                        m_objects[i].position = _resourceObjectDesigner.m_structObjectDesigner[i].m_positionRef;
                        m_objects[i].localScale = _resourceObjectDesigner.m_structObjectDesigner[i].m_scaleRef;
                        m_objects[i].localRotation = new Quaternion(_rot.x, _rot.y, _rot.z, 1);
                    }
                }
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }

    public void randomDesigner(string _nameAsset)
    {
        try
        {
            if (m_textAsset == null)
                create(_nameAsset);

            if (m_textAsset != null)
            {
                ResourceObjectDesigner _resourceObjectDesigner = m_struct[UnityEngine.Random.Range(0, m_struct.Count)];

                for (int i = 0; i < m_objects.Length; i++)
                {
                    m_objects[i].gameObject.SetActive(_resourceObjectDesigner.m_structObjectDesigner[i].m_activity);

                    if (m_objects[i].gameObject.activeSelf)
                    {
                        Vector3 _rot = _resourceObjectDesigner.m_structObjectDesigner[i].m_rotationRef;

                        m_objects[i].position = _resourceObjectDesigner.m_structObjectDesigner[i].m_positionRef;
                        m_objects[i].localScale = _resourceObjectDesigner.m_structObjectDesigner[i].m_scaleRef;
                        m_objects[i].localRotation = new Quaternion(_rot.x, _rot.y, _rot.z, 1);
                    }
                }
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }
}