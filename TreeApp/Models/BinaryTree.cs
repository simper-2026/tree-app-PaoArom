public class BinaryTree
{
    private Node? _root;

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
   
}