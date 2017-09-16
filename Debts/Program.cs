using System;
using System.Collections.Generic;

namespace Debts {

    class Program {

        static void Main(string[] args) {
            BinaryTree tree = new BinaryTree().ReadFromFile("../../input.txt");
            tree.Inorder();
            Console.WriteLine();
            Console.WriteLine(tree.SearchByPhoneNumber("9372204031").Name);

            tree.Delete("Турченков Паштет");
            
            var res = tree.GetMostPopularCallRates();
            
            foreach (KeyValuePair<string,int> pair in res) {
                Console.Write(pair.Key + " ");
            }
        }

    }

}