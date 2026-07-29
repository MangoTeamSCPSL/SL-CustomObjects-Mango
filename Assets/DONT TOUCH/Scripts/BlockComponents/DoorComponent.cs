using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DoorComponent : SchematicBlock
{
    [Tooltip("The in-game door prefab.")]
    public SchematicDoorType DoorType = SchematicDoorType.Lcz;

    [Tooltip("Whether the door is open when the schematic spawns.")]
    public bool IsOpen;

    [Tooltip("Whether the door is locked by the SpecialDoorFeature lock.")]
    public bool IsLocked;

    [Tooltip("Keycard permissions required to operate the door.")]
    public KeycardPermissions RequiredPermissions = KeycardPermissions.None;

    [Tooltip("If enabled, a keycard must contain every selected permission.")]
    public bool RequireAll = true;

    public override BlockType BlockType => BlockType.Door;

    public override bool Compile(SchematicBlockData block, Schematic _)
    {
        block.Rotation = transform.localEulerAngles;
        block.Scale = transform.localScale;
        block.BlockType = BlockType.Door;
        block.Properties = new Dictionary<string, object>
        {
            { "DoorType", DoorType },
            { "IsOpen", IsOpen },
            { "IsLocked", IsLocked },
            { "RequiredPermissions", RequiredPermissions },
            { "RequireAll", RequireAll },
        };

        return true;
    }

    private void OnDrawGizmos()
    {
        Color color = DoorType switch
        {
            SchematicDoorType.Lcz => new Color(0.25f, 0.75f, 1f, 0.35f),
            SchematicDoorType.Hcz => new Color(1f, 0.45f, 0.2f, 0.35f),
            SchematicDoorType.Ez => new Color(0.7f, 0.85f, 1f, 0.35f),
            SchematicDoorType.Bulkdoor => new Color(1f, 0.2f, 0.15f, 0.35f),
            SchematicDoorType.Gate => new Color(1f, 0.75f, 0.15f, 0.35f),
            _ => new Color(1f, 0f, 1f, 0.35f),
        };

        Vector3 size = DoorType switch
        {
            SchematicDoorType.Bulkdoor => new Vector3(3.4f, 3.5f, 0.45f),
            SchematicDoorType.Gate => new Vector3(4.5f, 4.2f, 0.5f),
            _ => new Vector3(2.1f, 3.1f, 0.3f),
        };

        Matrix4x4 previous = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = color;
        Gizmos.DrawCube(Vector3.up * size.y * 0.5f, size);
        Gizmos.color = new Color(color.r, color.g, color.b, 1f);
        Gizmos.DrawWireCube(Vector3.up * size.y * 0.5f, size);

        float frame = Mathf.Max(0.08f, size.x * 0.045f);
        Gizmos.DrawCube(new Vector3(-size.x * 0.5f, size.y * 0.5f, 0f), new Vector3(frame, size.y + frame, size.z * 1.4f));
        Gizmos.DrawCube(new Vector3(size.x * 0.5f, size.y * 0.5f, 0f), new Vector3(frame, size.y + frame, size.z * 1.4f));
        Gizmos.DrawCube(new Vector3(0f, size.y, 0f), new Vector3(size.x + frame, frame, size.z * 1.4f));

        Gizmos.matrix = previous;
    }
}
