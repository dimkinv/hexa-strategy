using UnityEngine;

namespace HexaStrategy.Assets.HexaMap
{
    public class HexaMap : MonoBehaviour
    {
        public GameObject hexPrefab;
        readonly float height = 2f;
        readonly float width;

        public HexaMap()
        {
            this.width = Mathf.Sqrt(3) / 2 * this.height;
        }
        void Start()
        {
            for (int column = 0; column < 10; column++)
            {
                for (int row = 0; row < 10; row++)
                {
                    if (row % 2 == 0)
                    {
                        Instantiate(this.hexPrefab, new Vector3(column * width, 0, row * height * 3 / 4), Quaternion.identity, this.transform);

                    }
                    else
                    {
                        Instantiate(this.hexPrefab, new Vector3(column * width + width / 2, 0, row * height * 3 / 4), Quaternion.identity, this.transform);
                    }
                }
            }
        }
    }
}