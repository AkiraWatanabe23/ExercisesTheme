using UnityEngine;

public class ClickInput : MonoBehaviour
{
    [SerializeField] private Material[] _materials = new Material[2];

    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        //TODO：選んだマスを反映
    }

    private void OnMouseEnter()
    {
        _renderer.material = _materials[0];
    }

    private void OnMouseExit()
    {
        _renderer.material = _materials[1];
    }
}
