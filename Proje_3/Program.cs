using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Proje_3.Program;

namespace Proje_3
{
    internal class Program
    {
        // Method to measure the execution time of a sorting algorithm
        public static long MeasureTime(int[] array, Action<int[]> sortAlgorithm, int iterations)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Run the sorting algorithm for the given number of iterations
            for (int i = 0; i < iterations; i++)
            {
                // Create a copy of the original array to sort
                int[] tempArray = (int[])array.Clone();
                sortAlgorithm(tempArray);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        // Wrapper for the QuickSort algorithm
        static void QuickSortWrapper(int[] arr)
        {
            QuickSort2(arr, 0, arr.Length - 1);
        }

        // Bubble sort implementation with console output for each swap
        public static void Bubble_sort(int[] arr)
        {
            int n = arr.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                Console.WriteLine("swap: " + swapped + "\n");
                print_array(arr);

                // Compare adjacent elements and swap if needed
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                        Console.WriteLine("swap: " + swapped + " " + arr[j] + " " + arr[j + 1] + "\n");
                        print_array(arr);
                    }
                }
                // If no swaps were made, break out of the loop
                if (!swapped)
                    break;
            }
        }

        // Optimized version of Bubble sort without console output
        public static void Bubble_sort2(int[] arr)
        {
            int n = arr.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                // Compare adjacent elements and swap if needed
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                // If no swaps were made, break out of the loop
                if (!swapped)
                    break;
            }
        }

        // Recursive QuickSort algorithm
        public static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);

                Console.WriteLine($"Pivot sonrası dizi (pivot: {arr[pi]}):");
                print_array(arr);

                // Recursively sort the left and right partitions
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        // Optimized recursive QuickSort algorithm (without output)
        public static void QuickSort2(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition2(arr, low, high);

                // Recursively sort the left and right partitions
                QuickSort2(arr, low, pi - 1);
                QuickSort2(arr, pi + 1, high);
            }
        }

        // Partition function for QuickSort with console output
        public static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            // Reorder the array such that all elements less than the pivot are on the left
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                    Console.WriteLine($"Swap: {arr[i]} ve {arr[j]} değiştirildi:");
                    print_array(arr);
                }
            }
            // Swap the pivot into the correct position
            Swap(arr, i + 1, high);
            Console.WriteLine($"Pivot ({pivot}) yerleştirildi:");
            print_array(arr);
            return i + 1;
        }

        // Optimized partition function for QuickSort (without output)
        public static int Partition2(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            // Reorder the array such that all elements less than the pivot are on the left
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            // Swap the pivot into the correct position
            Swap(arr, i + 1, high);
            return i + 1;
        }

        // Helper method to print an array to the console
        public static void print_array(int[] arr)
        {
            foreach (int i in arr)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        // Swap two elements in an array
        public static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        // Max heap implementation
        public class Max_heap
        {
            private int capacity = 10;
            private int size = 0;
            private EgedeniziB[] items;

            // Constructor for MaxHeap
            public Max_heap()
            {
                items = new EgedeniziB[capacity];
                capacity = 10;
                size = 0;
            }

            // Helper methods for navigating the heap structure
            private int getLeftChildIndex(int parentIndex) { return 2 * parentIndex; }
            private int getRightChildIndex(int parentIndex) { return 2 * parentIndex + 1; }
            private int getParentIndex(int Index) { return Index % 2 == 0 ? Index / 2 : (Index - 1) / 2; }
            private bool hasLeftChild(int index) { return getLeftChildIndex(index) <= size; }
            private bool hasRightChild(int index) { return getRightChildIndex(index) <= size; }
            private bool hasParent(int index) { return getParentIndex(index) > 0; }

            private EgedeniziB leftChild(int index) { return items[getLeftChildIndex(index)]; }
            private EgedeniziB rightChild(int index) { return items[getRightChildIndex(index)]; }
            private EgedeniziB parent(int index) { return items[getParentIndex(index)]; }

            // Swap two elements in the heap
            private void swap(int index1, int index2)
            {
                EgedeniziB temp = items[index1];
                items[index1] = items[index2];
                items[index2] = temp;
            }

            // Ensure the heap has enough capacity to store more elements
            private void ensureExtraCapacity()
            {
                if (size == capacity - 1)
                {
                    Array.Resize(ref items, capacity * 2);
                    capacity *= 2;
                }
            }

            // Peek the root element (max element in a max heap)
            public EgedeniziB peek()
            {
                if (size == 0) throw new InvalidOperationException();

                return items[1];
            }

            // Remove the root element (max element) from the heap
            public EgedeniziB remove()
            {
                if (size == 0) throw new InvalidOperationException();
                EgedeniziB temp = items[1];
                items[1] = items[size];
                size--;
                trickleDown();
                return temp;
            }

            // Add a new element to the heap
            public void Add(EgedeniziB item)
            {
                ensureExtraCapacity();
                size++;
                items[size] = item;
                trickleUp();
            }

            // Trickle up the heap to maintain the heap property
            public void trickleUp()
            {
                int index = size;
                while (hasParent(index))
                {
                    if (parent(index).Fish_name.CompareTo(items[index].Fish_name) < 0)
                    {
                        swap(getParentIndex(index), index);
                    }
                    index = getParentIndex(index);
                }
            }

            // Trickle down the heap to maintain the heap property
            public void trickleDown()
            {
                int index = 1;

                while (hasLeftChild(index))
                {
                    // Compare the left and right children and move the larger element up
                    if (hasRightChild(index) && rightChild(index).Fish_name.CompareTo(leftChild(index).Fish_name) < 0 && items[index].Fish_name.CompareTo(leftChild(index).Fish_name) < 0)
                    {
                        swap(index, getLeftChildIndex(index));
                        index = getLeftChildIndex(index);
                    }
                    else if (rightChild(index).Fish_name.CompareTo(items[index].Fish_name) > 0)
                    {
                        swap(index, getRightChildIndex(index));
                        index = getRightChildIndex(index);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        // Class representing fish details
        public class EgedeniziB
        {
            private string fish_name; // Main fish name
            private string fish_Other_name; // Alternative fish name
            private string size; // Fish size
            public TreeNode information; // Additional information about the fish
            private List<string> areas; // List of areas where the fish can be found

            // Constructor for initializing the fish object
            public EgedeniziB()
            {
                areas = new List<string>();
                fish_Other_name = "";
                size = "";
                information = new TreeNode();
            }

            // Print all the information about the fish
            public void print_info()
            {
                Console.WriteLine("Fish name : " + Fish_name);
                Console.WriteLine();
                Console.WriteLine("Other name : " + Fish_Other_name);
                Console.WriteLine();
                Console.WriteLine("Size : " + size);
                Console.WriteLine();
                Console.WriteLine("Information : ");
                information.inOrder(information);
                Console.WriteLine();
                Console.Write("Area's : ");
                foreach (string s in areas)
                {
                    Console.Write(s + " ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            // Property for fish name
            public string Fish_name
            {
                get { return fish_name; }
                set { fish_name = value; }
            }

            // Property for other fish name
            public string Fish_Other_name
            {
                get { return fish_Other_name; }
                set { fish_Other_name = value; }
            }

            // Property for fish size
            public string Size
            {
                get { return size; }
                set { size += value; }
            }

            // Add area to the areas list
            public void area_Add(string area)
            {
                areas.Add(area);
            }

            // Get the areas where the fish is found
            public List<string> get_areas()
            {
                return areas;
            }
        }

        // TreeNode class for storing words and their occurrences in the tree
        public class TreeNode
        {
            private string word; // The word stored in the node
            public int sayac; // Counter to store occurrences of the word
            public TreeNode rightchild; // Reference to the right child node
            public TreeNode leftchild; // Reference to the left child node

            // Constructor that initializes the node with a word
            public TreeNode(string info_word)
            {
                rightchild = null;
                leftchild = null;
                word = info_word;
                sayac = 0;
            }

            // Default constructor for creating an empty node
            public TreeNode()
            {
                rightchild = null;
                leftchild = null;
                word = "";
                sayac = 0;
            }

            // Property for the word stored in the node
            public string Word
            {
                get { return word; }
                set { word += value; }
            }

            // In-order traversal of the tree and display each word
            public void inOrder(TreeNode localRoot)
            {
                if (localRoot != null)
                {
                    inOrder(localRoot.leftchild); // Traverse left subtree
                    localRoot.display_data(); // Display the current node's data
                    inOrder(localRoot.rightchild); // Traverse right subtree
                }
            }

            // Calculates the total depth of the tree starting from the given node
            public int total_depth(TreeNode localRoot, int düzey = 0)
            {
                if (localRoot != null)
                {
                    düzey++; // Increment depth for current node
                    return düzey + total_depth(localRoot.leftchild, düzey) + total_depth(localRoot.rightchild, düzey);
                }
                return 0; // Return 0 if the node is null
            }

            // Count the total number of nodes in the tree starting from the given node
            public int Node_count(TreeNode localRoot)
            {
                if (localRoot == null)
                {
                    return 0; // Return 0 if the node is null
                }
                return 1 + Node_count(localRoot.leftchild) + Node_count(localRoot.rightchild); // Count current node and recursively count left and right subtrees
            }

            // Display the word stored in the current node
            public void display_data()
            {
                Console.Write(word + " "); // Display word
            }

            // Insert a word into the tree
            public void insert(string info_word)
            {
                if (info_word != "")
                {
                    if (word == "")
                    {
                        word = info_word; // If node is empty, set the word
                    }
                    else
                    {
                        TreeNode pointer = this; // Pointer to traverse the tree
                        TreeNode pointer2 = null; // Pointer to remember the parent node
                        bool check_left = false; // Flag to check if we need to go left
                        bool check_right = false; // Flag to check if we need to go right

                        // Traverse the tree to find the correct position
                        while (pointer != null)
                        {
                            pointer2 = pointer; // Save parent node
                            check_left = false; check_right = false; // Reset flags

                            if (pointer.word.CompareTo(info_word) > 0) // If word is smaller, go left
                            {
                                check_left = true;
                                pointer = pointer.leftchild;
                            }
                            else if (pointer.word.CompareTo(info_word) < 0) // If word is larger, go right
                            {
                                check_right = true;
                                pointer = pointer.rightchild;
                            }
                            else // If word already exists, increment count
                            {
                                sayac++;
                                break;
                            }
                        }

                        // Insert the word at the correct position
                        if (check_right)
                        {
                            pointer2.rightchild = new TreeNode(info_word); // Insert on the right
                        }
                        else if (check_left)
                        {
                            pointer2.leftchild = new TreeNode(info_word); // Insert on the left
                        }
                    }
                }
            }
        }

        // TreeNode_2 class for storing fish data
        public class TreeNode_2
        {
            private EgedeniziB fish; // The fish data stored in the node

            public TreeNode_2 rightchild; // Reference to the right child node
            public TreeNode_2 leftchild; // Reference to the left child node

            // Default constructor for creating an empty node
            public TreeNode_2()
            {
                rightchild = null;
                leftchild = null;
                fish = null;
            }

            // Constructor that initializes the node with a fish data
            public TreeNode_2(EgedeniziB fish)
            {
                rightchild = null;
                leftchild = null;
                this.fish = fish;
            }

            // Property for accessing the fish data
            public EgedeniziB Fish
            {
                get { return fish; }
                set { fish = value; }
            }

            // In-order traversal of the tree and display the fish data
            public void inOrder(TreeNode_2 localRoot)
            {
                if (localRoot != null)
                {
                    inOrder(localRoot.leftchild); // Traverse left subtree
                    localRoot.display_data(); // Display the current node's fish data
                    localRoot.fish.information.inOrder(localRoot.fish.information); // Traverse and display fish's additional information
                    Console.WriteLine("\n");
                    inOrder(localRoot.rightchild); // Traverse right subtree
                }
            }

            // Count the total number of nodes in the tree starting from the given node
            public int Node_count(TreeNode_2 localRoot)
            {
                if (localRoot == null)
                {
                    return 0; // Return 0 if the node is null
                }
                return 1 + Node_count(localRoot.leftchild) + Node_count(localRoot.rightchild); // Count current node and recursively count left and right subtrees
            }

            // Calculate the average depth of nodes in the tree
            public int Average_depths(TreeNode_2 localRoot)
            {
                if (localRoot != null)
                {
                    int node_count = localRoot.Fish.information.Node_count(localRoot.Fish.information) - 1; // Get the number of nodes in fish information
                    Console.Write("To be balanced tree depth should be: ");
                    Console.WriteLine(Math.Ceiling(Math.Log(node_count + 1, 2) - 1)); // Calculate expected tree depth for balance
                    int toplam = localRoot.Fish.information.total_depth(localRoot.Fish.information) / node_count; // Calculate the average depth
                    return toplam + Average_depths(localRoot.leftchild) + Average_depths(localRoot.rightchild); // Recursively calculate average depths for left and right subtrees
                }
                return 0; // Return 0 if node is null
            }

            // Display the fish name stored in the current node
            public void display_data()
            {
                Console.WriteLine(fish.Fish_name); // Display fish name
            }
        }

        // EgeDeniziB_Ağacı class to represent the tree of fish
        public class EgeDeniziB_Ağacı
        {
            public TreeNode_2 Balıklar; // Root node of the fish tree

            // Constructor to initialize an empty tree
            public EgeDeniziB_Ağacı()
            {
                Balıklar = new TreeNode_2();
            }

            // Insert a fish into the tree
            public void insert(EgedeniziB fish)
            {
                if (Balıklar.Fish == null)
                {
                    Balıklar.Fish = fish; // If tree is empty, set the root node
                }
                else
                {
                    TreeNode_2 pointer = Balıklar; // Pointer to traverse the tree
                    TreeNode_2 pointer2 = null; // Pointer to remember the parent node
                    bool check_left = false; // Flag to check if we need to go left
                    bool check_right = false; // Flag to check if we need to go right

                    // Traverse the tree to find the correct position
                    while (pointer != null)
                    {
                        pointer2 = pointer; // Save parent node
                        check_left = false;
                        check_right = false;

                        // Compare fish names and decide whether to go left or right
                        if (pointer.Fish.Fish_name.CompareTo(fish.Fish_name) > 0)
                        {
                            pointer = pointer.leftchild;
                            check_left = true;
                        }
                        else if (pointer.Fish.Fish_name.CompareTo(fish.Fish_name) < 0)
                        {
                            pointer = pointer.rightchild;
                            check_right = true;
                        }
                    }

                    // Insert the fish at the correct position
                    if (check_left)
                    {
                        pointer2.leftchild = new TreeNode_2(fish); // Insert on the left
                    }
                    else if (check_right)
                    {
                        pointer2.rightchild = new TreeNode_2(fish); // Insert on the right
                    }
                }
            }

            // List fish names starting from a specific character
            public void listele(TreeNode_2 localRoot, Char Char1 = 'd', Char Char2 = 'h')
            {
                TreeNode_2 pointer = localRoot;
                bool check = true;

                // Traverse the tree to find fish starting with Char1
                while (check)
                {
                    if (pointer != null && (string.Compare(char.ToLower(pointer.Fish.Fish_name[0]).ToString(), char.ToLower(Char1).ToString()) > 0))
                    {
                        pointer = pointer.leftchild;
                    }
                    else if (pointer != null && (string.Compare(char.ToLower(pointer.Fish.Fish_name[0]).ToString(), char.ToLower(Char1).ToString()) < 0))
                    {
                        pointer = pointer.rightchild;
                    }
                    else if (pointer != null && (string.Compare(char.ToLower(pointer.Fish.Fish_name[0]).ToString(), char.ToLower(Char1).ToString()) == 0))
                    {
                        break; // Stop when the desired character is found
                    }
                    else
                    {
                        Console.WriteLine("There is not any fish name begin with " + Char1); // Inform the user if no matching fish found
                        check = false;
                    }
                }

                // List the fish names in the range [Char1, Char2]
                if (check)
                {
                    List<string> list = new List<string>();
                    while (check)
                    {
                        list.Add(pointer.Fish.Fish_name); // Add fish name to the list
                        if (pointer != null && (string.Compare(char.ToLower(pointer.Fish.Fish_name[0]).ToString(), char.ToLower(Char2).ToString()) > 0))
                        {
                            pointer = pointer.leftchild;
                        }
                        else if (pointer != null && (string.Compare(char.ToLower(pointer.Fish.Fish_name[0]).ToString(), char.ToLower(Char2).ToString()) < 0))
                        {
                            pointer = pointer.rightchild;
                        }
                        else if (pointer != null && (string.Compare(char.ToLower(pointer.Fish.Fish_name[0]).ToString(), char.ToLower(Char2).ToString()) == 0))
                        {
                            break; // Stop when the upper limit is reached
                        }
                        if (pointer == null)
                        {
                            Console.WriteLine("There is not any fish name begin with " + Char2 + " after " + list[0]); // Inform if no fish found after the start
                            check = false;
                        }
                    }
                    if (check)
                    {
                        foreach (string fishname in list)
                        {
                            Console.Write(fishname + " "); // Display the fish names
                        }
                    }
                }
            }
        }

        // In-order traversal for the tree of fish
        public static void inOrder(TreeNode_2 localRoot, List<EgedeniziB> lst)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftchild, lst); // Traverse left subtree
                lst.Add(localRoot.Fish); // Add the fish to the list
                inOrder(localRoot.rightchild, lst); // Traverse right subtree
            }
        }

        // Insert fish data into the tree
        public static void insert(EgeDeniziB_Ağacı localRoot, List<EgedeniziB> lst)
        {
            if (lst.Count == 1)
            {
                localRoot.insert(lst[0]); // Insert directly if only one item
            }
            else if (lst.Count != 0)
            {
                if (lst.Count % 2 == 0)
                {
                    localRoot.insert(lst[lst.Count / 2 - 1]); // Insert the middle element
                    insert(localRoot, lst.GetRange(0, lst.Count / 2 - 1)); // Recursively insert left half
                    insert(localRoot, lst.GetRange(lst.Count / 2, lst.Count / 2)); // Recursively insert right half
                }
                else
                {
                    localRoot.insert(lst[lst.Count / 2]); // Insert the middle element
                    insert(localRoot, lst.GetRange(0, lst.Count / 2)); // Recursively insert left half
                    insert(localRoot, lst.GetRange(lst.Count / 2 + 1, lst.Count / 2)); // Recursively insert right half
                }
            }
        }

        // Update fish information in the hashtable
        public static void update_Info(string Fish_name, Hashtable hashtable, char[] seperator)
        {
            if (hashtable.ContainsKey(Fish_name))
            {
                Console.Write($"Enter new Information for {Fish_name} : ");
                String new_info = Console.ReadLine();
                Console.WriteLine();
                EgedeniziB Fish = (EgedeniziB)hashtable[Fish_name];
                hashtable.Remove(Fish_name); // Remove the old information
                String[] info_splited = new_info.Split(seperator); // Split the new info
                Fish.information = new TreeNode();

                // Insert the new information into the tree
                foreach (string word in info_splited)
                {
                    TreeNode tn = new TreeNode(word);
                    Fish.information.insert(word);
                }
                hashtable.Add(Fish_name, Fish); // Add the updated information back to the hashtable
            }
            else
            {
                Console.WriteLine($"There is not {Fish_name} try again please: "); // Prompt the user for the correct fish name
                Fish_name = Console.ReadLine();
                update_Info(Fish_name, hashtable, seperator); // Try again recursively
            }
        }


        static void Main(string[] args)
        {
            char[] seperator = { ',', '.', '(', ')' }; // Separator characters for parsing fish data
            char[] seperator_2 = { ',', '.', '(', ')', ' ' }; // Another set of separator characters for parsing fish data

            EgedeniziB Fish = null; // Variable to hold a fish object
            EgeDeniziB_Ağacı Fish_s = new EgeDeniziB_Ağacı(); // Tree object to store fish data

            try
            {
                List<string> default_area = new List<string> { "Nehir", "Hint - Pasifik", "Ege", "Kızıldeniz", "Endenozya", "Akdeniz", "Karadeniz", "tüm deniz", "diğer deniz", "Marmara" }; // List of default areas
                StreamReader reader = new StreamReader("balik.txt"); // Open the file containing fish data
                String line;
                int a = 1; // Counter for tracking the fish number
                String[] line_splited;

                // Read each line from the file
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    line_splited = line.Split(seperator);
                    if (line_splited[0] == a.ToString()) // If the first element in the line is the expected fish number
                    {
                        Fish = new EgedeniziB(); // Create a new fish object
                        a++; // Increment the fish number
                        Fish.Fish_name = line_splited[1]; // Set the fish name from the file
                        if (line_splited.Length > 2)
                        {
                            Fish.Fish_Other_name = line_splited[2]; // Set the fish's alternative name if it exists
                        }
                        Fish_s.insert(Fish); // Insert the fish into the tree
                    }
                    else
                    {
                        line_splited = line.Split(seperator_2); // Split the line using different separators
                        if (line != "")
                        {
                            // Insert each word into the fish information
                            foreach (string word in line_splited)
                            {
                                Fish.information.insert(word);
                            }

                            // Check if the line contains size information
                            if ((line.Contains("boyut") || line.Contains("cm")) && Fish.Size == "")
                            {
                                Fish.Size = line; // Set the size
                            }

                            // Check if the line mentions specific areas
                            foreach (string area in default_area)
                            {
                                if (line.Contains(area.ToLower()) || line.Contains(area))
                                {
                                    if (area == "tüm deniz" || area == "diğer deniz") // If the line mentions all seas
                                    {
                                        Fish.get_areas().Clear(); // Clear previous areas
                                        Fish.area_Add("Ege"); // Add multiple default areas
                                        Fish.area_Add("Akdeniz");
                                        Fish.area_Add("Karadeniz");
                                        Fish.area_Add("Marmara");
                                        break;
                                    }
                                    else
                                    {
                                        if (!Fish.get_areas().Contains(area))
                                        {
                                            Fish.area_Add(area); // Add the area to the fish's area list
                                        }
                                    }
                                }
                            }
                            // If the fish is not found in the Black Sea, remove it
                            if (line.Contains("bulunmaz"))
                            {
                                Fish.get_areas().Remove("Karadeniz");
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException) // Catch the error if the file is not found
            {
                Console.WriteLine("File Not found"); // Inform the user that the file was not found
            }

            Fish_s.Balıklar.inOrder(Fish_s.Balıklar); // Perform in-order traversal of the fish tree and display the fish

            // Calculate and display the average depth of the fish tree
            Console.WriteLine("Average depth = " + Fish_s.Balıklar.Average_depths(Fish_s.Balıklar) / Fish_s.Balıklar.Node_count(Fish_s.Balıklar));

            // Ask the user to input two letters to filter fish names
            Console.Write("Enter first letter: ");
            char letter = char.Parse(Console.ReadLine());
            Console.Write("\nEnter second letter: ");
            char letter2 = char.Parse(Console.ReadLine());

            Fish_s.listele(Fish_s.Balıklar, letter, letter2); // List fish names in the range of the entered letters

            List<EgedeniziB> List_fishes = new List<EgedeniziB>(); // List to store fish objects
            inOrder(Fish_s.Balıklar, List_fishes); // Perform in-order traversal and populate the list with fish objects

            EgeDeniziB_Ağacı fish_tree = new EgeDeniziB_Ağacı(); // Create a new tree to store the fish

            insert(fish_tree, List_fishes); // Insert fish objects into the tree

            Hashtable hashtable = new Hashtable(); // Create a hashtable to store fish objects by name

            // Add fish to the hashtable
            foreach (EgedeniziB egedeniziB in List_fishes)
            {
                hashtable.Add(egedeniziB.Fish_name, egedeniziB);
            }

            Console.Write("\nEnter fish name: "); // Prompt the user for a fish name
            string new_fish_name = Console.ReadLine();
            Console.WriteLine();

            // Update the fish information in the hashtable
            update_Info(new_fish_name, hashtable, seperator_2);

            Max_heap heap = new Max_heap(); // Create a max heap to store fish objects

            // Add fish objects to the heap
            foreach (EgedeniziB egedeniziB in List_fishes)
            {
                heap.Add(egedeniziB);
            }

            // Remove and print the top 3 fish from the heap
            for (int i = 0; i < 3; i++)
            {
                heap.remove().print_info();
            }

            int[] arr = { 5, 3, 8, 62, 4, 0, 57, 39 }; // Example array for sorting
            Console.WriteLine("Bubble Sort:\nUnsorted Array: { 5, 3, 8, 62, 4, 0, 57, 39 }");
            Bubble_sort(arr); // Sort the array using bubble sort
            Console.WriteLine("********************************************************** \nQuick Sort:");
            int[] arr2 = { 5, 3, 8, 62, 4, 0, 57, 39 }; // Another example array for sorting
            Console.WriteLine("Unsorted Array: { 68, 31, 2, 62, 34, 0, 150, 39 }");
            QuickSort(arr2, 0, arr2.Length - 1); // Sort the array using quicksort

            // Generate a random array for performance testing
            int[] unsortedArray = new int[100];
            Random rand = new Random();
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                unsortedArray[i] = rand.Next(0, 1000); // Fill the array with random numbers
            }

            Console.WriteLine("Time measurement begins...");

            // Measure the time taken by bubble sort
            long bubbleSortTime = MeasureTime(unsortedArray, Bubble_sort2, 10000000);
            Console.WriteLine($"Bubble Sort Total time: {bubbleSortTime} ms");

            // Measure the time taken by quicksort
            long quickSortTime = MeasureTime(unsortedArray, QuickSortWrapper, 10000000);
            Console.WriteLine($"Quick Sort Total time: {quickSortTime} ms");
        }

    }
}