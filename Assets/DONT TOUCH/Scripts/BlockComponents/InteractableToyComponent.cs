using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractableToyComponent : SchematicBlock
{
    public InteractableShape Shape = InteractableShape.Box;

    [Min(0f)]
    public float InteractionDuration;

    public bool IsLocked;

    public override BlockType BlockType => BlockType.Interactable;

    public override bool Compile(SchematicBlockData block, Schematic _)
    {
        block.Rotation = transform.localEulerAngles;
        block.Scale = transform.localScale;
        block.BlockType = BlockType.Interactable;
        block.Properties = new Dictionary<string, object>
        {
            { "Shape", Shape },
            { "InteractionDuration", InteractionDuration },
            { "IsLocked", IsLocked },
        };

        return true;
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 previous = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = IsLocked
            ? new Color(1f, 0.2f, 0.15f, 0.9f)
            : new Color(1f, 0.85f, 0.1f, 0.9f);

        switch (Shape)
        {
            case InteractableShape.Sphere:
                Gizmos.DrawWireSphere(Vector3.zero, 0.5f);
                break;
            case InteractableShape.Capsule:
                Gizmos.DrawWireSphere(Vector3.up * 0.25f, 0.5f);
                Gizmos.DrawWireSphere(Vector3.down * 0.25f, 0.5f);
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(1f, 0.5f, 1f));
                break;
            default:
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                break;
        }

        Gizmos.matrix = previous;
    }
}
