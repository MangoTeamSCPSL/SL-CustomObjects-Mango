using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HczClutterComponent : SchematicBlock
{
    [Tooltip("The HCZ decoration spawned in game. The Scene View preview shows its approximate footprint.")]
    public HczClutterType ClutterType = HczClutterType.ClutterSimpleBoxes;

    public override BlockType BlockType => BlockType.HczClutter;

    public override bool Compile(SchematicBlockData block, Schematic _)
    {
        block.Rotation = transform.localEulerAngles;
        block.Scale = transform.localScale;
        block.BlockType = BlockType.HczClutter;
        block.Properties = new Dictionary<string, object>
        {
            { "ClutterType", ClutterType },
        };

        return true;
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 previous = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1f, 0.55f, 0.12f, 0.9f);

        switch (ClutterType)
        {
            case HczClutterType.ClutterPipesLong:
                DrawPipes(5.5f, 3);
                break;
            case HczClutterType.ClutterPipesShort:
                DrawPipes(2.8f, 3);
                break;
            case HczClutterType.ClutterHugeOrangePipes:
                DrawPipes(5.5f, 2, 0.75f);
                break;
            case HczClutterType.ClutterSimpleBoxes:
                DrawBox(new Vector3(-0.8f, 0.55f, 0f), new Vector3(1.4f, 1.1f, 1.2f));
                DrawBox(new Vector3(0.55f, 0.4f, 0.1f), new Vector3(1.1f, 0.8f, 1f));
                DrawBox(new Vector3(0.35f, 1.15f, 0.05f), new Vector3(0.75f, 0.7f, 0.8f));
                break;
            case HczClutterType.ClutterBrokenElectricalBox:
                DrawBox(new Vector3(0f, 1.15f, 0f), new Vector3(1.2f, 1.5f, 0.45f));
                Gizmos.DrawLine(new Vector3(-0.45f, 0.45f, 0f), new Vector3(-0.9f, 0f, 0.45f));
                Gizmos.DrawLine(new Vector3(0.1f, 0.4f, 0f), new Vector3(0.55f, 0f, -0.35f));
                break;
            case HczClutterType.ClutterBoxesLadder:
                for (int i = 0; i < 4; i++)
                    DrawBox(new Vector3(-1.35f + i * 0.85f, 0.35f + i * 0.34f, 0f),
                        new Vector3(0.9f, 0.7f, 1f));
                break;
            case HczClutterType.ClutterTankSupportedShelf:
                DrawShelf();
                break;
            case HczClutterType.ClutterAngledFences:
                DrawFence();
                break;
        }

        Gizmos.matrix = previous;

#if UNITY_EDITOR
        UnityEditor.Handles.color = new Color(1f, 0.55f, 0.12f, 1f);
        UnityEditor.Handles.Label(transform.position + transform.up * 2.6f, ClutterType.ToString());
#endif
    }

    private static void DrawPipes(float length, int count, float spacing = 0.42f)
    {
        for (int i = 0; i < count; i++)
        {
            float y = 0.55f + i * spacing;
            Gizmos.DrawWireCube(new Vector3(0f, y, 0f), new Vector3(length, 0.22f, 0.22f));
        }
    }

    private static void DrawShelf()
    {
        Gizmos.DrawWireCube(new Vector3(0f, 0.15f, 0f), new Vector3(3.2f, 0.15f, 1.1f));
        Gizmos.DrawWireCube(new Vector3(0f, 1.25f, 0f), new Vector3(3.2f, 0.15f, 1.1f));
        Gizmos.DrawLine(new Vector3(-1.5f, 0f, -0.45f), new Vector3(-1.5f, 2.2f, -0.45f));
        Gizmos.DrawLine(new Vector3(1.5f, 0f, -0.45f), new Vector3(1.5f, 2.2f, -0.45f));
        Gizmos.DrawWireSphere(new Vector3(0.65f, 0.72f, 0f), 0.48f);
        Gizmos.DrawWireSphere(new Vector3(-0.55f, 0.72f, 0f), 0.48f);
    }

    private static void DrawFence()
    {
        Vector3 leftBottom = new Vector3(-2.4f, 0f, 0f);
        Vector3 leftTop = new Vector3(-2.4f, 2f, 0f);
        Vector3 rightBottom = new Vector3(2.4f, 0f, 1.3f);
        Vector3 rightTop = new Vector3(2.4f, 2f, 1.3f);
        Gizmos.DrawLine(leftBottom, leftTop);
        Gizmos.DrawLine(rightBottom, rightTop);
        Gizmos.DrawLine(leftBottom, rightBottom);
        Gizmos.DrawLine(leftTop, rightTop);
        for (int i = 1; i < 6; i++)
        {
            float t = i / 6f;
            Vector3 bottom = Vector3.Lerp(leftBottom, rightBottom, t);
            Gizmos.DrawLine(bottom, bottom + Vector3.up * 2f);
        }
    }

    private static void DrawBox(Vector3 center, Vector3 size) =>
        Gizmos.DrawWireCube(center, size);
}
