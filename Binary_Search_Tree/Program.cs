using System;

class TreeNode
{
    public int Key;
    public TreeNode Left;
    public TreeNode Right;

    public TreeNode(int key)
    {
        Key = key;
        Left = null;
        Right = null;
    }
}

class BinarySearchTree
{
    private TreeNode root;

    public BinarySearchTree()
    {
        root = null;
    }

    // Вставка нового узла
    public void Insert(int key)
    {
        root = InsertRecursive(root, key);
    }

    private TreeNode InsertRecursive(TreeNode node, int key)
    {
        if (node == null)
        {
            return new TreeNode(key);
        }
        if (key < node.Key)
        {
            node.Left = InsertRecursive(node.Left, key);
        }
        else
        {
            node.Right = InsertRecursive(node.Right, key);
        }
        return node;
    }

    // Поиск узла
    public bool Search(int key)
    {
        return SearchRecursive(root, key) != null;
    }

    private TreeNode SearchRecursive(TreeNode node, int key)
    {
        if (node == null || node.Key == key)
        {
            return node;
        }
        if (key < node.Key)
        {
            return SearchRecursive(node.Left, key);
        }
        return SearchRecursive(node.Right, key);
    }

    // Удаление узла
    public void Delete(int key)
    {
        root = DeleteRecursive(root, key);
    }

    private TreeNode DeleteRecursive(TreeNode node, int key)
    {
        if (node == null)
        {
            return null;
        }

        if (key < node.Key)
        {
            node.Left = DeleteRecursive(node.Left, key);
        }
        else if (key > node.Key)
        {
            node.Right = DeleteRecursive(node.Right, key);
        }
        else
        {
            // Узел найден
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            // Узел с двумя потомками
            node.Key = FindMin(node.Right).Key;
            node.Right = DeleteRecursive(node.Right, node.Key);
        }
        return node;
    }

    private TreeNode FindMin(TreeNode node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    // Обход дерева (In-Order Traversal)
    public void InOrderTraversal()
    {
        InOrderTraversalRecursive(root);
        Console.WriteLine();
    }

    private void InOrderTraversalRecursive(TreeNode node)
    {
        if (node != null)
        {
            InOrderTraversalRecursive(node.Left);
            Console.Write(node.Key + " ");
            InOrderTraversalRecursive(node.Right);
        }
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Начало проверки работы дерева поиска");
        Console.WriteLine(" ");
        
        Console.WriteLine("Вставим в дерево значения: 50,30,70,20,40,60,80");
        Console.WriteLine(" ");
        
        BinarySearchTree bst = new BinarySearchTree();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);
        bst.Insert(60);
        bst.Insert(80);
        
        
        
        Console.WriteLine("Выполним метод InOrderTraversal , чтобы элементы были по порядку");
        Console.WriteLine(" ");
        
        Console.WriteLine("In-Order Traversal:");
        bst.InOrderTraversal();
        
        Console.WriteLine(" ");
        Console.WriteLine("Найдем элемент со значением 40");

        Console.WriteLine("Поиск 40: " + (bst.Search(40) ? "найден" : "не найден"));
        Console.WriteLine(" ");
        Console.WriteLine("Теперь удалим элемент дерева со значением 40, чтобы проверить работу метода Delete и выполним поиск с по-сути новым дереавом");
        Console.WriteLine(" ");
        bst.Delete(40);
        Console.WriteLine("Поиск 40: " + (bst.Search(40) ? "найден" : "не найден"));
        
        Console.WriteLine(" ");
        Console.WriteLine("In-Order Traversal после удаления:");
        bst.InOrderTraversal();
    }
}