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
    
    public (bool, List<int>) SearchWithPath(int value)
    {
        List<int> path = new List<int>();
        bool found = SearchWithPathRec(root, value, path);
        return (found, path);
    }

    private bool SearchWithPathRec(Node root, int value, List<int> path)
    {
        if (root == null)
        {
            return false;
        }

        path.Add(root.Value); // Добавляем текущий узел в маршрут

        if (value == root.Value)
        {
            return true;
        }

        if (value < root.Value)
        {
            return SearchWithPathRec(root.Left, value, path);
        }
        else
        {
            return SearchWithPathRec(root.Right, value, path);
        }
    }
    
    public void PrintTree() //Метод графического представления дерева до сортировки
    {
        PrintTreeRec(root, "", true);
    }

    private void PrintTreeRec(Node node, string indent, bool isRight)
    {
        if (node != null)
        {
            Console.WriteLine(indent + (isRight ? "└── " : "├── ") + node.Value);
            PrintTreeRec(node.Left, indent + (isRight ? "    " : "│   "), false);
            PrintTreeRec(node.Right, indent + (isRight ? "    " : "│   "), true);
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
        Console.WriteLine("");
        
        //Рандомное дерево
        
        BinarySearchTree bst2 = new BinarySearchTree();
        Random random = new Random();
        
        int numberOfNodes = 10; // Количество узлов в дереве
        int[] randomValues = new int[numberOfNodes];
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("Генерация случайного дерева...");
        for (int i = 0; i < numberOfNodes; i++)
        {
            int randomValue = random.Next(1, 10000); // Генерация случайного числа в диапазоне [1, 10000]
            randomValues[i] = randomValue;
            bst2.Insert(randomValue);
        }
        Console.WriteLine("");
        Console.WriteLine("Графическое представления дерева до сортировки");
        bst2.PrintTree();
        // Выбор случайного значения для поиска
        int targetValue = randomValues[random.Next(numberOfNodes)];
        Console.WriteLine($"Случайное значение для поиска: {targetValue}");

        // Поиск элемента
        Stopwatch stopwatch2 = Stopwatch.StartNew();
        var (found2, path) = bst.SearchWithPath(targetValue);
        stopwatch2.Stop();

        // Результат поиска
        Console.WriteLine($"Элемент найден: {found}");
        Console.WriteLine($"Маршрут до элемента: {string.Join(" -> ", path)}");
        Console.WriteLine($"Время поиска элемента: {stopwatch.ElapsedMilliseconds} мс");

        // Вывод элементов дерева (необязательно, если дерево большое)
        Console.WriteLine("\nЭлементы дерева (In-Order Traversal):");
        bst2.InOrderTraversal();
        Console.WriteLine();
    }
}