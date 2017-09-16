using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Debts {
    
    class BinaryTree {
        
        BinaryTreeNode tree;

        public object Name {
            set { tree.Name = value; }
            get { return tree.Name; }
        }

        public int Counter {
            get { return tree.Counter; }
        }

        public BinaryTree() {
            tree = null;
        }

        private BinaryTree(BinaryTreeNode r) {
            tree = r;
        }

        public BinaryTree ReadFromFile(string path) {
            BinaryTree tree = new BinaryTree();

            using (StreamReader fileIn = new StreamReader(path)) {
                string line = fileIn.ReadToEnd();
                string[] data = line.Split('\n');

                foreach (string item in data) {
                    string[] userData = item.Split(' ');
                    string name = userData[0] + " " + userData[1];
                    List<Phone> phones = new List<Phone>();
                    
                    for (int i = 2; i + 1 < userData.Length; i += 2) {
                        phones.Add(new Phone(userData[i], userData[i + 1]));
                    }
                    
                    tree.Add(name, phones);
                }
            }

            return tree;
        }

        public void Add(object nodeInf) {
            BinaryTreeNode.Add(ref tree, nodeInf);
            BinaryTreeNode.Balancer(ref tree);
        }
        
        public void Add(object nodeInf, List<Phone> phones) {
            BinaryTreeNode.Add(ref tree, nodeInf, phones);
            BinaryTreeNode.Balancer(ref tree);
        }

        public void Preorder() {
            BinaryTreeNode.Preorder(tree);
        }

        public void Inorder() {
            BinaryTreeNode.Inorder(tree);
        }

        public void Postorder() {
            BinaryTreeNode.Postorder(tree);
        }

        public BinaryTree Search(object key) {
            BinaryTreeNode r;
            BinaryTreeNode.Search(tree, key, out r);
            BinaryTree t = new BinaryTree(r);
            return t;
        }
        
        public BinaryTree SearchByPhoneNumber(object phone) {
            BinaryTreeNode r;
            BinaryTreeNode.SearchByPhoneNumber(tree, phone, out r);
            BinaryTree t = new BinaryTree(r);
            return t;
        }

        public void SearchToRoot(object key) {
            BinaryTreeNode.SearchToRoot(ref tree, key);
        }

        public void InsertToRoot(object item) {
            BinaryTreeNode.InsertToRoot(ref tree, item);
        }
        
        public void InsertToRoot(object item, List<Phone> phones) {
            BinaryTreeNode.InsertToRoot(ref tree, item, phones);
        }

        public void Balancer() {
            BinaryTreeNode.Balancer(ref tree);
        }

        public void Delete(object key) {
            BinaryTreeNode.Delete(ref tree, key);
            BinaryTreeNode.Balancer(ref tree);
        }

        public int ProductOfNegativeNodes() {
            int res = 1;
            BinaryTreeNode.FindProductOfNegative(tree, ref res);
            return res;
        }

        public List<KeyValuePair<string, int>> GetMostPopularCallRates() {
            return BinaryTreeNode.GetMostPopularCallRates(tree);
        }

        private ArrayList GetNodeList()
        {
            ArrayList nodes = new ArrayList();
            BinaryTreeNode.GetNodeList(tree, nodes);
            return nodes;
        }

        public void FindHeightForEach() {
            foreach (BinaryTreeNode node in GetNodeList()) {
                Console.WriteLine("Height of " + node.Name + " is " + BinaryTreeNode.FindHeight(node));
            }
        }
        
    }
    
}