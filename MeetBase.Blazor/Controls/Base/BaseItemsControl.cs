using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The default implementation of the <see cref="IItemsControl{TItem}"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class BaseItemsControl<TItem> : BaseControl
        where TItem : class
    {
        #region Protected Members

        /// <summary>
        /// The member of the <see cref="Items"/> property
        /// </summary>
        protected readonly List<TItem> mItems = new();

        #endregion

        #region Public Properties

        /// <summary>
        /// The items
        /// </summary>
        /// <returns></returns>
        [Parameter]
        public IEnumerable<TItem> Items
        {
            get => mItems;

            set => SetItemsSource(value);
        }

        /// <summary>
        /// Gets the number of items of the items control
        /// </summary>
        public int ItemsCount => mItems.Count;

        /// <summary>
        /// Checks if the items control has items or not
        /// </summary>
        public bool HasItems => mItems.Count > 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseItemsControl() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the items source.
        /// </summary>
        /// <param name="items">The items</param>
        public void SetItemsSource(IEnumerable<TItem> items)
        {
            Clear();
            mItems.AddRange(items);
            OnItemsSourceSet(items);
            StateHasChanged();
        }

        /// <summary>
        /// Checks if the specified <paramref name="item"/> is contained in the <see cref="Items"/>
        /// </summary>
        /// <param name="item">The item to check if it's contained</param>
        /// <returns></returns>
        public bool Contains(TItem item)
        {
            return mItems.Contains(item);
        }

        /// <summary>
        /// Gets and returns the index of the specified <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item whose index to get</param>
        /// <returns></returns>
        public int IndexOf(TItem item)
        {
            return mItems.IndexOf(item);
        }

        /// <summary>
        /// Gets the item based on its index in the items source
        /// </summary>
        /// <param name="index">The index of the item to get</param>
        /// <returns></returns>
        public TItem Get(int index)
        {
            return mItems.ElementAt(index);
        }

        /// <summary>
        /// Adds an item to the items control.
        /// </summary>
        /// <param name="item">The child to add</param>
        public void Add(TItem item) 
        {
            mItems.Add(item);
            OnItemAdded(item);
            StateHasChanged(); 
        }

        /// <summary>
        /// Adds multiple items at once.
        /// NOTE: This method should be used when possible, because it might have a great performance instance
        ///       on some occasions!
        /// </summary>
        /// <param name="items">The items to add</param>
        public void AddRange(IEnumerable<TItem> items)
        {
            mItems.AddRange(items);
            OnItemsAdded(items);
            StateHasChanged();
        }

        /// <summary>
        /// Inserts the specified <paramref name="item"/> to the specified <paramref name="index"/>
        /// </summary>
        /// <param name="item">The item to insert</param>
        /// <param name="index">The index we want the item to get inserted</param>
        public void Insert(int index, TItem item)
        {
            mItems.Insert(index, item);
            OnItemInserted(index, item);
            StateHasChanged();
        }

        /// <summary>
        /// Removes the item from the items control
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void Remove(TItem item)
        {
            mItems.Remove(item);
            OnItemRemoved(item);
            StateHasChanged();
        }

        /// <summary>
        /// Replaces the <paramref name="oldItem"/> with the <paramref name="newItem"/>
        /// </summary>
        /// <param name="newItem">The new item</param>
        /// <param name="oldItem">The item that gets replaced</param>
        public void Replace(TItem newItem, TItem oldItem)
        {
            var index = mItems.IndexOf(oldItem);
            mItems[index] = newItem;
            OnItemReplaced(newItem, oldItem);
            StateHasChanged();
        }

        /// <summary>
        /// Clears all the items from the items control
        /// </summary>
        public void Clear()
        {
            mItems.Clear();
            OnItemsCleared();
            StateHasChanged();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Handles the set of an items source
        /// </summary>
        /// <param name="items">The items</param>
        protected virtual void OnItemsSourceSet(IEnumerable<TItem> items) { }

        /// <summary>
        /// Handles the addition of an item
        /// </summary>
        /// <param name="item">The item</param>
        protected virtual void OnItemAdded(TItem item) { }

        /// <summary>
        /// Handles the addition of multiple items
        /// </summary>
        /// <param name="items">The items</param>
        protected virtual void OnItemsAdded(IEnumerable<TItem> items) { }

        /// <summary>
        /// Handles the insertion of an item
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="item">The item</param>
        protected virtual void OnItemInserted(int index, TItem item) { }

        /// <summary>
        /// Handles the removal of an item
        /// </summary>
        /// <param name="item">The item</param>
        protected virtual void OnItemRemoved(TItem item) { }

        /// <summary>
        /// Handles the replacement of an item
        /// </summary>
        /// <param name="newItem">The new item</param>
        /// <param name="oldItem">The old item</param>
        protected virtual void OnItemReplaced(TItem newItem, TItem oldItem) { }

        /// <summary>
        /// Handles the deletion of all items
        /// </summary>
        protected virtual void OnItemsCleared() { }

        #endregion
    }

    public abstract class ItemsCotrol<TItem, TComponent> : BaseItemsControl<TItem>
        where TItem : class
        where TComponent : ComponentBase
    {
        #region Protected Members

        /// <summary>
        /// The mapper that links the item with the component.
        /// NOTE: The mapper only maps the associated elements to their initial items.
        ///       It doesn't contain all the items of the items control (ex. Separators)!
        /// </summary>
        protected readonly Dictionary<TItem, TComponent> mMapper = new Dictionary<TItem, TComponent>();

        #endregion

        #region Private Members

        /// <summary>
        /// The current index in the  <see cref="BaseItemsControl{TItem}.Items"/> list
        /// </summary>
        private int mCurrentIndex = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the pairs of items and components
        /// </summary>
        public IEnumerable<KeyValuePair<TItem, TComponent>> Pairs => mMapper;

        /// <summary>
        /// Configures the items and components
        /// </summary>
        public Action<TItem, TComponent>? Configure { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The dynamic component
        /// </summary>
        protected TComponent Component
        {
            set
            {
                // Maps the last element of the list to the currently added component
                mMapper.Add(Items.ElementAt(mCurrentIndex), value);

                // Increments the index by one
                mCurrentIndex++;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ItemsCotrol() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets all the associated elements
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TComponent> Elements => mMapper.Values;

        /// <summary>
        /// Checks if the <paramref name="element"/> is in the <see cref="mMapper"/>
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool ContainsElement(TComponent element) => Elements.Contains(element);

        /// <summary>
        /// Gets and returns the index of the specified <paramref name="element"/>
        /// </summary>
        /// <param name="element">The element whose index to get</param>
        /// <returns></returns>
        public int IndexOfElement(TComponent element) => IndexOf(Get(element));

        /// <summary>
        /// Returns the <typeparamref name="TItem"/> that's related to the <paramref name="element"/> and <see cref="null"/> if none is found
        /// </summary>
        /// <param name="element">The associated element</param>
        /// <returns></returns>
        public TItem Get(TComponent element) => mMapper.FirstOrDefault((x) => x.Value == element).Key;

        /// <summary>
        /// Gets the <typeparamref name="TComponent"/> that's related to the specified <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item whose element to get</param>
        /// <returns></returns>
        public TComponent GetElement(TItem item) => mMapper[item];

        #endregion
    }

    /// <summary>
    /// The items control with dynamic components
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TComponent"></typeparam>
    public abstract class DynamicItemsCotrol<TItem, TComponent> : ItemsCotrol<TItem,TComponent>
        where TItem : class
        where TComponent : ComponentBase
    {
        #region Private Members

        /// <summary>
        /// The mapper with the dynamic components
        /// </summary>
        protected readonly Dictionary<TItem, DynamicComponent> mDynamicMapper = new Dictionary<TItem, DynamicComponent>();

        /// <summary>
        /// The current index in the  <see cref="BaseItemsControl{TItem}.Items"/> list
        /// </summary>
        private int mCurrentIndex = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// The component's type
        /// </summary>
        public Type ComponentType => typeof(TComponent);

        /// <summary>
        /// The gap
        /// </summary>
        [Parameter]
        public string? Gap { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The dynamic component
        /// </summary>
        protected new DynamicComponent Component
        {
            set
            {
                // Maps the last element of the list to the currently added component
                mDynamicMapper.Add(Items.ElementAt(mCurrentIndex), value);
                
                // Increments the index by one
                mCurrentIndex++;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DynamicItemsCotrol() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the component references and adds to the pair list
        /// Calls the configure for each component
        /// </summary>
        protected void OnItemsAdded()
        {
            foreach (var dynamicPair in mDynamicMapper)
            {
                if (!mMapper.ContainsKey(dynamicPair.Key))
                {
                    var component = (TComponent)dynamicPair.Value.Instance!;
                    mMapper.Add(dynamicPair.Key, component);

                    Configure?.Invoke(dynamicPair.Key, component);

                    if (component is BaseControl baseControl)
                    {
                        baseControl.InvokeStateHasChanged();
                    }
                }
            }
        }

        protected override void OnItemsCleared()
        {
            mDynamicMapper.Clear();
            mMapper.Clear();
            mCurrentIndex = 0;
        }

        #endregion
    }
}
