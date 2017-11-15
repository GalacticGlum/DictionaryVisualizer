using System;
using System.Diagnostics.Eventing.Reader;
using DictionaryVisualizer.Utilities;

namespace DictionaryVisualizer
{
    internal class Program
    {
        private static TreeNode<string> rootTreeNode;

        private static void Main(string[] args)
        {
            AddDefinitionToTree(Console.ReadLine(), rootTreeNode);
            
            GraphvizHelper.DrawTree(rootTreeNode, "output_graph.png");
            Console.WriteLine("Finished graph output.");
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
                Console.WriteLine(word);
                wordNode = parentNode.AddChild(word);
            }

            if (WordExistsOnPath(wordNode, word))
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
            if (wordNode.Parent == null) return false;
            return wordNode.Value == word || WordExistsOnPath(wordNode.Parent, word);
        }
    }
}
