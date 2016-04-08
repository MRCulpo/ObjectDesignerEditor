using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class ObjectDesignerManager : MonoBehaviour
{
    Transform[] m_objects;
    List<ResourceObjectDesigner> m_struct;
    public TextAsset m_textAsset;

    public void create(string _path)
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

    public void create()
    {
        if (m_textAsset != null)
            m_struct = new List<ResourceObjectDesigner>(JsonMapper.ToObject<List<ResourceObjectDesigner>>(m_textAsset.text));

        m_objects = new Transform[m_struct[0].m_structObjectDesigner.Count];

        for (int i = 0; i < m_objects.Length; i++)
        {
            m_objects[i] = GameObject.Find(m_struct[0].m_structObjectDesigner[i].m_nameRef).transform;
        }
    }

    public void randomDesigner()
    {
        if (m_textAsset != null)
        {
            ResourceObjectDesigner _resourceObjectDesigner = m_struct[Random.Range(0, m_struct.Count)];

            for (int i = 0; i < m_objects.Length; i++)
            {
                this.m_objects[i].gameObject.SetActive(_resourceObjectDesigner.m_structObjectDesigner[i].m_activity);

                if (this.m_objects[i].gameObject.activeSelf)
                {
                    Vector3 _rot = _resourceObjectDesigner.m_structObjectDesigner[i].m_rotationRef;

                    this.m_objects[i].position = _resourceObjectDesigner.m_structObjectDesigner[i].m_positionRef;
                    this.m_objects[i].localScale = _resourceObjectDesigner.m_structObjectDesigner[i].m_scaleRef;
                    this.m_objects[i].localRotation = new Quaternion(_rot.x, _rot.y, _rot.z, 1);
                }
            }
        }
    }

    public void randomDesigner(string _nameAsset)
    {
        if (m_textAsset == null)
            create(_nameAsset);

        if (m_textAsset != null)
        {
            ResourceObjectDesigner _resourceObjectDesigner = m_struct[Random.Range(0, m_struct.Count)];

            for (int i = 0; i < m_objects.Length; i++)
            {
                this.m_objects[i].gameObject.SetActive(_resourceObjectDesigner.m_structObjectDesigner[i].m_activity);

                if (this.m_objects[i].gameObject.activeSelf)
                {
                    Vector3 _rot = _resourceObjectDesigner.m_structObjectDesigner[i].m_rotationRef;

                    this.m_objects[i].position = _resourceObjectDesigner.m_structObjectDesigner[i].m_positionRef;
                    this.m_objects[i].localScale = _resourceObjectDesigner.m_structObjectDesigner[i].m_scaleRef;
                    this.m_objects[i].localRotation = new Quaternion(_rot.x, _rot.y, _rot.z, 1);
                }
            }
        }
    }
}
