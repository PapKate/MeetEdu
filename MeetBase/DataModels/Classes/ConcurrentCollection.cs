using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MeetBase
{
    /// <summary>
    /// A collection that provides type safety 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentCollection<T> : IList<T>
    {
        #region Private Members

        /// <summary>
        /// A lock used for providing safe access to the <see cref="mList"/>
        /// </summary>
        private readonly object mLock = new();

        /// <summary>
        /// The list
        /// </summary>
        private readonly List<T> mList = new();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                lock (mLock)
                {
                    return mList[index];
                }
            }

            set
            {
                lock (mLock)
                {
                    mList[index] = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the collection
        /// </summary>
        public int Count => mList.Count;

        /// <summary>
        /// Gets a value indicating whether the collection is read-only
        /// </summary>
        public bool IsReadOnly => ((IList<T>)mList).IsReadOnly;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConcurrentCollection() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{mList.Count} item/s";

        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="item">The item</param>
        public void Add(T item)
        {
            lock (mLock)
            {
                mList.Add(item);
            }
        }

        /// <summary>
        /// Adds multiple item to the collection
        /// </summary>
        /// <param name="items">The items</param>
        public void AddRange(IEnumerable<T> items)
        {
            lock (mLock)
            {
                mList.AddRange(items);
            }
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            lock (mLock)
            {
                mList.Clear();
            }
        }

        /// <summary>
        /// Determines whether the collection contains the specific item
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            lock (mLock)
            {
                return mList.Contains(item);
            }
        }

        /// <summary>
        /// Copies the elements of the collection to an array
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="Array"/> that is the destination of the elements copied
        /// from <see cref="ICollection{T}"/>. The <see cref="Array"/> must have zero-based
        /// indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (mLock)
            {
                mList.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// NOTE: The enumerator from a shallow copy of the list is returned!
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            lock (mLock)
            {
                return mList.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Determines the index of a specific item
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns>The index of item if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            lock (mLock)
            {
                return mList.IndexOf(item);
            }
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert</param>
        public void Insert(int index, T item)
        {
            lock (mLock)
            {
                mList.Insert(index, item);
            }
        }

        /// <summary>
        /// Inserts the elements of a collection into the collection
        /// at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection whose elements should be inserted into the collection</param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            lock (mLock)
            {
                mList.InsertRange(index, collection);
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the.
        /// </summary>
        /// <param name="item">The object to remove from the collection.</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            lock (mLock)
            {
                return mList.Remove(item);
            }
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <returns></returns>
        public void RemoveAt(int index)
        {
            lock (mLock)
            {
                mList.RemoveAt(index);
            }
        }

        /// <summary>
        /// Removes a range of elements from the collection
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        public void RemoveRange(int index, int count)
        {
            lock (mLock)
            {
                mList.RemoveRange(index, count);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// NOTE: The enumerator from a shallow copy of the list is returned!
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (mLock)
            {
                return mList.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Reverses the order of the elements in the entire collection
        /// </summary>
        public void Reverse()
        {
            lock (mLock)
            {
                mList.Reverse();
            }
        }

        /// <summary>
        /// Sorts the elements in the entire list using the default comparer
        /// </summary>
        public void Sort()
        {
            lock (mLock)
            {
                mList.Sort();
            }
        }

        /// <summary>
        /// Sorts the elements in the entire collection using the specified
        /// comparer
        /// </summary>
        /// <param name="comparer">
        /// The <see cref="IComparer{T}"/> implementation to use when comparing
        /// elements, or null to use the default comparer
        /// </param>
        public void Sort(IComparer<T> comparer)
        {
            lock (mLock)
            {
                mList.Sort(comparer);
            }
        }

        /// <summary>
        /// Copies the elements of the collection to a new array
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            lock (mLock)
            {
                return mList.ToArray();
            }
        }

        /// <summary>
        /// Determines whether the collection contains elements that
        /// match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The delegate that defines the conditions of the elements to search for.</param>
        /// <returns></returns>
        public bool Exists(Predicate<T> match)
        {
            lock (mLock)
            {
                return mList.Exists(match);
            }
        }

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
        /// <returns>The number of elements removed from the collection</returns>
        public int RemoveAll(Func<T, bool> match)
        {
            lock (mLock)
            {
                return mList.RemoveAll(x => match(x));
            }
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified
        /// predicate, and returns the first occurrence within the entire collection
        /// </summary>
        /// <param name="match">The delegate that defines the conditions of the element to search for</param>
        /// <returns>
        /// The first element that matches the conditions defined by the specified predicate,
        /// if found; otherwise, the default value for type <typeparamref name="T"/>.
        /// </returns>
        public T? Find(Func<T, bool> match)
        {
            lock (mLock)
            {
                return mList.Find(x => match(x));
            }
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified
        /// predicate, and returns the last occurrence within the entire collection
        /// </summary>
        /// <param name="match">The delegate that defines the conditions of the element to search for</param>
        /// <returns>
        /// The last element that matches the conditions defined by the specified predicate,
        /// if found; otherwise, the default value for type <typeparamref name="T"/>.
        /// </returns>
        public T? FindLast(Func<T, bool> match)
        {
            lock (mLock)
            {
                return mList.FindLast(x => match(x));
            }
        }

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified
        /// predicate.
        /// </summary>
        /// <param name="match">The delegate that defines the conditions of the elements to search for.</param>
        /// <returns>
        /// A <see cref="List{T}"/> containing all the elements that match the
        /// conditions defined by the specified predicate, if found; otherwise, an empty
        /// <see cref="List{T}"/>
        /// </returns>
        public List<T> FindAll(Func<T, bool> match)
        {
            lock (mList)
            {
                return mList.FindAll(x => match(x));
            }
        }

        /// <summary>
        /// Converts the elements in the current collection to another type,
        /// and returns a list containing the converted elements
        /// </summary>
        /// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
        /// <param name="converter">A delegate that converts each element from one type to another type.</param>
        /// <returns></returns>
        public List<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
        {
            lock (mLock)
            {
                return mList.ConvertAll(x => converter(x));
            }
        }

        #endregion
    }
}
