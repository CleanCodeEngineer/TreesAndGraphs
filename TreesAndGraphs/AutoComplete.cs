using System;
using System.Collections.Generic;

namespace TreesAndGraphs
{
    // Question from bytebybyte
    // Write an autocomplete class that returns all dictionary words with a given prefix.
    // dic: {"abc", "acd", "bcd", "def", "a", "aba"}
    // prefix: "a" -> "abc", "acd", "a", "aba"
    // prefix: "b" -> "bcd"
    //            O
    //         a/   b\
    //         0      O
    //       b/ c\
    //       O
    //     a/ c\
    //     0    0
    // We'll solve this question using Trie(Prefix Tree)
    // "def", "a", "aba"
    // s = "def"
    // i = 2
    //          "", {'d'}, false
    //                  \d
    //              "d", {'e'}, false
    //                     \e
    //               "de", {'f'}, false
    //                       \f
    //                "def", {}, true
    public class AutoComplete
    {
        public class Node
        {
            public string Prefix { get; set; }

            // Hash Map<Char, Node>
            public Dictionary<Char, Node> Children { get; set; }
            public Boolean IsWord { get; set; }

            public Node(string prefix)
            {
                Prefix = prefix;
                Children = new Dictionary<Char, Node>();
            }
        }

        public Node Trie { get; set; }

        public AutoComplete(string[] dict)
        {
            Trie = new Node("");

            foreach (string str in dict)
                InsertWord(str);
        }

        private void InsertWord(string str)
        {
            Node curr = Trie;

            for (int i = 0; i < str.Length; i++)
            {
                if (!curr.Children.ContainsKey(str[i]))
                {
                    curr.Children.Add(str[i], new Node(str.Substring(0, i + 1)));
                }

                curr = curr.Children[str[i]];

                if (i == str.Length - 1)
                    curr.IsWord = true;
            }
        }

        // "ab", "a"
        //            "", {'a'}, false
        //              /  
        //          "a", {'b'}, true
        //              /
        //        "ab", {}, true
        //
        // {"a", "ab"}
        public List<string> GetWordsForPrefix(string pre)
        {
            List<string> results = new List<string>();

            Node curr = Trie;

            foreach (char c in pre.ToCharArray())
            {
                if (curr.Children.ContainsKey(c))
                {
                    curr = curr.Children[c];
                }
                else
                {
                    return results;
                }
            }

            FindAllChildWords(curr, results);

            return results;
        }

        private void FindAllChildWords(Node n, List<string> results)
        {
            if (n.IsWord) results.Add(n.Prefix);

            foreach (char c in n.Children.Keys)
                FindAllChildWords(n.Children[c], results);
        }
    }
}
