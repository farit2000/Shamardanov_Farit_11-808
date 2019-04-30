using System;
using System.Diagnostics;
using System.Linq;

namespace BTree
{
    /*
     * Алгоритм основан на основе книги "Введение в алгоритмы"
     * Tk - ключ, название фала который должен быть сравнимым
     * Tp - данные, связанные с ключом, в данном случае это путь к файлу    
    */   
    public class BTree<TKey, TPath> where TKey : IComparable<TKey>
    {
        public BTree(int degree)
        {
            if (degree < 2)
            {
                throw new ArgumentException("Степень должна быть больше или равна 2")
            }

            this.Root = new Node<TKey, TPath>(degree);
            this.Degree = degree;
            this.Height = 1;
        }
        //корень дерева
        public Node<TKey, TPath> Root { get; private set; }

        public int Degree { get; private set; }

        public int Height { get; private set; }

        // Метод поиска значения по ключу, метод возвращает имя файла и путь на этот файл
        public Entry<TKey, TPath> Search(TKey key)
        {
            return this.SearchInternal(this.Root, key);
        }

        // Вставляет новый ключ, связанный с указателем в дерево. 
        //Эта операция разбивает узлы по мере необходимости, чтобы сохранить свойства В дерева.
        public void Add(TKey newKey, TPath newPointer)
        {
            // в корне есть пробел
            if (!this.Root.HasReachedMaxEntries)
            {
                this.InsertNonFull(this.Root, newKey, newPointer);
                return;
            }

            //нужно создать новый узел и разделить его
            Node<TKey, TPath> oldRoot = this.Root;
            this.Root = new Node<TKey, TPath>(this.Degree);
            this.Root.Children.Add(oldRoot);
            this.SplitChild(this.Root, 0, oldRoot);
            this.InsertNonFull(this.Root, newKey, newPointer);

            this.Height++;
        }

        //Удаляет ключ из сбалансированного дерева. Эта операция перемещает ключи и узлы,
        //необходимые для сохранения свойств B дерева.
        public void Delete(TKey keyToDelete)
        {
            this.DeleteInternal(this.Root, keyToDelete);

            // если последняя запись была перемещена на дочерний узел, мы ее удаляем ее
            if (this.Root.Entries.Count == 0 && !this.Root.IsLeaf)
            {
                this.Root = this.Root.Children.Single();
                this.Height--;
            }
        }

        //Вспомогательный метод для удаления ключей из дерева
        private void DeleteInternal(Node<TKey, TPath> node, TKey keyToDelete)
        {
            int i = node.Entries.TakeWhile(entry => keyToDelete.CompareTo(entry.Key) > 0).Count();

            //если найден ключ то мы его удаляем
            if (i < node.Entries.Count && node.Entries[i].Key.CompareTo(keyToDelete) == 0)
            {
                this.DeleteKeyFromNode(node, keyToDelete, i);
                return;
            }

            //удаляем ключ из поддерева
            if (!node.IsLeaf)
            {
                this.DeleteKeyFromSubtree(node, keyToDelete, i);
            }
        }

        //Вспомогательный метод, который удаляет ключ из поддерева.
        private void DeleteKeyFromSubtree(Node<TKey, TPath> parentNode, TKey keyToDelete, int subtreeIndexInNode)
        {
            Node<TKey, TPath> childNode = parentNode.Children[subtreeIndexInNode];
            //узел достиг минимума, и удаление любого из них нарушит свойство В дерева,
            //поэтому этот блок гарантирует, что дочерний элемени имеет хотя бы степень не 0 узлов,
            //после перемещаем запись из узла-брата или объединяем узлы
            if (childNode.HasReachedMinEntries)
            {
                int leftIndex = subtreeIndexInNode - 1;
                Node<TKey, TPath> leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] : null;

                int rightIndex = subtreeIndexInNode + 1;
                Node<TKey, TPath> rightSibling = subtreeIndexInNode < parentNode.Children.Count - 1
                                                ? parentNode.Children[rightIndex]
                                                : null;

                if (leftSibling != null && leftSibling.Entries.Count > this.Degree - 1)
                {
                    //у левого брата есть запасной узел, поэтому он перемещает один узел из левого брата
                    //в родительский узел и один узел из родительского в этот текущий узел дочерний
                    childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
                    parentNode.Entries[subtreeIndexInNode] = leftSibling.Entries.Last();
                    leftSibling.Entries.RemoveAt(leftSibling.Entries.Count - 1);

                    if (!leftSibling.IsLeaf)
                    {
                        childNode.Children.Insert(0, leftSibling.Children.Last());
                        leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
                    }
                }
                else if (rightSibling != null && rightSibling.Entries.Count > this.Degree - 1)
                {
                    //у правого брата есть запасной узел, поэтому он перемещает один узел из правого брата
                    // в родительский узел и один узел из родительского в этот текущий узел
                    childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
                    parentNode.Entries[subtreeIndexInNode] = rightSibling.Entries.First();
                    rightSibling.Entries.RemoveAt(0);

                    if (!rightSibling.IsLeaf)
                    {
                        childNode.Children.Add(rightSibling.Children.First());
                        rightSibling.Children.RemoveAt(0);
                    }
                }
                else
                {
                    //этот блок объединяет левого или правого брата в текущий узел
                    if (leftSibling != null)
                    {
                        childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
                        var oldEntries = childNode.Entries;
                        childNode.Entries = leftSibling.Entries;
                        childNode.Entries.AddRange(oldEntries);
                        if (!leftSibling.IsLeaf)
                        {
                            var oldChildren = childNode.Children;
                            childNode.Children = leftSibling.Children;
                            childNode.Children.AddRange(oldChildren);
                        }

                        parentNode.Children.RemoveAt(leftIndex);
                        parentNode.Entries.RemoveAt(subtreeIndexInNode);
                    }
                    else
                    {
                        Debug.Assert(rightSibling != null, "Node should have at least one sibling");
                        childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
                        childNode.Entries.AddRange(rightSibling.Entries);
                        if (!rightSibling.IsLeaf)
                        {
                            childNode.Children.AddRange(rightSibling.Children);
                        }

                        parentNode.Children.RemoveAt(rightIndex);
                        parentNode.Entries.RemoveAt(subtreeIndexInNode);
                    }
                }
            }
            //на данный момент мы знаем, что потомок имеет по крайней мере узлы не нулевой степени,
            //поэтому мы можем двигаться дальше - это гарантирует, что если некоторый узел необходимо удалить из поддерева,
            // чтобы гарантировать свойство B дерева.
            this.DeleteInternal(childNode, keyToDelete);
        }

