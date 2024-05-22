using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour, IJoint
{
    // Start is called before the first frame update
    public void Set(IRoom tile1, IRoom tile2) {

        Vector2 startPos = tile1.GetPosition();
        Vector2 endPos = tile2.GetPosition();

        transform.position = (startPos + endPos) / 2;
        float value;

        if (startPos.x == endPos.x)
        {
            transform.eulerAngles = Vector3.forward * 90f;
            value = Mathf.Abs(endPos.y - startPos.y) - (tile1.Size.y + tile2.Size.y) * 0.5f;
        }
        else
        {
            value = Mathf.Abs(endPos.x - startPos.x) - (tile1.Size.x + tile2.Size.x) * 0.5f;
        }

        transform.localScale = new Vector3(value + 1, 1, 1);

        tile1.JointAt(transform.position);
        tile2.JointAt(transform.position);
    }
}
