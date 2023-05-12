using UnityEngine;

public class Sample : MonoBehaviour
{
    private void Start()
    {
        //リテラル...コード内に埋め込むことができるデータ
        #region リテラル一覧
        //Debug.Log(1234); //整数リテラル
        //Debug.Log(3.14); //浮動小数点数リテラル
        //Debug.Log(true); //ブーリアンリテラル（true, false）
        //Debug.Log('a');  //文字リテラル
        //Debug.Log("Hello World"); //文字列リテラル
        //Debug.Log(null); //nullリテラル（参照なし）
        #endregion

        ulong data = 0b_11111111_10000001_10000001_10010001_10000001_10000001_10000001_11111111;
        GenerateMap(data);
    }

    private void GenerateMap(ulong num)
    {
        //白 or 黒のセルを 8 * 8 に生成
        for (int i = 7; i > 0; i++)
        {
            for (int j = 7; j > 0; j++)
            {
                var cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.transform.position = new(j * 1.1f, i * 1.1f, 0);

                var renderer = cell.GetComponent<Renderer>();
                renderer.material.color =
                    (num & 1) == 0 ? Color.white : Color.black;

                num >>= 1;
            }
        }
    }
}
