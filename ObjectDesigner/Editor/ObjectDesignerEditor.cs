using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectDesigner))]
public class ObjectDesignerEditor : Editor
{
    //#if UNITY_EDITOR
    string CurrentStringKeyBoard;

    int CurrentCountObj;

    Transform transform;

    bool toolBarActivity = true;
    bool toolBarObjectAcitivity = true;

    public override void OnInspectorGUI()
    {
        ObjectDesigner script = (ObjectDesigner)target;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Nome da atividade");
        script.m_nameActivity = EditorGUILayout.TextField(script.m_nameActivity);
        EditorGUILayout.EndHorizontal();

        Space(1);

        EditorGUILayout.HelpBox("Estes objetos nao podem ser salvos inativos dentro da cena", MessageType.Warning);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Build"))
        {
            script.m_object = new Transform[script.m_amountObject];
        }

        if (GUILayout.Button("Clear"))
        {
            script.m_object = new Transform[0];
        }

        EditorGUILayout.LabelField("Quantidade");
        script.m_amountObject = EditorGUILayout.IntField(script.m_amountObject);

        EditorGUILayout.EndHorizontal();

        if (script.m_object != null)
        {
            for (int i = 0; i < script.m_object.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();

                script.m_object[i] = EditorGUILayout.ObjectField(script.m_object[i], typeof(Transform), true) as Transform;

                EditorGUILayout.EndHorizontal();
            }
        }

        Space(2);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Load Arquivo"))
        {
            bool loadFile;

            if (script.m_assetActivity != null)
                loadFile = script.loadResourceActivity(script.m_assetActivity);
            else
                loadFile = script.loadResourceActivity(null);


            if (loadFile)
                EditorUtility.DisplayDialog("Carregado", "Arquivo Carregado com Sucesso", "Prosseguir");
            else
            {
                EditorUtility.DisplayDialog("Carregado", "Arquivo nao encontrado", "Prosseguir");
            }
        }

        script.m_assetActivity = EditorGUILayout.ObjectField(script.m_assetActivity, typeof(TextAsset), false) as TextAsset;

        EditorGUILayout.EndHorizontal();

        Space(1);

        if (GUILayout.Button("Criar Arquivo"))
        {
            bool createFile = script.createFile();
            if (createFile)
            {
                EditorUtility.DisplayDialog("Arquivo", "Arquivo Criado com Sucesso", "Prosseguir");
            }
            else
            {
                EditorUtility.DisplayDialog("Arquivo", "Arquivo Ja existe", "Prosseguir");
            }
        }

        Space(1);

        if (GUILayout.Button("Salvar Arquivo"))
        {
            bool booleanSaveFile;
            booleanSaveFile = script.saveFileActivity();
            if (booleanSaveFile)
            {
                EditorUtility.DisplayDialog("Arquivo", "Arquivo Salvado com Sucesso", "Prosseguir");
            }
            else
            {
                if (EditorUtility.DisplayDialog("Arquivo", "Nao foi possivel salvar o arquivo", "Ok"))
                {
                    //bool createFile = script.createFile();
                    //if (createFile)
                    //{
                    //    EditorUtility.DisplayDialog("Arquivo", "Arquivo Criado com Sucesso", "Prosseguir");
                    //}
                }
            }
        }

        Space(1);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Criar um Object de Designer"))
        {
            script.saveActivityObjectDesigner();
        }

        if (GUILayout.Button("Limpar Lista"))
        {
            if (EditorUtility.DisplayDialog("Deletar", "Deseja Limpar a lista de Objetos", "Sim", "Nao"))
            {
                script.m_activityDesigner = null;
            }
        }

        EditorGUILayout.EndHorizontal();
        Space(1);

        if (script.m_activityDesigner != null)
        {
            toolBarActivity = EditorGUILayout.Foldout(toolBarActivity, "Activity Designer");

            if (toolBarActivity)
            {
                if (script.m_activityDesigner != null)
                {
                    int countObjectPerActivity = -1;
                    foreach (ResourceObjectDesigner activityObjectDesigner in script.m_activityDesigner)
                    {
                        countObjectPerActivity++;
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(activityObjectDesigner + " " + countObjectPerActivity.ToString());

                        if (GUILayout.Button("Delete Objeto"))
                        {
                            if (EditorUtility.DisplayDialog("Deletar Objeto",
                                                "Deseja deletar este Objeto?"
                                                , "Sim", "Nao Desejo"))
                            {
                                script.deleteObjectDesigner(countObjectPerActivity);
                                if (script.m_activityDesigner.Count == 0) script.m_activityDesigner = null;
                                return;
                            }
                        }

                        if (GUILayout.Button("Save Designer Atual"))
                        {
                            if (EditorUtility.DisplayDialog("Atualizar Objeto",
                                                "Deseja atualizar este Objeto?"
                                                , "Sim", "Nao Desejo"))
                            { }
                            return;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    void Space(int _quantidade)
    {
        for (int i = 0; i < _quantidade; i++)
        {
            EditorGUILayout.Space();
        }
    }
}