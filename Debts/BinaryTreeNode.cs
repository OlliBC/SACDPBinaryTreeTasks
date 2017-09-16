using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Debts {

    class BinaryTreeNode {

        public object Name { get; set; }
        public List<Phone> Phones;

        public int Counter;
        public BinaryTreeNode Left;
        public BinaryTreeNode Right;

        public BinaryTreeNode(object nodeName) {
            Name = nodeName;
            Counter = 1;
            Left = null;
            Right = null;
        }

        public BinaryTreeNode(object nodeName, List<Phone> phones) {
            Name = nodeName;
            Counter = 1;
            Left = null;
            Right = null;
            Phones = phones;
        }

        public static void Add(ref BinaryTreeNode r, object nodeName) {
            if (r == null) {
                r = new BinaryTreeNode(nodeName);
            } else {
                r.Counter++;
                if (((IComparable) r.Name).CompareTo(nodeName) > 0) {
                    Add(ref r.Left, nodeName);
                } else {
                    Add(ref r.Right, nodeName);
                }
            }
        }

        public static void Add(ref BinaryTreeNode r, object nodeName, List<Phone> phones) {
            if (r == null) {
                r = new BinaryTreeNode(nodeName, phones);
            } else {
                r.Counter++;
                if (((IComparable) r.Name).CompareTo(nodeName) > 0) {
                    Add(ref r.Left, nodeName, phones);
                } else {
                    Add(ref r.Right, nodeName, phones);
                }
            }
        }

        public static void Preorder(BinaryTreeNode r) {
            if (r != null) {
                Console.Write(r.Name);
                foreach (Phone phone in GetPhones(r)) {
                    Console.Write(", " + phone.Number + ", " + phone.CallRate);
                }
                Console.WriteLine();
                Preorder(r.Left);
                Preorder(r.Right);
            }
        }

        public static void Inorder(BinaryTreeNode r) {
            if (r != null) {
                Inorder(r.Left);
                Console.Write(r.Name);
                foreach (Phone phone in GetPhones(r)) {
                    Console.Write(", " + phone.Number + ", " + phone.CallRate);
                }
                Console.WriteLine();
                Inorder(r.Right);
            }
        }

        public static void Postorder(BinaryTreeNode r) {
            if (r != null) {
                Postorder(r.Left);
                Postorder(r.Right);
                Console.Write(r.Name);
                foreach (Phone phone in GetPhones(r)) {
                    Console.Write(", " + phone.Number + ", " + phone.CallRate);
                }
                Console.WriteLine();
            }
        }

        public static void Part(ref BinaryTreeNode t, int k) {
            int x = t.Left == null ? 0 : t.Left.Counter;
            if (x > k) {
                Part(ref t.Left, k);
                RotationRight(ref t);
            }
            if (x < k) {
                Part(ref t.Right, k - x - 1);
                RotationLeft(ref t);
            }
        }

        public static void Balancer(ref BinaryTreeNode t) {
            if (t == null || t.Counter == 1) return;
            Part(ref t, t.Counter / 2);
            Balancer(ref t.Left);
            Balancer(ref t.Right);
        }

        public static void Search(BinaryTreeNode r, object key, out BinaryTreeNode item) {
            if (r == null) {
                item = null;
            } else {
                if (((IComparable) r.Name).CompareTo(key) == 0) {
                    item = r;
                } else {
                    if (((IComparable) r.Name).CompareTo(key) > 0) {
                        Search(r.Left, key, out item);
                    } else {
                        Search(r.Right, key, out item);
                    }
                }
            }
        }

        public static void SearchByPhoneNumber(BinaryTreeNode r, object phone, out BinaryTreeNode item) {
            ArrayList nodes = new ArrayList();
            GetNodeList(r, nodes);
            item = null;

            foreach (BinaryTreeNode node in nodes) {
                if (node.Phones.Any(x => x.Number == (string) phone)) {
                    item = node;
                }
            }
        }

        public static void SearchToRoot(ref BinaryTreeNode r, object key) {
            if (r != null) {
                if (((IComparable) r.Name).CompareTo(key) == 0) {
                    return;
                } else {
                    if (((IComparable) r.Name).CompareTo(key) > 0) {
                        SearchToRoot(ref r.Left, key);
                        RotationRight(ref r);
                    } else {
                        SearchToRoot(ref r.Right, key);
                        RotationLeft(ref r);
                    }
                }
            }
        }

        public static void Count(ref BinaryTreeNode r) {
            r.Counter = 1;
            if (r.Left != null) r.Counter += r.Left.Counter;
            if (r.Right != null) r.Counter += r.Right.Counter;
        }

        public static void RotationRight(ref BinaryTreeNode t) {
            BinaryTreeNode x = t.Left;
            t.Left = x.Right;
            x.Right = t;

            Count(ref t);
            Count(ref x);

            t = x;
        }

        public static void RotationLeft(ref BinaryTreeNode t) {
            BinaryTreeNode x = t.Right;
            t.Right = x.Left;
            x.Left = t;

            Count(ref t);
            Count(ref x);

            t = x;
        }

        public static void InsertToRoot(ref BinaryTreeNode t, object nodeName) {
            if (t == null) {
                t = new BinaryTreeNode(nodeName);
            } else {
                t.Counter++;
                if (((IComparable) t.Name).CompareTo(nodeName) > 0) {
                    InsertToRoot(ref t.Left, nodeName);
                    RotationRight(ref t);
                } else {
                    InsertToRoot(ref t.Right, nodeName);
                    RotationLeft(ref t);
                }
            }
        }

        public static void InsertToRoot(ref BinaryTreeNode t, object nodeName, List<Phone> phones) {
            if (t == null) {
                t = new BinaryTreeNode(nodeName, phones);
            } else {
                t.Counter++;
                if (((IComparable) t.Name).CompareTo(nodeName) > 0) {
                    InsertToRoot(ref t.Left, nodeName, phones);
                    RotationRight(ref t);
                } else {
                    InsertToRoot(ref t.Right, nodeName, phones);
                    RotationLeft(ref t);
                }
            }
        }

        private static void Del(BinaryTreeNode t, ref BinaryTreeNode tr) {
            if (tr.Right != null) {
                Del(t, ref tr.Right);
            } else {
                t.Name = tr.Name;
                tr = tr.Left;
            }
        }

        public static void Delete(ref BinaryTreeNode t, object key) {
            if (t == null) throw new Exception("Данное значение в дереве отсутствует");

            t.Counter--;
            if (((IComparable) t.Name).CompareTo(key) > 0) {
                Delete(ref t.Left, key);
            } else {
                if (((IComparable) t.Name).CompareTo(key) < 0) {
                    Delete(ref t.Right, key);
                } else {
                    if (t.Left == null) {
                        t = t.Right;
                    } else {
                        if (t.Right == null) {
                            t = t.Left;
                        } else {
                            Del(t, ref t.Left);
                        }
                    }
                }
            }
        }

        public static void GetNodeList(BinaryTreeNode r, ArrayList nodes)
        {
            if (r != null)
            {
                nodes.Add(r);
                GetNodeList(r.Left, nodes);
                GetNodeList(r.Right, nodes);
            }
        }

        public static List<KeyValuePair<string, int>> GetMostPopularCallRates(BinaryTreeNode r) {
            Dictionary<string, int> callRates = new Dictionary<string, int>();
            ArrayList nodes = new ArrayList();
            GetNodeList(r, nodes);

            foreach (BinaryTreeNode node in nodes) {
                foreach (Phone phone in node.Phones) {
                    if (callRates.ContainsKey(phone.CallRate)) {
                        callRates[phone.CallRate]++;
                    } else {
                        callRates.Add(phone.CallRate, 1);
                    }
                }
            }

            return callRates
                .OrderByDescending(x => x.Value)
                .TakeWhile(y => y.Value == callRates.Values.Max())
                .ToList();
        }
        
        public static List<Phone> GetPhones(BinaryTreeNode r) {
            return r.Phones;
        }

        public static void FindProductOfNegative(BinaryTreeNode node, ref int res) {
            if (node != null) {
                if ((int) node.Name < 0) {
                    res *= (int) node.Name;
                }
                FindProductOfNegative(node.Left, ref res);
                FindProductOfNegative(node.Right, ref res);
            }
        }

        public static int FindHeight(BinaryTreeNode node) {
            if (node != null) return Math.Max(FindHeight(node.Left), FindHeight(node.Right)) + 1;
            return 0;
        }

    }

}