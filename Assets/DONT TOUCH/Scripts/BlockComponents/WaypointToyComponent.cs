using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointToyComponent : SchematicBlock
{
    private const float RuntimeScaleMultiplier = 256f;

    public override BlockType BlockType => BlockType.Waypoint;

    public override bool Compile(SchematicBlockData block, Schematic _)
    {
        block.Rotation = transform.localEulerAngles;
        block.Scale = transform.localScale * RuntimeScaleMultiplier;
        block.BlockType = BlockType.Waypoint;
        block.Properties = new Dictionary<string, object>();
        return true;
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 previous = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0.65f, 0.25f, 1f, 0.9f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.DrawLine(Vector3.left * 0.65f, Vector3.right * 0.65f);
        Gizmos.DrawLine(Vector3.down * 0.65f, Vector3.up * 0.65f);
        Gizmos.DrawLine(Vector3.back * 0.65f, Vector3.forward * 0.65f);
        Gizmos.matrix = previous;
    }
}
