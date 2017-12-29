using System.Runtime.CompilerServices;
using UnityEngine;

namespace Model
{
    public class HexNode
    {
        private int Column { get; set; }
        private int Row { get; set; }
        public GameObject HexGO { get; set; }
       
        public Vector3 Position { get; private set; }

        public static readonly float Width;
        public static readonly float Height;

        private static readonly float HeightSpacing;

        static HexNode()
        {
            HexNode.Height = 2f;
            Width = Mathf.Sqrt(3) / 2 * HexNode.Height;
            HeightSpacing = HexNode.Height * 0.75f;
        }

        public HexNode(int column, int row)
        {
            this.Column = column;
            this.Row = row;

            this.Position = row % 2 == 0
                ? new Vector3(column * HexNode.Width, 0, row * HexNode.HeightSpacing)
                : new Vector3(column * HexNode.Width + HexNode.Width / 2, 0, row * HexNode.HeightSpacing);
        }

        public Vector3 GetPosistionFromCamera(Vector3 cameraPosition, float mapWidth, int mapRows)
        {
            var position = this.Position;

            var widthDistanceFromCamera = (this.Position.x - cameraPosition.x) / mapWidth;

            if (widthDistanceFromCamera > 0)
                widthDistanceFromCamera += 0.5f;
            else
                widthDistanceFromCamera -= 0.5f;

            var moveDistance = (int) widthDistanceFromCamera;

            position.x -= moveDistance * mapWidth;

            return position;
        }
    }
}