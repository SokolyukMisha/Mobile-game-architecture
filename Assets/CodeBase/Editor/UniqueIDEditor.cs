using System;
using System.Linq;
using CodeBase.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueID))]
    public class UniqueIDEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueID uniqueID = (UniqueID)target;
            ClearID(uniqueID);
            if (string.IsNullOrEmpty(uniqueID.id))
                Generate(uniqueID);
            else
            {
                UniqueID[] uniqueIds = FindObjectsOfType<UniqueID>();
                if(uniqueIds.Any(other => other != uniqueID && other.id == uniqueID.id))
                    Generate(uniqueID);
            }
        }

        private void Generate(UniqueID uniqueID)
        {
            uniqueID.id = $"{uniqueID.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueID);
                EditorSceneManager.MarkSceneDirty(uniqueID.gameObject.scene);
            }
        }

        private void ClearID(UniqueID uniqueID)
        {
            uniqueID.id = null;
        }
    }
}