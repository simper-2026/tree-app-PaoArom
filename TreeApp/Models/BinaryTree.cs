public class BinaryTree
{
    private Node? _root;
    private int _edgeIndex;

    public void Insert(int value)
    {
        _root = InsertRecursive(_root, value, null);
    }

    private Node InsertRecursive(Node? node, int value, Node? parent = null)
    {
        if (node == null)
        {
            Node newNode = new Node(value);
            newNode.Parent = parent;
            return newNode;
        }

        if (value < node.Value)
        {
            node.Left = InsertRecursive(node.Left, value, node);
        }

        else if (value > node.Value)
        {
            node.Right = InsertRecursive(node.Right, value, node);
        }

        UpdateHeight(node);

        int balance = GetBalance(node);
        if(balance > 1 && value < node.Left!.Value)
        {
            return RotateRight(node);
        }

        if(balance < -1 && value > node.Right!.Value)
        {
            return RotateLeft(node);
        }

        if(balance > 1 && value > node.Left!.Value)
        {
            node.Left = RotateLeft(node.Left!);
            return RotateRight(node);
        }

        if(balance < -1 && value < node.Right!.Value)
        {
            node.Right = RotateRight(node.Right!);
            return RotateLeft(node);
        }
        return node;
    }

    public string InOrder()
    {
        return InOrderRecursive(_root);
    }

    private string InOrderRecursive(Node? node)
    {
        if (node == null)
        {
            return "";
        }

        string left = InOrderRecursive(node.Left);
        string current = node.Value.ToString();
        string right = InOrderRecursive(node.Right);

        return $"{left} {current} {right}".Trim();
    }

    public int Height()
    {
        return HeightRecursive(_root);
    }

    private int HeightRecursive(Node? node)
    {
        if (node == null)
        {
            return -1;
        }

        int leftHeigh = HeightRecursive(node.Left);
        int rightHeight = HeightRecursive(node.Right);

        return Math.Max(leftHeigh, rightHeight) + 1;
    }
    public string ToMermaid()
    {
        _edgeIndex = 0;

        if (_root == null)
        {
            return "graph TD\n    empty[\"(empty tree)\"]";
        }

        if (_root.Left == null && _root.Right == null)
        {
            return "graph TD\n" + $"{_root.Value}[ {_root.Value} h:{_root.Height} ]";
        }

        return "graph TD\n" + MermaidRecursive(_root);
    }

    private string MermaidRecursive(Node? node)
    {
        if (node == null)
            return "";

        if (node.Left == null && node.Right == null)
        {
            return "";
        }

        string result = "";

        if (node.Left != null)
        {
            result += $"{node.Value}[ {node.Value} h:{node.Height} ] --> {node.Left.Value}[ {node.Left.Value} h:{node.Left.Height} ]\n";
            _edgeIndex++;
            result += MermaidRecursive(node.Left);
        }
        else
        {
            string phantomMode = $"_ph{_edgeIndex}";
            result += $"{node.Value}[ {node.Value} h:{node.Height} ] --> {phantomMode}[ ]\n";
            result += $"linkStyle {_edgeIndex} stroke:none,stroke-width:0,fill:none\n";
            result += $"style {phantomMode} fill:none,stroke:none,color:none\n";
            _edgeIndex++;
        }

        if (node.Right != null)
        {
            result += $"{node.Value}[ {node.Value} h:{node.Height} ] --> {node.Right.Value}[ {node.Right.Value} h:{node.Right.Height} ]\n";
            _edgeIndex++;
            result += MermaidRecursive(node.Right);
        }
        else
        {
            string phantomMode = $"_ph{_edgeIndex}";
            result += $"{node.Value}[ {node.Value} h:{node.Height} ] --> {phantomMode}[ ]\n";
            result += $"linkStyle {_edgeIndex} stroke:none,stroke-width:0,fill:none\n";
            result += $"style {phantomMode} fill:none,stroke:none,color:none\n";
            _edgeIndex++;
        }

        return result;
    }

    private Node RotateRight(Node z)
    {
        Node y = z.Left!;
        Node? t3 = y.Right;   // T3 moves from y's right to z's left
        y.Right = z;
        z.Left = t3;

        y.Parent = z.Parent;
        z.Parent = y;
        if (t3 != null)
        {
            t3.Parent = z;
        }

        UpdateHeight(z);
        UpdateHeight(y);

        return y;                // y is the new root of this subtree 
    }

    private Node RotateLeft(Node z)
    {
        Node y = z.Right!;
        Node? t2 = y.Left;
        y.Left = z;
        z.Right = t2;

        y.Parent = z.Parent;
        z.Parent = y;

        if(t2 != null)
        {
            t2.Parent = z;
        }

        UpdateHeight(z);
        UpdateHeight(y);
        return y;
    }

    private int GetHeight(Node? node)
    {
        if (node == null)
        {
            return -1;
        }

        return node.Height;
    }
    private void UpdateHeight(Node node)
    {
        int leftHeight = GetHeight(node.Left);
        int rightHeight = GetHeight(node.Right);
        node.Height = 1 + Math.Max(leftHeight, rightHeight);
    }

    private int GetBalance(Node? node)
    {
        if (node == null)
        {
            return 0;
        }

        return GetHeight(node.Left) - GetHeight(node.Right);
    }
}