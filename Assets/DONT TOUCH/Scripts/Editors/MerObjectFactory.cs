using UnityEditor;
using UnityEngine;

public static class MerObjectFactory
{
    [MenuItem("GameObject/MER Objects/Door/LCZ Door", false, 20)]
    private static void CreateLczDoor() => CreateDoor(SchematicDoorType.Lcz);

    [MenuItem("GameObject/MER Objects/Door/HCZ Door", false, 21)]
    private static void CreateHczDoor() => CreateDoor(SchematicDoorType.Hcz);

    [MenuItem("GameObject/MER Objects/Door/EZ Door", false, 22)]
    private static void CreateEzDoor() => CreateDoor(SchematicDoorType.Ez);

    [MenuItem("GameObject/MER Objects/Door/Bulk Door", false, 23)]
    private static void CreateBulkDoor() => CreateDoor(SchematicDoorType.Bulkdoor);

    [MenuItem("GameObject/MER Objects/Door/Gate", false, 24)]
    private static void CreateGate() => CreateDoor(SchematicDoorType.Gate);

    [MenuItem("GameObject/MER Objects/Text Toy", false, 40)]
    private static void CreateText()
    {
        GameObject gameObject = CreateObject("TextToy");
        Undo.AddComponent<TextToyComponent>(gameObject);
    }

    [MenuItem("GameObject/MER Objects/Interactable Toy", false, 41)]
    private static void CreateInteractable()
    {
        GameObject gameObject = CreateObject("InteractableToy");
        Undo.AddComponent<InteractableToyComponent>(gameObject);
    }

    [MenuItem("GameObject/MER Objects/Waypoint Toy", false, 42)]
    private static void CreateWaypoint()
    {
        GameObject gameObject = CreateObject("WaypointToy");
        Undo.AddComponent<WaypointToyComponent>(gameObject);
    }

    [MenuItem("GameObject/MER Objects/Shooting Target/Class-D", false, 60)]
    private static void CreateClassDTarget() => CreateShootingTarget(SchematicTargetType.ClassD);

    [MenuItem("GameObject/MER Objects/Shooting Target/Sport", false, 61)]
    private static void CreateSportTarget() => CreateShootingTarget(SchematicTargetType.Sport);

    [MenuItem("GameObject/MER Objects/Shooting Target/Binary", false, 62)]
    private static void CreateBinaryTarget() => CreateShootingTarget(SchematicTargetType.Binary);

    private static void CreateDoor(SchematicDoorType doorType)
    {
        GameObject gameObject = CreateObject(doorType + "Door");
        DoorComponent component = Undo.AddComponent<DoorComponent>(gameObject);
        component.DoorType = doorType;
        EditorUtility.SetDirty(component);
    }

    private static void CreateShootingTarget(SchematicTargetType targetType)
    {
        GameObject gameObject = CreateObject(targetType + "Target");
        ShootingTargetComponent component = Undo.AddComponent<ShootingTargetComponent>(gameObject);
        component.TargetType = targetType;
        EditorUtility.SetDirty(component);
    }

    private static GameObject CreateObject(string objectName)
    {
        GameObject gameObject = new GameObject(objectName);
        Undo.RegisterCreatedObjectUndo(gameObject, "Create " + objectName);

        Transform parent = Selection.activeTransform;
        if (parent != null)
        {
            Undo.SetTransformParent(gameObject.transform, parent, "Parent " + objectName);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;

            if (parent.GetComponentInParent<Schematic>() == null)
                Debug.LogWarning(objectName + " is not inside a Schematic and will not be exported until it is parented to one.");
        }
        else
        {
            Debug.LogWarning(objectName + " was created at scene root. Parent it to a Schematic before compiling.");
        }

        Selection.activeGameObject = gameObject;
        EditorGUIUtility.PingObject(gameObject);
        return gameObject;
    }
}
