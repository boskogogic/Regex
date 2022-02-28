using System;

namespace ProjekatNoviNovi
{
    internal class SingleLinkedList
    {
        internal class Node
        {
            internal string token;
            internal string reg;
            internal Node next;
            public Node(string t, string r)
            {
                token = t;
                reg = r;
                next = null;
            }

            public Node()
            {
                token = null;
                reg = null;
                next = null;
            }


        }

        internal Node head;

        /*internal void InsertFront(SingleLinkedList singlyList, int new_data)
        {
            Node new_node = new Node(new_data);
            new_node.next = singlyList.head;
            singlyList.head = new_node;
        }*/


        internal void InsertLastRegex(SingleLinkedList singlyList, string new_reg)
        {
            Node new_node = new Node(null,new_reg);
            if (singlyList.head == null)
            {
                singlyList.head = new_node;
                return;
            }
            Node lastNode = GetLastNode(singlyList);
            lastNode.next = new_node;
        }

        internal void InsertLastToken(SingleLinkedList singlyList, string new_token)
        {
            Node new_node = new Node(new_token,null);
            if (singlyList.head == null)
            {
                singlyList.head = new_node;
                return;
            }
            Node lastNode = GetLastNode(singlyList);
            lastNode.next = new_node;
        }


        internal Node GetLastNode(SingleLinkedList singlyList)
        {
            Node temp = singlyList.head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            return temp;
        }
        
        
        /*internal void InsertAfter(Node prev_node, int new_data)
        {
            if (prev_node == null)
            {
                Console.WriteLine("The given previous node Cannot be null");
                return;
            }
            Node new_node = new Node(new_data);
            new_node.next = prev_node.next;
            prev_node.next = new_node;

        }

        internal void DeleteNodebyKey(SingleLinkedList singlyList, int key)
        {
            Node temp = singlyList.head;
            Node prev = null;
            if (temp != null && temp.data == key)
            {
                singlyList.head = temp.next;
                return;
            }
            while (temp != null && temp.data != key)
            {
                prev = temp;
                temp = temp.next;
            }
            if (temp == null)
            {
                return;
            }
            prev.next = temp.next;
        }

        public void ReverseLinkedList(SingleLinkedList singlyList)
        {
            Node prev = null;
            Node current = singlyList.head;
            Node temp = null;
            while (current != null)
            {
                temp = current.next;
                current.next = prev;
                prev = current;
                current = temp;
            }
            singlyList.head = prev;
        }*/


    }
}
