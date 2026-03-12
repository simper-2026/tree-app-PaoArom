public class BinaryTree
{
    private Node? _root;
    private int _edgeIndex = 0;

    public void Insert(int value)
    {
        _root = InsertRecursive(_root, value);
    }

    private Node InsertRecursive(Node? node, int value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        if (value < node.Value)
        {
            node.Left = InsertRecursive(node.Left, value);
        }

        else if (value > node.Value)
        {
            node.Right = InsertRecursive(node.Right, value);
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

        return 1 + Math.Max(leftHeigh, rightHeight);
    }
    public string ToMermaid()
    {
        if (_root == null)
        {
            return "graph TD\n    empty[\"(empty tree)\"]";
        }

        if (_root.Left == null && _root.Right == null)
        {
            return "graph TD\n    " + _root.Value;
        }

        _edgeIndex = 0;
        return "graph TD\n" + MermaidRecursive(_root);
    }

    private string MermaidRecursive(Node? node)
    {
        if (node == null)
            return "";

        string result = "";

        if(node.Left != null && node.Right != null)
        {
            result += "    " + node.Value + " --> " + node.Left.Value + "\n";
            _edgeIndex++;
            result += "    " + node.Value + " --> " + node.Right.Value + "\n";
            _edgeIndex++;
            result += MermaidRecursive(node.Left);
            result += MermaidRecursive(node.Right);
        }

        else if (node.Left != null)
        {
            result += "    " + node.Value + " --> " + node.Left.Value + "\n";
            _edgeIndex++;
            string phantomMode = "_ph" + _edgeIndex;
            result += "    " + node.Value + " --> " + phantomMode + "[ ]\n";
            result += "    linkStyle " + _edgeIndex + " stroke:none,stroke-width:0,fill:none\n";
            result += "    style " + phantomMode + " fill:none,stroke:none,color:none\n";
            _edgeIndex++;
            result += MermaidRecursive(node.Left);
        }

        else if (node.Right != null)
        {
            string phantomMode = "_ph" + _edgeIndex;
            result += "    " + node.Value + " --> " + phantomMode + "[ ]\n";
            result += "    linkStyle " + _edgeIndex + " stroke:none,stroke-width:0,fill:none\n";
            result += "    style " + phantomMode + " fill:none,stroke:none,color:none\n";
            _edgeIndex++;
            result += "    " + node.Value + " --> " + node.Right.Value + "\n";
            _edgeIndex++;
            result += MermaidRecursive(node.Right);
        }

        return result;
    }
}