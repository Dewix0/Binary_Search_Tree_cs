using System;
using System.Diagnostics;

public class BinarySearchTree
{
    // Узел дерева
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    // Вставка элемента
    public void Insert(int value)
    {
        root = InsertRec(root, value);
    }

    private Node InsertRec(Node root, int value)
    {
        if (root == null)
        {
            root = new Node(value);
            return root;
        }

        if (value < root.Value)
        {
            root.Left = InsertRec(root.Left, value);
        }
        else if (value > root.Value)
        {
            root.Right = InsertRec(root.Right, value);
        }

        return root;
    }

    // Поиск элемента
    public bool Search(int value)
    {
        return SearchRec(root, value);
    }

    private bool SearchRec(Node root, int value)
    {
        if (root == null)
        {
            return false;
        }

        if (value == root.Value)
        {
            return true;
        }

        if (value < root.Value)
        {
            return SearchRec(root.Left, value);
        }
        else
        {
            return SearchRec(root.Right, value);
        }
    }

    // Удаление элемента
    public void Delete(int value)
    {
        root = DeleteRec(root, value);
    }

    private Node DeleteRec(Node root, int value)
    {
        if (root == null)
        {
            return root;
        }

        if (value < root.Value)
        {
            root.Left = DeleteRec(root.Left, value);
        }
        else if (value > root.Value)
        {
            root.Right = DeleteRec(root.Right, value);
        }
        else
        {
            // Узел с одним или нулевым потомком
            if (root.Left == null)
            {
                return root.Right;
            }
            else if (root.Right == null)
            {
                return root.Left;
            }

            // Узел с двумя потомками
            root.Value = MinValue(root.Right);
            root.Right = DeleteRec(root.Right, root.Value);
        }

        return root;
    }

    private int MinValue(Node root)
    {
        int minValue = root.Value;
        while (root.Left != null)
        {
            minValue = root.Left.Value;
            root = root.Left;
        }
        return minValue;
    }

    // Для обхода дерева (например, In-Order обход)
    public void InOrderTraversal()
    {
        InOrderRec(root);
    }

    private void InOrderRec(Node root)
    {
        if (root != null)
        {
            InOrderRec(root.Left);
            Console.Write(root.Value + " ");
            InOrderRec(root.Right);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BinarySearchTree bst = new BinarySearchTree();

        // Тест 1: Вставка элементов
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            bst.Insert(i);
        }
        stopwatch.Stop();
        Console.WriteLine($"Время вставки 1000 элементов: {stopwatch.ElapsedMilliseconds} мс");

        // Тест 2: Поиск элемента
        stopwatch.Restart();
        bool found = bst.Search(500); // Ищем элемент
        stopwatch.Stop();
        Console.WriteLine($"Время поиска элемента 500: {stopwatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"Элемент найден: {found}");

        // Тест 3: Удаление элемента
        stopwatch.Restart();
        bst.Delete(500); // Удаляем элемент
        stopwatch.Stop();
        Console.WriteLine($"Время удаления элемента 500: {stopwatch.ElapsedMilliseconds} мс");

        // Тест 4: Вставка и поиск большого количества элементов
        stopwatch.Restart();
        for (int i = 1000; i < 5000; i++)
        {
            bst.Insert(i);
        }
        stopwatch.Stop();
        Console.WriteLine($"Время вставки еще 4000 элементов: {stopwatch.ElapsedMilliseconds} мс");

        stopwatch.Restart();
        found = bst.Search(3500); // Ищем элемент
        stopwatch.Stop();
        Console.WriteLine($"Время поиска элемента 3500: {stopwatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"Элемент найден: {found}");

        // Вывод всех элементов дерева
        Console.WriteLine("Элементы дерева в порядке обхода In-Order:");
        bst.InOrderTraversal();
    }
}