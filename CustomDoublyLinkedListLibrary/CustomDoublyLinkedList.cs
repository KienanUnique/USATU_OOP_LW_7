namespace CustomDoublyLinkedListLibrary
{
    public class CustomDoublyLinkedList<T>
    {
        internal class DoublyLinkedElement
        {
            public T Object;
            public readonly bool IsBorder;
            public DoublyLinkedElement Next;
            public DoublyLinkedElement Previous;

            public DoublyLinkedElement()
            {
                IsBorder = true;
            }

            public DoublyLinkedElement(DoublyLinkedElement previous)
            {
                IsBorder = true;
                Previous = previous;
            }

            public DoublyLinkedElement(T objectSet, DoublyLinkedElement next, DoublyLinkedElement previous)
            {
                IsBorder = false;
                Object = objectSet;
                Next = next;
                Previous = previous;
            }
        }

        public int Count { get; private set; }

        public T First
        {
            get => _firstElement.Object;
            set => _firstElement.Object = value;
        }

        public T Last
        {
            get => _lastElement.Object;
            set => _lastElement.Object = value;
        }

        private DoublyLinkedElement _firstElement;
        private DoublyLinkedElement _lastElement;

        public CustomDoublyLinkedList()
        {
            Count = 0;
            _firstElement = new DoublyLinkedElement();
            _firstElement.Next = new DoublyLinkedElement(_firstElement);
            _lastElement = _firstElement.Next;
        }

        public PointerCustomDoublyLinkedList<T> GetPointerOnBeginning()
        {
            return new PointerCustomDoublyLinkedList<T>(_firstElement);
        }

        public PointerCustomDoublyLinkedList<T> GetPointerOnEnd()
        {
            return new PointerCustomDoublyLinkedList<T>(_lastElement);
        }

        public void Add(T objectToAdd)
        {
            if (Count == 0)
            {
                var addedElement = new DoublyLinkedElement(objectToAdd, _lastElement, _firstElement);
                _firstElement.Next = addedElement;
                _lastElement.Previous = addedElement;
                _firstElement = addedElement;
                _lastElement = addedElement;
            }
            else
            {
                _lastElement.Next = new DoublyLinkedElement(objectToAdd, _lastElement.Next, _lastElement);
                _lastElement = _lastElement.Next;
            }

            Count++;
        }

        public void RemovePointerElement(PointerCustomDoublyLinkedList<T> pointer)
        {
            pointer.CurrentElement.Previous.Next = pointer.CurrentElement.Next;
            pointer.CurrentElement.Next.Previous = pointer.CurrentElement.Previous;
            if (pointer.CurrentElement.Next.IsBorder && !pointer.CurrentElement.Previous.IsBorder)
            {
                pointer.CurrentElement = pointer.CurrentElement.Previous;
                _lastElement = pointer.CurrentElement;
            }
            else if (!pointer.CurrentElement.Next.IsBorder && pointer.CurrentElement.Previous.IsBorder)
            {
                pointer.CurrentElement = pointer.CurrentElement.Previous;
                _firstElement = pointer.CurrentElement.Next;
            }
            else if (pointer.CurrentElement.Next.IsBorder && pointer.CurrentElement.Previous.IsBorder)
            {
                pointer.CurrentElement = pointer.CurrentElement.Previous;
                _firstElement = pointer.CurrentElement;
                _lastElement = pointer.CurrentElement.Next;
            }
            else if (!pointer.CurrentElement.Next.IsBorder && !pointer.CurrentElement.Previous.IsBorder)
            {
                pointer.CurrentElement = pointer.CurrentElement.Previous;
            }

            Count--;
        }

        public void InsertAfterPointer(T objectToInsert, PointerCustomDoublyLinkedList<T> pointer)
        {
            if (Count == 0)
            {
                Add(objectToInsert);
            }
            else
            {
                pointer.CurrentElement = new CustomDoublyLinkedList<T>.DoublyLinkedElement(objectToInsert,
                    pointer.CurrentElement.Next, pointer.CurrentElement);
                Count++;
            }
        }

        public void InsertBeforeCurrent(T objectToInsert, PointerCustomDoublyLinkedList<T> pointer)
        {
            if (Count == 0)
            {
                Add(objectToInsert);
            }
            else
            {
                pointer.CurrentElement = new CustomDoublyLinkedList<T>.DoublyLinkedElement(objectToInsert,
                    pointer.CurrentElement, pointer.CurrentElement.Previous);
                Count++;
            }
        }
    }

    public class PointerCustomDoublyLinkedList<T>
    {
        public T Current
        {
            get => CurrentElement.Object;
            set => CurrentElement.Object = value;
        }

        internal CustomDoublyLinkedList<T>.DoublyLinkedElement CurrentElement;

        internal PointerCustomDoublyLinkedList(CustomDoublyLinkedList<T>.DoublyLinkedElement startElement) =>
            CurrentElement = startElement;


        public void MoveNext()
        {
            CurrentElement = CurrentElement.Next;
        }

        public void MovePrevious()
        {
            CurrentElement = CurrentElement.Previous;
        }

        public bool IsBorderReached()
        {
            return CurrentElement.IsBorder;
        }
    }
}