using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextToyComponent : SchematicBlock
{
    [TextArea(2, 8)]
    public string Text = "Custom Text";

    [Tooltip("Width and height of the in-game text canvas.")]
    public Vector2 DisplaySize = Vector2.one;

    public override BlockType BlockType => BlockType.Text;

    public override bool Compile(SchematicBlockData block, Schematic _)
    {
        block.Rotation = transform.localEulerAngles;
        block.Scale = transform.localScale;
        block.BlockType = BlockType.Text;
        block.Properties = new Dictionary<string, object>
        {
            { "Text", Text },
            { "DisplaySize", (SerializableVector2)(DisplaySize / 20f) },
        };

        return true;
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 previous = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0.2f, 1f, 0.9f, 0.85f);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(DisplaySize.x, DisplaySize.y, 0.025f));
        Gizmos.matrix = previous;

#if UNITY_EDITOR
        UnityEditor.Handles.color = new Color(0.2f, 1f, 0.9f, 1f);
        UnityEditor.Handles.Label(transform.position, string.IsNullOrEmpty(Text) ? "TextToy" : Text);
#endif
    }
}
