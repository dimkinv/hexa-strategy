using Handlers;
using Model;
using UnityEngine;

namespace HexaMap
{
    public class HexaMap : MonoBehaviour
    {
        public GameObject HexPrefab;
        public Material[] TerrainMaterials;
        
        private readonly HexNode[,] _nodes;
        private readonly float _mapWidth;
        private const int Columns = 60;
        private const int Rows = 20;

        public HexaMap()
        {
            this._nodes = new HexNode[HexaMap.Columns, HexaMap.Rows];
            this._mapWidth = HexaMap.Columns * HexNode.Width;
        }

        private void Start()
        {
            for (var column = 0; column < HexaMap.Columns; column++)
            {
                for (var row = 0; row < HexaMap.Rows; row++)
                {
                    var hexNode = new HexNode(column, row);
                    var hexGO = Instantiate(this.HexPrefab, hexNode.Position, Quaternion.identity, this.transform);
                    var hexMeshRenderer = hexGO.GetComponentInChildren<MeshRenderer>();
                    hexMeshRenderer.material = this.TerrainMaterials[Random.Range(0, this.TerrainMaterials.Length)];

                    this._nodes[column, row] = hexNode;
                    hexNode.HexGO = hexGO;
                }
            }

            HandleCameraMovement();
        }

        private void HandleCameraMovement()
        {
            var cameraHandler = Camera.main.GetComponentInChildren<CameraHandler>();
            cameraHandler.OnCameraMoved += (sender, args) =>
            {
                for (var column = 0; column < HexaMap.Columns; column++)
                {
                    for (var row = 0; row < HexaMap.Rows; row++)
                    {
                        var hexNode = this._nodes[column, row];
                        var hexNodeGO = hexNode.HexGO;

                        var newPosition =
                            hexNode.GetPosistionFromCamera(args.NewPosition, this._mapWidth, HexaMap.Rows);

                        hexNodeGO.transform.position = newPosition;
                    }
                }
            };
        }
    }
}