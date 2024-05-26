using TowerDefence.Configs;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Editor
{
  [CustomEditor(typeof(LevelConfig))]
  public class LevelDataEditor : UnityEditor.Editor
  {
    private LevelConfig _levelConfig;

    private void OnEnable() =>
      _levelConfig = (LevelConfig)target;

    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      GUILayout.Space(10);

      var currentEvent = Event.current;
      var dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
      GUI.Box(dropArea, "Drag Transform here to add waypoint");
      switch (currentEvent.type)
      {
        case EventType.DragUpdated:
        case EventType.DragPerform:
          if (!dropArea.Contains(currentEvent.mousePosition))
            break;

          DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

          if (currentEvent.type == EventType.DragPerform)
          {
            DragAndDrop.AcceptDrag();

            foreach (var draggedObject in DragAndDrop.objectReferences)
            {
              var draggedGameObject = draggedObject as GameObject;
              if (draggedGameObject != null)
              {
                var transform = draggedGameObject.transform;
                var waypoint = transform.position;
                _levelConfig.EnemyWaypoints.Add(waypoint);
              }
            }

            EditorUtility.SetDirty(_levelConfig);
          }

          break;
      }
    }
  }
}