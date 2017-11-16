using System;
using DictionaryVisualizer.Utilities;

namespace DictionaryVisualizer
{
    internal class Program
    {
        private static TreeNode<string> rootTreeNode;

        private static void Main(string[] args)
        {
            Console.WriteLine("What is the root word?");
            string root = Console.ReadLine();

            Console.WriteLine("What is the path of the output file (relative to this executable).");
            string outputFilepath = Console.ReadLine();

            AddDefinitionToTree(root, rootTreeNode);
            GraphvizHelper.DrawTree(rootTreeNode, outputFilepath);
        }

        private static void AddDefinitionToTree(string word, TreeNode<string> parentNode = null)
        {
            TreeNode<string> wordNode;
            if (parentNode == null)
            {
                rootTreeNode = new TreeNode<string>(word);
                wordNode = rootTreeNode;
            }
            else
            {       
                wordNode = parentNode.AddChild(word);
            }

            if (wordNode.Parent != null && WordExistsOnPath(wordNode.Parent, word))
            {
                return;
            }

            string[] definitionWords = DefinitionManager.GetDefinition(word);
            foreach (string definition in definitionWords)
            {
                AddDefinitionToTree(definition, wordNode);
            }
        }

        private static bool WordExistsOnPath(TreeNode<string> wordNode, string word)
        {
            if (wordNode.Value == word) return true;
            return wordNode.Parent != null && WordExistsOnPath(wordNode.Parent, word);
        }
    }
}
