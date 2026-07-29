using UnityEngine;

public class SerializableVector2
{
    public SerializableVector2()
    {
    }

    public SerializableVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public float x { get; set; }

    public float y { get; set; }

    public static implicit operator SerializableVector2(Vector2 vector) =>
        new SerializableVector2(vector.x, vector.y);

    public static implicit operator Vector2(SerializableVector2 vector) =>
        new Vector2(vector.x, vector.y);
}
