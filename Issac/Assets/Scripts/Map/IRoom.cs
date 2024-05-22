using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoom {

    public Vector2 Size { get; }

    public enum RoomType {
        Root, Normal, Hub, Reward, Boss, Shop
    }

    public void Set(Vector2 pos);

    public void JointAt(Vector3 pos);

    public Vector3 GetPosition();

}