        //Вспомогательный метод, который удаляет ключ из узла, содержащего его,
        // будь то этот узел листового узла или внутреннего узла.
        private void DeleteKeyFromNode(Node<TKey, TPath> node, TKey keyToDelete, int keyIndexInNode)
        {
            //если лист, просто удалите его, чтобы сохранить свойство В дерева
            if (node.IsLeaf)
            {
                node.Entries.RemoveAt(keyIndexInNode);
                return;
            }

            Node<TKey, TPath> predecessorChild = node.Children[keyIndexInNode];
            if (predecessorChild.Entries.Count >= this.Degree)
            {
                Entry<TKey, TPath> predecessor = this.DeletePredecessor(predecessorChild);
                node.Entries[keyIndexInNode] = predecessor;
            }
            else
            {
                Node<TKey, TPath> successorChild = node.Children[keyIndexInNode + 1];
                if (successorChild.Entries.Count >= this.Degree)
                {
                    Entry<TKey, TPath> successor = this.DeleteSuccessor(predecessorChild);
                    node.Entries[keyIndexInNode] = successor;
                }
                else
                {
                    predecessorChild.Entries.Add(node.Entries[keyIndexInNode]);
                    predecessorChild.Entries.AddRange(successorChild.Entries);
                    predecessorChild.Children.AddRange(successorChild.Children);

                    node.Entries.RemoveAt(keyIndexInNode);
                    node.Children.RemoveAt(keyIndexInNode + 1);

                    this.DeleteInternal(predecessorChild, keyToDelete);
                }
            }
        }
        // Вспомогательный метод, который удаляет ключ предшественника (т.е. самый правый ключ) для данного узла.
        private Entry<TKey, TPath> DeletePredecessor(Node<TKey, TPath> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[node.Entries.Count - 1];
                node.Entries.RemoveAt(node.Entries.Count - 1);
                return result;
            }

            return this.DeletePredecessor(node.Children.Last());
        }
        // Вспомогательный метод, который удаляет ключ-преемник (т. е. самый левый ключ) для данного узла.
        private Entry<TKey, TPath> DeleteSuccessor(Node<TKey, TPath> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[0];
                node.Entries.RemoveAt(0);
                return result;
            }

            return this.DeletePredecessor(node.Children.First());
        }
        // Helper method that search for a key in a given BTree.
        private Entry<TKey, TPath> SearchInternal(Node<TKey, TPath> node, TKey key)
        {
            int i = node.Entries.TakeWhile(entry => key.CompareTo(entry.Key) > 0).Count();

            if (i < node.Entries.Count && node.Entries[i].Key.CompareTo(key) == 0)
            {
                return node.Entries[i];
            }
            return node.IsLeaf ? null : this.SearchInternal(node.Children[i], key);
        }
        // Вспомогательный метод, который разбивает весь узел на два узла.
        private void SplitChild(Node<TKey, TPath> parentNode, int nodeToBeSplitIndex, Node<TKey, TPath> nodeToBeSplit)
        {
            var newNode = new Node<TKey, TPath>(this.Degree);

            parentNode.Entries.Insert(nodeToBeSplitIndex, nodeToBeSplit.Entries[this.Degree - 1]);
            parentNode.Children.Insert(nodeToBeSplitIndex + 1, newNode);

            newNode.Entries.AddRange(nodeToBeSplit.Entries.GetRange(this.Degree, this.Degree - 1));

            //удаляем запись степень - 1, который является единственной, для перехода к родителю
            nodeToBeSplit.Entries.RemoveRange(this.Degree - 1, this.Degree);

            if (!nodeToBeSplit.IsLeaf)
            {
                newNode.Children.AddRange(nodeToBeSplit.Children.GetRange(this.Degree, this.Degree));
                nodeToBeSplit.Children.RemoveRange(this.Degree, this.Degree);
            }
        }

        private void InsertNonFull(Node<TKey, TPath> node, TKey newKey, TPath newPointer)
        {
            int positionToInsert = node.Entries.TakeWhile(entry => newKey.CompareTo(entry.Key) >= 0).Count();

            // конечный узел
            if (node.IsLeaf)
            {
                node.Entries.Insert(positionToInsert, new Entry<TKey, TPath>() { Key = newKey, Pointer = newPointer });
                return;
            }

            // неконечный узел
            Node<TKey, TPath> child = node.Children[positionToInsert];
            if (child.HasReachedMaxEntries)
            {
                this.SplitChild(node, positionToInsert, child);
                if (newKey.CompareTo(node.Entries[positionToInsert].Key) > 0)
                {
                    positionToInsert++;
                }
            }
            this.InsertNonFull(node.Children[positionToInsert], newKey, newPointer);
        }
    }
}