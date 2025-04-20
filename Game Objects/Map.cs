using System;
using System.Collections.Generic;

namespace HeroQuest
{
    public class Map
    {
        // Represents the graph as an adjacency list where each node (room) maps to a list of edges (connections to other rooms).
        public Dictionary<int, List<Edge>> AdjacencyList { get; private set; }

        // The designated exit node (room) in the map.
        public int ExitNode { get; private set; }

        // Random number generator for creating the map and assigning the exit node.
        private Random random;

        // Constructor to initialize the map with a specified number of nodes (rooms).
        public Map(int numNodes)
        {
            random = new Random(); // Initialize the random number generator.
            InitializeAdjacencyList(numNodes); // Set up the adjacency list for the graph.
            ExitNode = random.Next(1, numNodes + 1); // Randomly select the exit node.
            Console.WriteLine($"The exit is in room: {ExitNode}"); // Inform the user of the exit room.
            GenerateRandomMap(numNodes); // Generate the random map with connections.
            EnsurePathToExit(1, ExitNode); // Ensure there is a valid path from the start node to the exit node.
        }

        // Initializes the adjacency list for the graph with the specified number of nodes.
        private void InitializeAdjacencyList(int numNodes)
        {
            AdjacencyList = new Dictionary<int, List<Edge>>(); // Create a new dictionary to store the adjacency list.
            for (int i = 1; i <= numNodes; i++) // Loop through each node.
            {
                AdjacencyList[i] = new List<Edge>(); // Initialize an empty list of edges for each node.
            }
        }

        // Adds an edge (connection) between two nodes (rooms) with optional requirements.
        public void AddEdge(int u, int v, Attribute requiredStatType = Attribute.Intelligence, string requiredItem = null, int minimumStatValue = 0)
        {
            if (!IsAdjacent(u, v)) // Check if the edge already exists to avoid duplicates.
            {
                // Add the edge from node u to node v with the specified requirements.
                AdjacencyList[u].Add(new Edge(v, requiredStatType, requiredItem, minimumStatValue));
                // Add the edge from node v to node u (undirected graph).
                AdjacencyList[v].Add(new Edge(u, requiredStatType, requiredItem, minimumStatValue));
            }
        }

        // Generates a random map by ensuring basic connectivity and adding random edges.
        private void GenerateRandomMap(int numNodes)
        {
            EnsureBasicConnectivity(numNodes); // Ensure every node is connected to at least one other node.
            AddRandomEdges(numNodes); // Add additional random edges to increase complexity.
        }

        // Ensures that the graph is connected by creating a basic path through all nodes.
        private void EnsureBasicConnectivity(int numNodes)
        {
            for (int i = 1; i < numNodes; i++) // Loop through each node except the last one.
            {
                int neighbor = random.Next(i + 1, numNodes + 1); // Randomly select a neighbor node.
                AddEdge(i, neighbor); // Add an edge between the current node and the neighbor.
            }
        }

        // Adds additional random edges to the graph to make it more complex.
        private void AddRandomEdges(int numNodes)
        {
            for (int i = 0; i < numNodes * 2; i++) // Add twice as many edges as there are nodes.
            {
                int u = random.Next(1, numNodes + 1); // Randomly select the first node.
                int v = random.Next(1, numNodes + 1); // Randomly select the second node.
                if (u != v) // Ensure the two nodes are not the same.
                {
                    AddEdge(u, v); // Add an edge between the two nodes.
                }
            }
        }

        // Ensures there is a valid path from the start node to the exit node.
        private void EnsurePathToExit(int startNode, int exitNode)
        {
            if (!IsGraphConnected()) // Check if the graph is connected.
            {
                Console.WriteLine("Graph is not connected. Adding direct path to exit...");
                AddEdge(startNode, exitNode); // Add a direct edge from the start node to the exit node.
            }
        }

        // Checks if two nodes are directly connected by an edge.
        private bool IsAdjacent(int u, int v)
        {
            // Check if there is an edge in the adjacency list from node u to node v.
            return AdjacencyList[u].Exists(edge => edge.Target == v);
        }

        // Determines if the graph is connected using Depth-First Search (DFS).
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

            // The graph is connected if all nodes have been visited.
            return visited.Count == AdjacencyList.Count;
        }
    }
}