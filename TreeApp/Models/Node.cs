public class Node
{
    private int _value;
    public Node? Left {get; set;}
    public Node? Right {get; set;}
    public int Value
    {
        get{return _value;}
    }

    public Node(int value, Node? left = null, Node? right = null)
    {
        _value = value;
        Left = left;
        Right = right;
    }
}