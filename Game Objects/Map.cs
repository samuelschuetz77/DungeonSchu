using System;
using System.Collections.Generic;

namespace HeroQuest
{
    public class Map
    {
        public Dictionary<int, List<Edge>> AdjacencyList { get; private set; }
        public int ExitNode { get; private set; }
        private Random random;
        public Map(int numNodes)
        {
            random = new Random(); 
            InitializeAdjacencyList(numNodes); 
            ExitNode = random.Next(1, numNodes + 1); 
            Console.WriteLine($"The exit is in room: {ExitNode}");
            GenerateRandomMap(numNodes); 
            EnsurePathToExit(1, ExitNode); 
        }
        private void InitializeAdjacencyList(int numNodes)
        {
            AdjacencyList = new Dictionary<int, List<Edge>>(); 
            for (int i = 1; i <= numNodes; i++) // Loop through each node.
            {
                AdjacencyList[i] = new List<Edge>(); 
            }
        }
        public void AddEdge(int u, int v, Attribute requiredStatType = Attribute.Intelligence, string requiredItem = null, int minimumStatValue = 0)
        {
            if (!IsAdjacent(u, v)) 
            {
                // Add the edge from node u to node v with the specified requirements
                AdjacencyList[u].Add(new Edge(v, requiredStatType, requiredItem, minimumStatValue));
                // Add the edge from node v to node u (undirected graph)
                AdjacencyList[v].Add(new Edge(u, requiredStatType, requiredItem, minimumStatValue));
            }
        }
        private void GenerateRandomMap(int numNodes)
        {
            EnsureBasicConnectivity(numNodes); 
            AddRandomEdges(numNodes); 
        }
        private void EnsureBasicConnectivity(int numNodes)
        {
            for (int i = 1; i < numNodes; i++) 
            {
                int neighbor = random.Next(i + 1, numNodes + 1); //randomly select one neighbor 
                AddEdge(i, neighbor); //add an edge between the current node and the neighbor node
            }
        }

        // Adds additional random edges to the graph to make it more complex.
        private void AddRandomEdges(int numNodes)
        {
            for (int i = 0; i < numNodes * 2; i++)
            {
                int u = random.Next(1, numNodes + 1); //random selection
                int v = random.Next(1, numNodes + 1); // random selection
                if (u != v) 
                {
                    AddEdge(u, v); // Add an edge between the two nodes.
                }
            }
        }
        private void EnsurePathToExit(int startNode, int exitNode)
        {
            if (!IsGraphConnected()) 
            {
                Console.WriteLine("Graph is not connected. Adding direct path to exit...");
                AddEdge(startNode, exitNode); // Add an edge from start to finish to ensure a path to the exit
            }
        }

        // Checks if two nodes are connected
        private bool IsAdjacent(int u, int v)
        {
            return AdjacencyList[u].Exists(edge => edge.Target == v);
        }

        // Uses Depth First Search to ensure the graph is connected
        private bool IsGraphConnected()
        {
            HashSet<int> visited = new HashSet<int>(); // Keep track of visited nodes.
            Stack<int> stack = new Stack<int>(); // Use a stack to track nodes during DFS.
            stack.Push(1); // Start the traversal from node 1.

            while (stack.Count > 0) // Continue until there are no more nodes to visit.
            {
                int currentNode = stack.Pop(); // Get the next node to process.
                if (!visited.Contains(currentNode)) // If the node has not been visited yet:
                {
                    visited.Add(currentNode); // Mark the node as visited.
                    foreach (var edge in AdjacencyList[currentNode]) // Loop through all edges of the current node.
                    {
                        if (!visited.Contains(edge.Target)) // If the target node has not been visited:
                        {
                            stack.Push(edge.Target); // Add the target node to the stack for processing.
                        }
                    }
                }
            }
            
        // Time Complexity: O(V + E), V=num nodes e=num edges

            // The graph is connected if all nodes have been visited.
            return visited.Count == AdjacencyList.Count;
        }
    }
}