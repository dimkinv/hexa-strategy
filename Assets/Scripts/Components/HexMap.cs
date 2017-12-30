using Handlers;
using Model;
using UnityEngine;

namespace Components
{
    public class HexMap : MonoBehaviour
    {
        private readonly HexNode[,] _nodes;
        private readonly float _mapWidth;
        private const int MapColumns = 30;
        private const int MapRows = 15;
        private const bool Debug = false;

        public HexMap()
        {
            this._nodes = new HexNode[HexMap.MapColumns, HexMap.MapRows];
            this._mapWidth = HexMap.MapColumns * HexNode.Width;
        }

        void Start()
        {
            BuildMap();
            HandleCameraMovement();
        }

        private void BuildMap()
        {
            var hexGenerator = new HexGenerator();
            var prefabAndMaterial = hexGenerator.GetMeshAndMaterialForTile(HexGenerator.MaterialType.Water,
                HexGenerator.PrefabType.Ocean);

            for (var column = 0; column < HexMap.MapColumns; column++)
            {
                for (var row = 0; row < HexMap.MapRows; row++)
                {
                    var hexNode = new HexNode(column, row);
                    var hexGO = Instantiate(prefabAndMaterial.Prefab, hexNode.Position, Quaternion.identity,
                        this.transform);
                    var hexMeshRenderer = hexGO.GetComponentInChildren<MeshRenderer>();
                    hexMeshRenderer.material = prefabAndMaterial.Material;

                    if (HexMap.Debug)
                    {
                        var debugTextMesh = hexGO.GetComponentInChildren<TextMesh>();
                        debugTextMesh.text = string.Format("{0},{1}", column, row);
                    }

                    this._nodes[column, row] = hexNode;
                    hexNode.HexGO = hexGO;
                }
            }
        }

        private void HandleCameraMovement()
        {
            var cameraHandler = Camera.main.GetComponentInChildren<CameraHandler>();
            cameraHandler.OnCameraMoved += (sender, args) =>
            {
                for (var column = 0; column < HexMap.MapColumns; column++)
                {
                    for (var row = 0; row < HexMap.MapRows; row++)
                    {
                        var hexNode = this._nodes[column, row];
                        hexNode.UpdatePositionForCamera(args.NewPosition, this._mapWidth, HexMap.MapRows);
                    }
                }
            };
        }
    }
}