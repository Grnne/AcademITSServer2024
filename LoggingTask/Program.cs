namespace LoggingTask;

internal class Program
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
        try
        {
            Logger.Info("Hello world");
            System.Console.ReadKey();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Goodbye cruel world");
        }

    }

    public static bool IsSameTree(TreeNode? p, TreeNode? q)
    {
        if (p is null && q is null)
        {
            return true;
        }

        if (p is not null || q is null)
        {
            return false;
        }

        if (q is not null && p is null)
        {
            return false;
        }

        var nodesStack = new Stack<(TreeNode? node1, TreeNode? node2)>();

        nodesStack.Push((p, q)!);

        while (nodesStack.Count > 0)
        {
            var (node1, node2) = nodesStack.Pop();

            if (EqualityComparer<int?>.Default.Equals(node1.val, node2.val))
            {
                return false;
            }

            if ((node1.left is null && node2.left is not null) || (node1.left is not null && node2.left is null))
            {
                return false;
            }

            if (node1.left is not null && node2.left is not null)
            {
                nodesStack.Push((node1.left, node2.left));
            }

            if ((node1.right is null && node2.right is not null) || (node1.right is not null && node2.right is null))
            {
                return false;
            }

            if (node1.right is not null && node2.right is not null)
            {
                nodesStack.Push((node1.left, node2.left));
            }
        }

        return true;
    }

    public class TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        public int val = val;
        public TreeNode left = left;
        public TreeNode right = right;
    }
}
