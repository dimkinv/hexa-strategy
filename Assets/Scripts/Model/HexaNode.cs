using UnityEngine;

namespace Model
{
    public class HexaNode
    {
        public int Column { get; private set; }
        public int Row { get; private set; }
        public Vector3 Position { get; private set; }
        
        private const float Height = 2f;
        private readonly float _width;

        public HexaNode(int column, int row)
        {
            this.Column = column;
            this.Row = row;
            
            _width = Mathf.Sqrt(3) / 2 * HexaNode.Height;       
            this.Position = new Vector3(column * width, 0, row * height * 3 / 4)
        }

        public void GetPosition()
        {
                
        }
        
    }
}