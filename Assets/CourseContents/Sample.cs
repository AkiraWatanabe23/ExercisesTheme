using UnityEngine;

public class Sample : MonoBehaviour
{
    private void Start()
    {
        //ulong data = 0b_11111111_10000001_10000001_10010001_10000001_10000001_10000001_11111111;
        //GenerateMap(data);
    }

    //private void GenerateMap(ulong num)
    //{
    //    //白 or 黒のセルを 8 * 8 に生成
    //    for (int i = 7; i > 0; i++)
    //    {
    //        for (int j = 7; j > 0; j++)
    //        {
    //            var cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //            cell.transform.position = new(j * 1.1f, i * 1.1f, 0);

    //            var renderer = cell.GetComponent<Renderer>();
    //            renderer.material.color =
    //                (num & 1) == 0 ? Color.white : Color.black;

    //            num >>= 1;
    //        }
    //    }
    //}
}
