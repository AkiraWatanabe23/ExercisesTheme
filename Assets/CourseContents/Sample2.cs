using UnityEngine;
using UnityEngine.UI;

public class Sample2 : MonoBehaviour
{
    [Tooltip("オブジェクト数")]
    [SerializeField] private int _cellCount = 0;

    private Image[] _images = default;
    private int _index = 0;

    private void Start()
    {
        _images = new Image[_cellCount];

        for (var i = 0; i < _cellCount; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            var image = obj.AddComponent<Image>();
            image.color = i == 0 ? Color.red : Color.white;

            _images[i] = obj.GetComponent<Image>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            if (_index != 0)
            {
                SelectCell(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            if (_index != _cellCount - 1)
            {
                SelectCell(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //オブジェクトを破壊
            Destroy(_images[_index].gameObject);

            //配列をつめる
            for (int i = _index; i < _cellCount - 1; i++)
            {
                _images[i] = _images[i + 1];
            }
            //カウントを減らす
            _cellCount--;

            SelectCell(false);
        }
    }

    private void SelectCell(bool dir)
    {
        if (_cellCount == 0) return;

        _images[_index].color = Color.white;
        _index = dir ? _index - 1 : _index + 1;

        _images[_index].color = Color.red;
    }
}
