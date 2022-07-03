using System.Collections.Generic;

namespace TreesAndGraphs
{
    // How to make a search engine to visit web pages, storing URLs I've alreader visited and only visit the URL if it hasn't already been visited.
    // We can use a trie. If you've never heard of a trie, think of it this way:
    // Let's make visited a nested dictionary where each map has keys of just one character. So we would store 'google.com' as visited['g']['o']['o']['g']['l']['e']['.']['c']['o']['m']['*'] = true.
    // The '*' at the end means 'this is the end of an entry'. Otherwise we wouldn't know what parts of visited are real URLs and which parts are just prefixes. In the example above, 'google.co' is a prefix that we might think is a visited URL if we didn't have some way to mark 'this is the end of an entry.'
    // Now when we go to add 'google.com/maps' to visited, we only have to add the characters '/maps', because the 'google.com' prefix is already there.Same with 'google.com/about/jobs'.
    // We can visualize this as a tree, where each character in a string corresponds to a node.
    // A trie is a type of tree.
    // A trie containing "donut.net", "dogood.org", "dog.com", "dog.com/about", "dog.com/pug", and "dog.org"
    public class MillionGazillion
    {
        public class TrieNode
        {
            private Dictionary<char, TrieNode> _nodeChildren = new Dictionary<char, TrieNode>();

            public bool HasChildNode(char character)
            {
                return _nodeChildren.ContainsKey(character);
            }

            public void MakeChildNode(char character)
            {
                _nodeChildren[character] = new TrieNode();
            }

            public TrieNode GetChildNode(char character)
            {
                return _nodeChildren[character];
            }
        }

        public class Trie
        {
            private const char EndOfWordMarker = '\0';

            private TrieNode _rootNode = new TrieNode();

            public bool AddWord(string word)
            {
                var currentNode = _rootNode;
                bool isNewWord = false;

                // Work donwards through the trie, adding nodes
                // as needed, and keeping track of wether we add
                // any nodes.
                foreach (var character in word)
                {
                    if (!currentNode.HasChildNode(character))
                    {
                        isNewWord = true;
                        currentNode.MakeChildNode(character);
                    }

                    currentNode = currentNode.GetChildNode(character);
                }

                // Explicity mark the end of a word.
                // Otherwise, we might say a word is
                // present if it is a prefix of a different
                // longer word that was added earlier.
                if (!currentNode.HasChildNode(EndOfWordMarker))
                {
                    isNewWord = true;
                    currentNode.MakeChildNode(EndOfWordMarker);
                }

                return isNewWord;
            }
        }
    }
}
