using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShootingTargetComponent : SchematicBlock
{
    public SchematicTargetType TargetType = SchematicTargetType.ClassD;

    public override BlockType BlockType => BlockType.ShootingTarget;

    public override bool Compile(SchematicBlockData block, Schematic _)
    {
        block.Rotation = transform.localEulerAngles;
        block.Scale = transform.localScale;
        block.BlockType = BlockType.ShootingTarget;
        block.Properties = new Dictionary<string, object>
        {
            { "TargetType", TargetType },
        };

        return true;
    }

    private void OnDrawGizmos()
    {
        Color color = TargetType switch
        {
            SchematicTargetType.Sport => new Color(0.2f, 0.75f, 1f, 0.8f),
            SchematicTargetType.Binary => new Color(0.75f, 0.3f, 1f, 0.8f),
            _ => new Color(1f, 0.5f, 0.15f, 0.8f),
        };

        Matrix4x4 previous = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = color;
        Gizmos.DrawWireCube(new Vector3(0f, 1.1f, 0f), new Vector3(0.85f, 1.65f, 0.12f));
        Gizmos.DrawWireSphere(new Vector3(0f, 2.15f, 0f), 0.35f);
        Gizmos.DrawLine(new Vector3(-0.5f, 0f, 0f), new Vector3(0.5f, 0f, 0f));
        Gizmos.matrix = previous;
    }
}
