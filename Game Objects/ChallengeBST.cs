namespace HeroQuest{
    public class ChallengeBST{
        public ChallengeNode Root { get; private set; }

        // Inserts a new challenge into the BST
        public void Insert(Challenge challenge)
        {
            Root = Insert(Root, challenge);
        }

        private ChallengeNode Insert(ChallengeNode node, Challenge challenge)
        {
            if (node == null)
            {
                return new ChallengeNode(challenge);
            }

            if (challenge.Difficulty < node.Challenge.Difficulty)
            {
                node.Left = Insert(node.Left, challenge);
            }
            else if (challenge.Difficulty > node.Challenge.Difficulty)
            {
                node.Right = Insert(node.Right, challenge);
            }

            return node;
        }
          // Time Complexity:
            // unblanaced h = O(n)
            //balanced  h = O(log n).

        // Searches for the closest challenge based on difficulty
        public Challenge FindClosestChallenge(int difficulty)
        {
            return FindClosestChallenge(Root, difficulty, null);
        }

        private Challenge FindClosestChallenge(ChallengeNode node, int difficulty, Challenge closest)
        {
            if (node == null)
            {
                return closest;
            }

            if (closest == null || Math.Abs(node.Challenge.Difficulty - difficulty) < Math.Abs(closest.Difficulty - difficulty))
            {
                closest = node.Challenge;
            }

            if (difficulty < node.Challenge.Difficulty)
            {
                return FindClosestChallenge(node.Left, difficulty, closest);
            }
            else
            {
                return FindClosestChallenge(node.Right, difficulty, closest);
            }

                // Time Complexity:
            // unblanaced h = O(n)
            //balanced  h = O(log n).
        }

        // Removes a challenge from the BST
        public void Remove(int difficulty)
        {
            Root = Remove(Root, difficulty);
        }

        private ChallengeNode Remove(ChallengeNode node, int difficulty)
        {
            if (node == null)
            {
                return null;
            }

            if (difficulty < node.Challenge.Difficulty)
            {
                node.Left = Remove(node.Left, difficulty);
            }
            else if (difficulty > node.Challenge.Difficulty)
            {
                node.Right = Remove(node.Right, difficulty);
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }

                // Get inorder successor 
                node.Challenge = FindMin(node.Right).Challenge;

                // Delete the inorder successor
                node.Right = Remove(node.Right, node.Challenge.Difficulty);
            }

            // Time Complexity:
            // unblanaced h = O(n)
            //balanced  h = O(log n).

            return node;
        }

        private ChallengeNode FindMin(ChallengeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

            public void InOrderTraversal(Action<Challenge> action)
        {
            InOrderTraversal(Root, action);
        }

        private void InOrderTraversal(ChallengeNode node, Action<Challenge> action) //uses challenge delegate
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, action);
                action(node.Challenge);
                InOrderTraversal(node.Right, action);
            }
        }
    }

    public class ChallengeNode
    {
        public Challenge Challenge { get; set; }
        public ChallengeNode Left { get; set; }
        public ChallengeNode Right { get; set; }

        public ChallengeNode(Challenge challenge)
        {
            Challenge = challenge;
        }
    }

}
