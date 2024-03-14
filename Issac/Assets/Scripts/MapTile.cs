using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour
{
    [SerializeField] Vector2 _size;


    public enum TileType {
        Root, Normal, Hub, Reward, Boss, Shop
    }


    public Vector2 Size { get { return _size; } }

    [SerializeField] GameObject _upJoint;
    [SerializeField] GameObject _downJoint;
    [SerializeField] GameObject _rightJoint;
    [SerializeField] GameObject _leftJoint;

    [SerializeField] SpriteRenderer spr;
    [SerializeField] Text _txt;

    // Start is called before the first frame update
    public void Set(Vector2 pos, TileType type)
    {
        transform.position = pos;
        name = type.ToString();

        _txt.text = gameObject.name;
        switch (gameObject.name)
        {

            case "Root":
                spr.color = Color.red;
                break;
            case "Normal":
                spr.color = Color.green;
                break;
            case "Hub":
                spr.color = Color.blue;
                break;
            case "Reward":
                spr.color = Color.yellow;
                break;
            case "Boss":
                spr.color = Color.cyan;
                break;
            default:
                spr.color = Color.gray;
                break;

        }
    }

    public void JointAt(Vector3 pos) {
        if (pos.x < transform.position.x) {
            _leftJoint.SetActive(false);
            return;
        }
        if (pos.x > transform.position.x)
        {
            _rightJoint.SetActive(false);
            return;
        }
        if (pos.y < transform.position.y)
        {
            _downJoint.SetActive(false);
            return;
        }
        _upJoint.SetActive(false);
    }

}
