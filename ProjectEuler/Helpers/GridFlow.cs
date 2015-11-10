using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{

    class SimpleGridFlowNode
    {

        private int _id;
        private int? _idAcross;
        private int? _idDown;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? IdAcross
        {
            get { return _idAcross; }
            set { _idAcross = value; }
        }

        public int? IdDown
        {
            get { return _idDown; }
            set { _idDown = value; }
        }

        public SimpleGridFlowNode(int id)
        {
            _id = id;
        }

        public override string ToString()
        {
            if (IdAcross != null && IdDown != null)
                return string.Format("({0}) across [{1}]  down[{2}]  ", ID, IdAcross, IdDown);
            else if (IdAcross != null)
                return string.Format("({0}) across [{1}]  ", ID, IdAcross);
            else if (IdDown != null)
                return string.Format("({0}) down [{1}]  ", ID, IdDown);
            else
                return string.Format("({0})", ID);
        }
    }


    class SimpleGridPath : List<SimpleGridFlowNode>
    {
        public SimpleGridPath Clone()
        {
            SimpleGridPath cloned = new SimpleGridPath();
            foreach (SimpleGridFlowNode node in this)
            {
                cloned.Add(node);
            }
            return cloned;
        }
        public override string ToString()
        {
            //return string.Format("Contains {0} paths.", Count);
            StringBuilder sb = new StringBuilder();
            foreach (SimpleGridFlowNode node in this)
            {
                sb.AppendFormat("{0,3}", node.ID);
            }
            return sb.ToString();
        }
    }



    class GridFlowNode
    {

        private bool _accessible;
        private int _x;
        private int _y;
        private int _id;
        private int? _idAcross;
        private int? _idDown;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public bool Accessible
        {
            get { return _accessible; }
            set { _accessible = value; }
        }


        public int? IdAcross
        {
          get { return _idAcross; }
          set { _idAcross = value; }
        }

        public int? IdDown
        {
          get { return _idDown; }
          set { _idDown = value; }
        }


        public GridFlowNode()
        {
        }
        public GridFlowNode(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public GridFlowNode( int x, int y, int id)
        {
            _x = x;
            _y = y;
            _id = id;
        }

        public GridFlowNode Clone()
        {
            return new GridFlowNode( _x, _y);
        }


        public override string ToString()
        {
            if (IdAcross != null && IdDown != null)
                return string.Format("  ( {0} , {1} )   across [{2}]  down[{3}]  ", X, Y, IdAcross, IdDown);
            else if (IdAcross != null)
                return string.Format("  ( {0} , {1} )   across [{2}]  ", X, Y, IdAcross);
            else if (IdDown != null)
                return string.Format("  ( {0} , {1} )   down [{2}]  ", X, Y, IdDown);
            else
                return string.Format("  ( {0} , {1} )  ", X, Y);
            
        }


    }

    class GridPath : List<GridFlowNode>
    {
        public GridPath Clone()
        {
            GridPath cloned = new GridPath();
            foreach (GridFlowNode node in this)
            {
                cloned.Add( node);
            }
            return cloned;
        }
        public override string ToString()
        {
            //return string.Format("Contains {0} paths.", Count);
            StringBuilder sb = new StringBuilder( );
            foreach (GridFlowNode node in this)
            {
                sb.AppendFormat("{0,3}", node.ID);
            }
            return sb.ToString();
        }
    }


    class ValueGridFlowNode
    {

        private int _id;
        private int? _idAcross;
        private int? _idDown;
        private int _value;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? IdAcross
        {
            get { return _idAcross; }
            set { _idAcross = value; }
        }

        public int? IdDown
        {
            get { return _idDown; }
            set { _idDown = value; }
        }
        

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public ValueGridFlowNode(int id, int value)
        {
            _id = id;
            _value=value;
        }

        public override string ToString()
        {
            if (IdAcross != null && IdDown != null)
                return string.Format("({0,3}:{1}) across [{2}]  down[{2}]  ", ID, Value, IdAcross, IdDown);
            else if (IdAcross != null)
                return string.Format("({0,3}:{1}) across [{2}]  ", ID, Value, IdAcross);
            else if (IdDown != null)
                return string.Format("({0,3}:{1}) down [{2}]  ", ID, Value, IdDown);
            else
                return string.Format("({0,3}:{1})", ID, Value);
        }
    }


    class ValueGridPath : List<ValueGridFlowNode>
    {

        public int TotalValue = 0;
        public ValueGridPath Clone()
        {
            ValueGridPath cloned = new ValueGridPath();
            cloned.TotalValue = this.TotalValue;
            foreach (ValueGridFlowNode node in this)
            {
                cloned.Add(node);
            }
            return cloned;
        }
        public override string ToString()
        {
            //return string.Format("Contains {0} paths.", Count);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Value {0} | ", TotalValue);
            foreach (ValueGridFlowNode node in this)
            {
                sb.AppendFormat(" {0}", node.Value);
            }
            return sb.ToString();
        }
    }


}
